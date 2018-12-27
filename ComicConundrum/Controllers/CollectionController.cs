using ComicConundrum.Services;
using ComicConundrum.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ComicConundrum.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CollectionController : Controller {
        private readonly ComicDbService _comicDbService;

        public CollectionController(ComicDbService comicDbService) {
            _comicDbService = comicDbService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search(string title) {
            throw new NotImplementedException();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Save([FromBody]ComicListing comic) {
            await _comicDbService.SaveComicAsync(comic);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Save(int id) {
            await _comicDbService.DeleteComicAsync(id);
            return Ok();
        }

        [HttpGet("")]
        public IActionResult GetAll() {
            var comics = _comicDbService.GetAllComics();
            return Ok(comics);
        }
    }
}
