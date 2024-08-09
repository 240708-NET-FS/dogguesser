import { jwtDecode } from 'jwt-decode';
class ProfilePage {
    constructor() {
        this.token = localStorage.getItem('jwt');
        this.welcomeMessage = document.getElementById('welcome-message');
        this.scoresTableBody = document.querySelector('#scores-table tbody');
        this.playGameButton = document.getElementById('play-game-button');
        this.logoutButton = document.getElementById('logout-button');

        if (!this.token) {
            this.handleMissingToken();
        } else {
            this.init();
        }
    }

    init() {
        const decodedToken = jwtDecode(this.token);
        this.username = decodedToken.UserName; 
        this.userID = decodedToken.UserID; 
        this.scores = []; 

        this.fetchScores().then(data => {
            this.scores = data;
            console.log (this.scores);
            this.populateScoresTable();
        })

        

        this.setWelcomeMessage();
        this.initEventListeners();
    }

    handleMissingToken() {
        window.location.href = '../loginPage/login.html';
    }

    setWelcomeMessage() {
        this.welcomeMessage.textContent = `Welcome, ${this.username}!`;
    }

    populateScoresTable() {

    const decodedToken = jwtDecode(this.token);
    const userID = decodedToken.UserID;

    const filteredScores = this.scores.filter(score => score.userID == userID);
    console.log(userID);
        filteredScores.forEach(score => {
            const date = new Date(score.date);
            const options = {  year: '2-digit', month: '2-digit', day: '2-digit' };
            const formattedDate = date.toLocaleTimeString('en-US', options);
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${score.scoreValue}</td>
                <td>${`${formattedDate}`}</td>
            `;
            this.scoresTableBody.appendChild(row);
        });
    }

    initEventListeners() {
        this.playGameButton.addEventListener('click', () => {
            window.location.href = '../gamePage/game.html'; 
        });

        this.logoutButton.addEventListener('click', () => {
            localStorage.removeItem('jwt');
            window.location.href = '../loginPage/login.html';
        });
    }
    fetchScores() {
        return fetch("http://localhost:5153/api/Score/leaderboard ")
            .then(response => {
                if (!response.ok) {
                    throw new Error('failed to get scoreboard response.');
                }
                return response.json();
            })
            .then(data => data)
            .catch(error => {
                console.error('problem fetching scoreboard', error);
                return [];
            });
    }
}

document.addEventListener('DOMContentLoaded', () => {
    new ProfilePage();
});