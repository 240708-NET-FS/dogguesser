class Round {
    constructor(number, imageUrl, correctAnswer) {
        this.number = number;
        this.imageUrl = imageUrl;
        this.correctAnswer = correctAnswer;
    }
}


let options = [
    'Akita',
    'American Bulldog',
    'Poodle',
    'Afghan Hound',
    'Kai ken',
    'Siberian Husky',
    'Labrador',
    'fox'
];

let rounds = [];

const FetchDogs = () => {
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
};


const FetchGame = () => {
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
};




const image = "https://upload.wikimedia.org/wikipedia/commons/d/d5/Retriever_in_water.jpg" //placeholder

const dropdown = document.getElementById('dropdown');
const searchInput = document.getElementById('search-input');
const displayImage = document.getElementById('display-image');
const form = document.getElementById('input-form');
const submitButton = document.getElementById('submit-button')

const toggleSubmitButton = () => {
    submitButton.disabled = !searchInput.value.trim();
  };


const waitForUserInput = () => {

    searchInput.addEventListener('input', toggleSubmitButton);

    return new Promise((resolve) => {
        form.addEventListener('submit', (event) => {
            event.preventDefault();
            resolve(searchInput.value);
            searchInput.value = '';
            toggleSubmitButton();
        }, { once: true });
    });
};

toggleSubmitButton();

FetchDogs().then(items => {
    options = items;
    FetchGame().then(r => {
        rounds = r;
        processRounds();
    })
});

// FetchGame().then(r => {
//     rounds = r;
// })


console.log(options);

const populate = (options) => {
    dropdown.innerHTML = ''

    options.forEach(option => {
        const dropdownItem = document.createElement('div');
        dropdownItem.textContent = option;

        dropdownItem.addEventListener('click', () => {
            searchInput.value = option;
            dropdown.style.display = 'none';
        });
        dropdown.appendChild(dropdownItem);
    });
}

const updateImage = () => {
    if (image) {
        displayImage.src = image;
    }
}

const updateImageUrl = (imgurl) => {
    if (imgurl) {
        displayImage.src = imgurl;
    }
}




searchInput.addEventListener('input', () => {
    const searchTerm = searchInput.value.toLowerCase();
    const filterOptions = options.filter(option => option.toLowerCase().includes(searchTerm));

    if (filterOptions.length > 0) {
        populate(filterOptions);
        dropdown.style.display = 'block';
    } else {
        dropdown.style.display = 'none'
    }

})


dropdown.addEventListener('change', () => {
    const selectedOption = dropdown.value;
    if (selectedOption) {
        searchInput.value = selectedOption;
    }
});

// const rungame = () => {

//     console.log("Running Game with rounds:" + rounds);
//     let guessesCorrect = 0;
//     populate(options);


//     // rounds.forEach(r => {
//     //     console.log("creating round with " + r.imageUrl)
//     //     updateImageUrl(r.imageUrl);
//     //     let correctAnswer = r.correctAnswer;

//     //     const submittedInput = waitForUserInput();
//     //     console.log(`User Selected ${submittedInput} correctAnswer was ${correctAnswer}`);


//     // });
// };

const processRounds = async () => {
    for (const r of rounds) {
        console.log(r);
        updateImageUrl(r.imageUrl);
        const userInput = await waitForUserInput();
        console.log(`User input: ${r.number}: ${userInput}`);
    }
};





// updateImage();