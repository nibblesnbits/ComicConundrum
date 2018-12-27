using ComicConundrum.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComicConundrum.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ComicsController : Controller {
        private readonly MarvelComicsApi _marvelComicsApi;

        public ComicsController(MarvelComicsApi marvelComicsApi) {
            _marvelComicsApi = marvelComicsApi;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search(string title) {
            var result = await _marvelComicsApi.SearchByTitleAsync(title);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var comic = await _marvelComicsApi.GetById(id);
            return Ok(comic);
        }
    }
}
