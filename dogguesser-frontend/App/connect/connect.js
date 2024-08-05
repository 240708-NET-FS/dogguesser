import config from "../config"
import Round from "../models/Rounds"
class Connector {
    FetchGameInfo = async () => {
        try {
            const response = await (`${config.backendUrl}/newgame`);
            const retrieved = await response.json();
            return retrieved.rounds.map((round) => new Round(round.number, round.imageUrl, round.correctAnswer));

        } catch (error) {
            console.error('could not fetch game information')
            return [];
        }
    }


    FetchDogs = async () => {
        try {
            const response = await (`${config.backendUrl}/dogs`);
            const retrieved = await response.json();

            const doglist = retrieved.dogs.map(dog => dog);

            return doglist;

        } catch (error) {
            console.error('could not fetch game information')
            return [];
        }
    }
}