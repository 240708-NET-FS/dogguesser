const BoardPage = require('../src/boardPage/board');
import fetchMock from "jest-fetch-mock";
import { jwtDecode } from "jwt-decode";

fetchMock.enableMocks();
// jest.mock('jwt-decode');
jest.mock('jwt-decode', () => ({
  jwtDecode: () => ({ Role: 'Admin' }),
}));


describe('BoardPage', () => {
  let boardPage;


  beforeEach(() => {

    //supressing these to make the output cleaner
    console.error = jest.fn();
    // console.log = jest.fn();

    // Set up the document body with necessary elements
    document.body.innerHTML = `
      <div>
        <table id="scores-table">
          <tbody></tbody>
        </table>
        <button id="play-game-button">Play Game</button>
      </div>
    `;

    boardPage = new BoardPage();
  });

  afterEach(() => {
    jest.clearAllMocks();
  });

  describe('fetchScores', () => {

    global.config = {
      apiUrl: 'https://api.example.com'
    };

    const mockScores = [
      { username: 'user1', score: 8, date: '2024-08-10T00:00:00Z' },
      { username: 'user2', score: 5, date: '2024-08-11T00:00:00Z' },
    ];


    it('should fetch scores from the API', async () => {

      fetch.mockResponseOnce(JSON.stringify(mockScores));

      const data = await boardPage.fetchScores();
      expect(fetch).toHaveBeenCalledWith(`${global.config.apiUrl}/Score/leaderboard`);
      expect(data).toEqual(mockScores);
    });

    it('should handle fetch errors', async () => {
      fetch.mockResponseOnce(() => Promise.reject("oof"));

      const data = await boardPage.fetchScores();

      expect(data).toEqual([]);
    });
  });

  describe('populateScoresTable', () => {
    it('should populate the scores table with fetched data', () => {
      boardPage.scores = [
        { scoreValue: '1', date: '2024-01-01T00:00:00Z' },
        { scoreValue: '4', date: '2024-02-02T00:00:00Z' },
      ];

      boardPage.populateScoresTable();

      const rows = document.querySelectorAll('#scores-table tbody tr');

      expect(rows.length).toBe(2);

      expect(rows[0].innerHTML).toContain('1');

      expect(rows[1].innerHTML).toContain('4');
    });
  });

  describe('isAdmin function', () => {

    Storage.prototype.getItem = jest.fn(() => "mocked-jwt-token");

    it('should return true when the Role is Admin', () => {
      expect(boardPage.isAdmin()).toBe(true);
    });

    it('should return false when there is no token', () => {


      const userboardPage = new BoardPage();
      userboardPage.token = "";

      expect(userboardPage.isAdmin()).toBe(false);
    });
  });

});