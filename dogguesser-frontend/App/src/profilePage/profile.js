import { jwtDecode } from 'jwt-decode';
class ProfilePage {
    constructor() {
        this.token = localStorage.getItem('jwt');
        this.welcomeMessage = document.getElementById('welcome-message');
        this.scoresTableBody = document.querySelector('#scores-table tbody');
        this.playGameButton = document.getElementById('play-game-button');

        if (!this.token) {
            this.handleMissingToken();
        } else {
            this.init();
        }
    }

    init() {
        const decodedToken = jwtDecode(this.token);
        this.username = decodedToken.UserName; 
        this.scores = [
            { game: 'Game 1', score: 150, date: '2023-01-01' },
            { game: 'Game 2', score: 200, date: '2023-02-15' },
            { game: 'Game 3', score: 180, date: '2023-03-20' }
        ]; 

        this.setWelcomeMessage();
        this.populateScoresTable();
        this.initEventListeners();
    }

    handleMissingToken() {
        window.location.href = '../loginPage/login.html';
    }

    setWelcomeMessage() {
        this.welcomeMessage.textContent = `Welcome, ${this.username}!`;
    }

    populateScoresTable() {
        this.scores.forEach(score => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${score.game}</td>
                <td>${score.score}</td>
                <td>${score.date}</td>
            `;
            this.scoresTableBody.appendChild(row);
        });
    }

    initEventListeners() {
        this.playGameButton.addEventListener('click', () => {
            window.location.href = '../gamePage/game.html'; 
        });
    }
}

document.addEventListener('DOMContentLoaded', () => {
    new ProfilePage();
});