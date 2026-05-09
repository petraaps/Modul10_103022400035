using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Modul10_103022400035
{
    // Controller untuk mengelola data game
    [Route("api/[controller]")]
    [ApiController]

    public class GameController : ControllerBase
    {
        //List untuk meyimpan data game
        private static List<Game> games = new List<Game>
        {
            new Game(1, "Valorant", "Riot Games", 2020, "FPS", 8.5, new string[] { "PC" }, new string[] { "Multiplayer" }, true, 0),
            new Game(2, "GTA V", "Rockstar Games", 2013, "Open World", 9.5, new string[] { "PC", "PS4", "PS5", "Xbox" }, new string[] { "Single-player", "Multiplayer" }, true, 300000),
            new Game(3, "The Witcher 3", "CD Projekt Red", 2015, "RPG", 9.7, new string[] { "PC", "PS4", "PS5", "Xbox", "Switch" }, new string[] { "Single-player" }, false, 250000)
        };

        //Untuk mengambil semua data game
        [HttpGet]
        public ActionResult<List<Game>> GetAllGames()
        {
            return Ok(games);
        }

        //Untuk mengambil data game berdasarkan id
        [HttpGet("{id}")]
        public ActionResult<Game> GetGameById(int id)
        {
            //Mencari game berdasarkan id
            var game = games.FirstOrDefault(g => g.Id == id);
            //Jika game tidak ditemukan, kembalikan NotFound
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        //Untuk menambahkan data game baru
        [HttpPost]
        public ActionResult Post([FromBody] Game newGame)
        {
            //Validasi input
            if (newGame == null)
            {
                //Jika input tidak valid, kembalikan BadRequest
                return BadRequest();
            }
            games.Add(newGame);
            return CreatedAtAction(nameof(GetGameById), new { id = games.Count - 1 }, newGame);
        }

        //Untuk mengupdate data game berdasarkan id
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            //Mencari game berdasarkan id
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                //Jika game tidak ditemukan, kembalikan NotFound
                return NotFound();
            }
            games.Remove(game);
            return NoContent();
        }
    }
}
