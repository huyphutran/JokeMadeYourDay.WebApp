namespace JokeMadeYourDay.WebApp.Models.Repositorys
{
    public interface IJokeRepository
    {
        void AddVote(int jokeId, int voteValue);
        Joke GetNextJoke(IEnumerable<int> seenJokes);
        Joke GetPreviousJoke(IEnumerable<int> seenJokes);
        Joke GetJokeById(int id);
    }
}