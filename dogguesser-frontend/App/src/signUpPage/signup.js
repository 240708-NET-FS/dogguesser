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
            password: password,
            admUser: false
        };

        fetch(`${global.config.apiUrl}/User`, {
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
            alert("User Created!")
            window.location.href = '../loginPage/login.html';

        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            alert('user creation failed. Please try again.');
        });
    }
}

document.addEventListener('DOMContentLoaded', () => {
    new Login();
});