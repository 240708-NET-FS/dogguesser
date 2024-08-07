
const username = document.getElementById('username').value;
const password = document.getElementById('password').value;
const loginForm = document.getElementById('login-form');



loginForm.addEventListener('submit', function(event) {
    event.preventDefault();
    console.log("logging in!")
    //login(username, password);
});
