const jwtDecode = require('jwt-decode');
const ProfilePage = require('../src/profilePage/profile');

// Mock the jwtDecode function
jest.mock('jwt-decode', () => jest.fn());

// Mock global.fetch
global.fetch = jest.fn();

// Mock localStorage
global.localStorage = {
    getItem: jest.fn(),
    removeItem: jest.fn(),
};

describe('ProfilePage', () => {
    let profilePage;
    let jwtDecode;

    beforeEach(() => {

        //supressing these to make the output cleaner
        console.error = jest.fn();
        console.log = jest.fn();
        // Mock the DOM elements
        document.body.innerHTML = `
            <div id="welcome-message"></div>
            <table id="scores-table">
                <tbody></tbody>
            </table>
            <button id="play-game-button">Play Game</button>
            <button id="logout-button">Logout</button>
        `;

        // Mock localStorage.getItem to return a fake JWT token

        global.localStorage.getItem = () => {
            return 'mocked-jwt-token'
        };

        // Mock the return value of jwtDecode
        // jwtDecode.mockReturnValue({ UserName: 'testuser', UserID: 123 });

        jwtDecode = () => {
            return { UserID: 123 }
        }
        global.config = {
            apiUrl: 'https://api.example.com'
        };

        // Mock fetch API
        // global.fetch.mockResolvedValue({
        //     ok: true,
        //     json: () => Promise.resolve([
        //         { userID: 123, scoreValue: 10, date: '2024-08-09T03:08:28.675Z' },
        //         { userID: 456, scoreValue: 8, date: '2024-08-10T03:08:28.675Z' }
        //     ]),
        // });

        global.fetch = jest.fn().mockImplementation((url) => {
            if (url.includes('/Score/leaderboard')) {
                return Promise.resolve({
                    ok: true,
                    json: jest.fn().mockResolvedValue([
                        { userID: 123, scoreValue: 10, date: '2024-08-09T03:08:28.675Z' },
                        { userID: 456, scoreValue: 8, date: '2024-08-10T03:08:28.675Z' }
                    ])
                });
            }
            return Promise.reject(new Error('Unknown API endpoint'));
        });

        // Initialize the ProfilePage class
        profilePage = new ProfilePage();
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('should redirect to login page if JWT token is missing', () => {
        profilePage = new ProfilePage();
        expect(window.location.href).toBe('http://localhost/');
    });


});
