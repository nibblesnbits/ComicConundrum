using ComicConundrum.Services;
using ComicConundrum.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComicConundrum.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ComicsController : Controller {
        private readonly MarvelComicsApi _marvelComicsApi;
        private readonly ComicDbService _comicDbService;

        public ComicsController(MarvelComicsApi marvelComicsApi, ComicDbService comicDbService) {
            _marvelComicsApi = marvelComicsApi;
            _comicDbService = comicDbService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search(string title) {
            var result = await _marvelComicsApi.SearchComicsByTitle(title);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Save([FromBody]ComicListing comic) {
            await _comicDbService.SaveComicAsync(comic);
            return Ok();
        }
    }
}
