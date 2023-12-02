namespace JokeMadeYourDay.WebApp.Models.Repositorys
{
    public class JokeRepository : IJokeRepository
    {
        private List<Joke> _jokeList;
        public JokeRepository()
        {
            _jokeList = new List<Joke>()
            {
                new Joke() {Id = 1, Content = "A child asked his father, \"How were people born?\" So his father said, \"Adam and Eve made babies, then their babies became adults and made babies, and so on.\"\r\n\r\nThe child then went to his mother, asked her the same question and she told him, \"We were monkeys then we evolved to become like we are now.\"\r\n\r\nThe child ran back to his father and said, \"You lied to me!\" His father replied, \"No, your mom was talking about her side of the family.\"",Vote = 10},
                new Joke() {Id = 2,Content ="Teacher: \"Kids,what does the chicken give you?\" Student: \"Meat!\" Teacher: \"Very good! Now what does the pig give you?\" Student: \"Bacon!\" Teacher: \"Great! And what does the fat cow give you?\" Student: \"Homework!\"\r\n\r\n",Vote = 10},
                new Joke() {Id = 3,Content ="The teacher asked Jimmy, \"Why is your cat at school today Jimmy?\" Jimmy replied crying, \"Because I heard my daddy tell my mommy, 'I am going to eat that pussy once Jimmy leaves for school today!'\"",Vote = 10},
                new Joke() {Id = 4,Content ="A housewife, an accountant and a lawyer were asked \"How much is 2+2?\" The housewife replies: \"Four!\". The accountant says: \"I think it's either 3 or 4. Let me run those figures through my spreadsheet one more time.\" The lawyer pulls the drapes, dims the lights and asks in a hushed voice, \"How much do you want it to be?\"",Vote = 10}

            };
        }
        public Joke GetJokeById(int id)
        {
            return _jokeList.FirstOrDefault(joke => joke.Id == id);
        }
        public Joke GetNextJoke(IEnumerable<int> seenJokes)
        {

            return _jokeList.FirstOrDefault(x => !seenJokes.Contains(x.Id));
        }
        public Joke GetPreviousJoke(IEnumerable<int> seenJokes)
        {
            if (!seenJokes.Any()) return null;

            var lastSeenJokeId = seenJokes.Last();
            var lastSeenIndex = _jokeList.FindIndex(x => x.Id == lastSeenJokeId);

            if (lastSeenIndex > 0)
            {
                return _jokeList[lastSeenIndex - 1];
            }

            return null;
        }

        public void AddVote(int jokeId, int voteValue)
        {
            var joke = _jokeList.FirstOrDefault(j => j.Id == jokeId);
            if (joke != null)
            {
                if (voteValue < 0 && joke.Vote <= 0)
                {
                    return;
                }

                joke.Vote += voteValue;
            }
        }
    }
}
