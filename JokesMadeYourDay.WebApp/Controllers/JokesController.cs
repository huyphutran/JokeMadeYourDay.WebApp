using JokeMadeYourDay.WebApp.Models;
using JokeMadeYourDay.WebApp.Models.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace JokesMadeYourDay.WebApp.Controllers
{
    public class JokesController : Controller
    {
        private readonly IJokeRepository jokeRepository;

        public JokesController(IJokeRepository jokeRepository)
        {
            this.jokeRepository = jokeRepository;
        }

        public IActionResult Index()
        {
            var seenJokes = GetSeenJokes();
            Joke joke;
            
            if(!seenJokes.Any())
            {
                joke = jokeRepository.GetJokeById(1);

            }else
            {
                joke = jokeRepository.GetNextJoke(seenJokes);
            }
            if(joke == null)
            {
                return View("EndOfJokes");
            }

            UpdateSeenJokes(joke.Id);
            return View(joke);
        }
        private List<int> GetSeenJokes()
        {
            var seenJokesValue = Request.Cookies["SeenJokes"];
            return seenJokesValue != null ? seenJokesValue.Split(',').Select(int.Parse).ToList() : new List<int>();
        }

        private void UpdateSeenJokes(int jokeId)
        {
            var seenJokes = GetSeenJokes();
            seenJokes.Add(jokeId);

            var options = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1),
                HttpOnly = true,
                IsEssential = true
            };
            Response.Cookies.Append("SeenJokes", string.Join(",", seenJokes), options);
        }
        
        public IActionResult Vote(int jokeId, bool liked)
        {
            var joke = jokeRepository.GetJokeById(jokeId);
            if (joke != null)
            {
                if (liked)
                {
                    // Increment vote if 'like' button is clicked
                    jokeRepository.AddVote(jokeId, 1);
                }
                else if (!liked && joke.Vote > 0)
                {
                    // Decrement vote if 'dislike' is clicked 
                    jokeRepository.AddVote(jokeId, -1);
                }
            }

            return RedirectToAction("Index");
        }

    }
}
