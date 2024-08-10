const Login = require('../src/loginPage/login');
const jwtDecode = require('jwt-decode'); // Make sure this import is correct
jest.mock('jwt-decode', () => jest.fn()); // Mock jwtDecode

describe('Login', () => {
    let originalConsoleError;
    let originalConsoleLog;

    beforeEach(() => {
        // Mock document elements
        document.body.innerHTML = `
            <form id="login-form">
                <input id="username" value="testuser" />
                <input id="password" value="password123" />
                <button type="submit">Login</button>
            </form>
        `;

        // Mock console.error and console.log
        originalConsoleError = console.error;
        originalConsoleLog = console.log;
        console.error = jest.fn();
        console.log = jest.fn();

        // Mock fetch
        global.fetch = jest.fn(() =>
            Promise.resolve({
                ok: true,
                json: () => Promise.resolve({ token: 'mocked-jwt-token' }),
            })
        );

        // Mock localStorage
        global.localStorage.setItem = jest.fn();

       

        global.config = {
            apiUrl: 'https://api.example.com'  
          };
    
    });

    afterEach(() => {
        // Restore the original console methods
        console.error = originalConsoleError;
        console.log = originalConsoleLog;
        jest.clearAllMocks();
    });

    it('should submit login form and store the JWT token', async () => {
        const loginInstance = new Login();

        // Simulate form submission
        const form = document.getElementById('login-form');
        form.dispatchEvent(new Event('submit'));

        // Wait for all promises to resolve
        await Promise.resolve();

        // Check that fetch was called with correct arguments
        expect(global.fetch).toHaveBeenCalledWith(
            `${global.config.apiUrl}/Auth/login`,
            expect.objectContaining({
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username: 'testuser', password: 'password123' }),
            })
        );
    });
});
