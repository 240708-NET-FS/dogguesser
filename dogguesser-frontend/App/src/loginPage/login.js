import { jwtDecode } from 'jwt-decode';

class Login {
    constructor() {
        this.loginForm = document.getElementById('login-form');
        this.initEventListeners();
    }

    initEventListeners() {
        this.loginForm.addEventListener('submit', (event) => this.handleLogin(event));
    }

    handleLogin(event) {
        event.preventDefault();
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;
        console.log("Logging in!");

        this.login(username, password);
    }

    login(username, password) {
        const loginData = {
            username: username,
            password: password
        };

        fetch('http://localhost:5153/api/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Login successful!', data);
            if (data.token) {
                localStorage.setItem('jwt', data.token);   
                const decoded = jwtDecode(data.token);
                console.log('Decoded JWT:', decoded);
           
            }

            window.location.href = '../profilePage/profile.html';

        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            alert('Login failed. Please try again.');
        });
    }
}

document.addEventListener('DOMContentLoaded', () => {
    new Login();
});