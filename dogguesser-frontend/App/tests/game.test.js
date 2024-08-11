import { jwtDecode } from "jwt-decode";
const DogGuesser = require('../src/gamePage/game');

// Mock localStorage
global.localStorage = {
    getItem: jest.fn(),
    setItem: jest.fn(),
};

describe('DogGuesser', () => {
    let dogGuesser;
    let jwtDecode;

    beforeEach(() => {
        //supressing these to make the output cleaner
        console.error = jest.fn();
        console.log = jest.fn();
        document.body.innerHTML = `
            <div id="dropdown"></div>
            <input id="search-input" />
            <img id="display-image" />
            <form id="input-form">
                <button id="submit-button" type="submit">Submit</button>
            </form>
        `;

        // global.localStorage.getItem.mockReturnValue('mocked-jwt-token');
        global.localStorage.getItem = () => {
            return 'mocked-jwt-token'
        };
        // jwtDecode.mockReturnValue({ UserID: 123 });
        jwtDecode = () => {
            return { UserID: 123 }
        }
        global.config = {
            apiUrl: 'https://api.example.com'
        };

        global.fetch = jest.fn((url) => {
            if (url.includes('/breeds')) {
                return Promise.resolve({
                    ok: true,
                    json: () => Promise.resolve({ dogs: ['Akita', 'Poodle'] }),
                });
            } else if (url.includes('/newgame')) {
                return Promise.resolve({
                    ok: true,
                    json: () => Promise.resolve({
                        rounds: [
                            { number: 1, imageUrl: 'https://example.com/image1.jpg', correctAnswer: 'Akita' },
                            { number: 2, imageUrl: 'https://example.com/image2.jpg', correctAnswer: 'Poodle' },
                        ],
                    }),
                });
            } else if (url.includes('/Score')) {
                return Promise.resolve({
                    ok: true,
                    json: () => Promise.resolve({ status: 'success' }),
                });
            }
            return Promise.reject(new Error('Unknown API endpoint'));
        }
        );

        dogGuesser = new DogGuesser();
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('should fetch dogs and game data', async () => {
        await Promise.resolve(); // Wait for async operations in init()

        expect(fetch).toHaveBeenCalledTimes(2);
        expect(fetch).toHaveBeenCalledWith(`${global.config.apiUrl}/breeds`);
        expect(fetch).toHaveBeenCalledWith(`${global.config.apiUrl}/newgame`);

        expect(dogGuesser.options).toEqual(['Akita', 'Poodle']);
        expect(dogGuesser.rounds).toHaveLength(2);
    });
    it('should correctly update the image URL', () => {
        dogGuesser.updateImageUrl('https://example.com/new-image.jpg');
        expect(dogGuesser.displayImage.src).toBe('https://example.com/new-image.jpg');
    });

    it('should populate dropdown correctly', () => {
        dogGuesser.populate(['Akita', 'Poodle']);
        expect(dogGuesser.dropdown.children).toHaveLength(2);
        expect(dogGuesser.dropdown.children[0].textContent).toBe('Akita');
        expect(dogGuesser.dropdown.children[1].textContent).toBe('Poodle');
    });
});