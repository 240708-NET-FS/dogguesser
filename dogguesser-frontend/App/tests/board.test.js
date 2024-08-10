const BoardPage = require('../src/boardPage/board'); 

describe('BoardPage', () => {
  let boardPage;

  beforeEach(() => {

     //supressing these to make the output cleaner
    console.error = jest.fn();
    console.log = jest.fn();
   
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

    global.fetch = jest.fn(() =>
        Promise.resolve({
          ok: true,
          json: () => Promise.resolve(mockScores),
        })
      );
    it('should fetch scores from the API', async () => {

      const data = await boardPage.fetchScores();

      expect(global.fetch).toHaveBeenCalledWith(`${global.config.apiUrl}/Score/leaderboard`);
      expect(data).toEqual(mockScores);
    });

    it('should handle fetch errors', async () => {
      global.fetch = jest.fn(() =>
        Promise.resolve({
          ok: false,
        })
      );

      const data = await boardPage.fetchScores();

      expect(data).toEqual([]);
    });
  });

  describe('populateScoresTable', () => {
    it('should populate the scores table with fetched data', () => {
      // Mock scores data
      boardPage.scores = [
        { username: 'user1', score: 8, date: '2024-01-01T00:00:00Z' },
        { username: 'user2', score: 5, date: '2024-02-02T00:00:00Z' },
      ];

      boardPage.populateScoresTable();

      const rows = document.querySelectorAll('#scores-table tbody tr');
      
      expect(rows.length).toBe(2);

      expect(rows[0].innerHTML).toContain('user1');
      expect(rows[0].innerHTML).toContain('8');

      expect(rows[1].innerHTML).toContain('user2');
      expect(rows[1].innerHTML).toContain('5');
    });
  });
});