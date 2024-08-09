import { jwtDecode } from 'jwt-decode';

class DogGuesser {
    constructor() {
        this.options = [
            'Akita',
            'American Bulldog',
            'Poodle',
            'Afghan Hound',
            'Kai ken',
            'Siberian Husky',
            'Labrador',
            'fox'
        ];

        this.rounds = [];
        this.imagePlaceholder = "https://upload.wikimedia.org/wikipedia/commons/d/d5/Retriever_in_water.jpg";
        this.dropdown = document.getElementById('dropdown');
        this.searchInput = document.getElementById('search-input');
        this.displayImage = document.getElementById('display-image');
        this.form = document.getElementById('input-form');
        this.submitButton = document.getElementById('submit-button');

        this.token = localStorage.getItem('jwt');
        this.decodedToken = "";
        
        
        this.init();
    }

    init() {

        if (this.token) {
            this.decodedToken = jwtDecode(this.token);
        }
        Promise.all([this.fetchDogs(), this.fetchGame()]).then(([dogs, rounds]) => {
            this.options = dogs;
            this.rounds = rounds;
            this.processRounds();
        }).catch(error => {
            console.error('Error during fetch operations:', error);
        });

        this.searchInput.addEventListener('input', () => this.handleSearchInput());
        this.dropdown.addEventListener('change', () => this.handleDropdownChange());
    }

    fetchDogs() {
        return fetch("http://localhost:3001/breeds")
            .then(response => {
                if (!response.ok) {
                    throw new Error('failed to get breeds response.');
                }
                return response.json();
            })
            .then(data => data.dogs)
            .catch(error => {
                console.error('problem fetching breeds', error);
                return [];
            });
    }

    fetchGame() {
        return fetch("http://localhost:3001/newgame")
            .then(response => {
                if (!response.ok) {
                    throw new Error('failed to get newgame response.');
                }
                return response.json();
            })
            .then(data => data.rounds.map(round => new Round(round.number, round.imageUrl, round.correctAnswer)))
            .catch(error => {
                console.error('problem fetching newgame', error);
                return [];
            });
    }

    async postScore(userID, scoreValue) {
        const scoreData = {
            userID: userID,
            date: new Date(),
            scoreValue: scoreValue
        };
    
        try {
            const response = await fetch('http://localhost:5153/api/Score', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(scoreData)
            });
    
            if (!response.ok) {
                throw new Error('ooops. could not post score');
            }
    
            const data = await response.json();
            console.log('Score posted successfully:', data);
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }

    toggleSubmitButton() {
        this.submitButton.disabled = !this.searchInput.value.trim();
    }

    waitForUserInput() {
        this.searchInput.addEventListener('input', () => this.toggleSubmitButton());

        return new Promise((resolve) => {
            this.form.addEventListener('submit', (event) => {
                event.preventDefault();
                resolve(this.searchInput.value);
                this.searchInput.value = '';
            }, { once: true });
        });
    }

    populate(options) {
        this.dropdown.innerHTML = '';
        options.forEach(option => {
            const dropdownItem = document.createElement('div');
            dropdownItem.textContent = option;

            dropdownItem.addEventListener('click', () => {
                this.searchInput.value = option;
                this.dropdown.style.display = 'none';
            });

            this.dropdown.appendChild(dropdownItem);
        });
    }

    updateImageUrl(imgurl) {
        if (imgurl) {
            this.displayImage.src = imgurl;
        }
    }

    handleSearchInput() {
        const searchTerm = this.searchInput.value.toLowerCase();
        const filterOptions = this.options.filter(option => option.toLowerCase().includes(searchTerm));

        if (filterOptions.length > 0) {
            this.populate(filterOptions);
            this.dropdown.style.display = 'block';
        } else {
            this.dropdown.style.display = 'none';
        }
    }

    handleDropdownChange() {
        const selectedOption = this.dropdown.value;
        if (selectedOption) {
            this.searchInput.value = selectedOption;
        }
    }

    async processRounds() {

        let score = 0;
        for (const r of this.rounds) {
            console.log(r);
            this.updateImageUrl(r.imageUrl);
            const userInput = await this.waitForUserInput();
            if (userInput == r.correctAnswer){
                score = score + 1;
            }
            console.log(`User input: ${r.number}: ${userInput}`);
        }

        this.postScore(this.decodedToken.UserID, score)
    }
}

class Round {
    constructor(number, imageUrl, correctAnswer) {
        this.number = number;
        this.imageUrl = imageUrl;
        this.correctAnswer = correctAnswer;
    }
}

document.addEventListener('DOMContentLoaded', () => {
    new DogGuesser();
});