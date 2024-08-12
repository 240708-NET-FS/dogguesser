import { jwtDecode } from 'jwt-decode';


class BoardPage {
    constructor() {

        this.token = localStorage.getItem('jwt');
        this.welcomeMessage = document.getElementById('welcome-message');
        this.scoresTableBody = document.querySelector('#scores-table tbody');
        this.playGameButton = document.getElementById('play-game-button');
        this.logoutButton = document.getElementById('logout-button');

        this.init();
    }

    init() {
        this.scores = []; 

        this.fetchScores().then(data => {
            this.scores = data;
            console.log (this.scores);
            this.populateScoresTable();
        })

        this.initEventListeners();
    }

    isAdmin(){
        if (this.token) {
            const decoded = jwtDecode(this.token);
            console.log("jwtDecodeout:" + decoded);
            return decoded.Role == "Admin" ? true : false;
        } else {
            return false;
        }
    }
   
    populateScoresTable() {
        this.scores.forEach(score => {
            const date = new Date(score.date);
            date.setHours(date.getHours() - 4);
            const options = {  timeZone: 'America/New_York', year: '2-digit', month: '2-digit', day: '2-digit' };
            const formattedDate = date.toLocaleTimeString('en-US', options);
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${score.scoreValue}</td>
                <td>${`${formattedDate}`}</td>
            `;
            row.dataset.scoreId = score.scoreID;
            if (this.isAdmin()) {
                const deleteCell = document.createElement('td');
                deleteCell.className = 'delete-cell';
                deleteCell.textContent = 'X';
                deleteCell.addEventListener('click', function() {
                    const scoreID = row.dataset.scoreId;
                    console.log("deleting using scoreID: " + scoreID)
                    fetch(`${global.config.apiUrl}/Score/DeleteScore/${scoreID}`, {
                        method: 'DELETE',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }) .then (response => {
                        if (!response.ok) {
                        throw new Error(`Failed to delete score with ID ${scoreID}`);
                    }
                    }).then(() => {
                        row.remove();
                    });
                });
    
                row.appendChild(deleteCell);  // Append the delete cell to the row
            }
    
            this.scoresTableBody.appendChild(row);
        });
    }

    initEventListeners() {
        this.playGameButton.addEventListener('click', () => {
            window.location.href = '../gamePage/game.html'; 
        });
    }
    fetchScores() {
        return fetch(`${global.config.apiUrl}/Score/leaderboard`)
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
    new BoardPage();
});

module.exports = BoardPage;