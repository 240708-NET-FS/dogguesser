class HighscoreTable {
    constructor(tableId, apiEndpoint) {
        this.table = document.getElementById(tableId);
        this.apiEndpoint = apiEndpoint;
        this.initialize();
    }

    initialize() {
        this.fetchScores();
        this.initEventListeners();
    }

    initEventListeners() {
        const headers = this.table.querySelectorAll('th');
        headers.forEach(header => {
            header.addEventListener('click', () => this.sortTable(header.dataset.column));
        });
    }

    fetchScores() {
        fetch(this.apiEndpoint)
            .then(response => response.json())
            .then(data => this.populateTable(data))
            .catch(error => console.error('Error fetching scores:', error));
    }

    populateTable(data) {
        const tableBody = this.table.querySelector('tbody');
        tableBody.innerHTML = '';

        data.forEach(score => {
            const row = document.createElement('tr');

            const playerNameCell = document.createElement('td');
            playerNameCell.textContent = score.playerName;
            row.appendChild(playerNameCell);

            const scoreCell = document.createElement('td');
            scoreCell.textContent = score.score;
            row.appendChild(scoreCell);

            const dateCell = document.createElement('td');
            dateCell.textContent = new Date(score.date).toLocaleDateString();
            row.appendChild(dateCell);

            tableBody.appendChild(row);
        });
    }

    sortTable(column) {
        const tableBody = this.table.querySelector('tbody');
        const rows = Array.from(tableBody.rows);
        const isNumeric = column === 'score';

        rows.sort((a, b) => {
            const cellA = a.querySelector(`td:nth-child(${this.getColumnIndex(column)})`).textContent;
            const cellB = b.querySelector(`td:nth-child(${this.getColumnIndex(column)})`).textContent;

            if (isNumeric) {
                return Number(cellA) - Number(cellB);
            } else {
                return cellA.localeCompare(cellB);
            }
        });

        rows.forEach(row => tableBody.appendChild(row));
    }

    getColumnIndex(column) {
        switch (column) {
            case 'playerName': return 1;
            case 'score': return 2;
            case 'date': return 3;
            default: return 1;
        }
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const apiEndpoint = 'https://example.com/api/highscores'; // Replace with your actual API endpoint
    new HighscoreTable('highscore-table', apiEndpoint);
});
