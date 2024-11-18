using MediaService.Data;
using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediaService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        private readonly MediaServiceDbContext _dbContext;

        public MediaController(MediaServiceDbContext context)
        {
            _dbContext = context;
        }

        // Media endpoints

        [HttpGet]
        public async Task<ActionResult<List<MediaItem>>> GetAllMediaItems()
        {
            var mediaItems = await _dbContext.MediaItems
                                             .Include(m => m.MediaType)
                                             .ToListAsync();
            return Ok(mediaItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MediaItem>> GetMediaItem(int id)
        {
            var mediaItem = await _dbContext.MediaItems
                                            .Include(m => m.MediaType) 
                                            .FirstOrDefaultAsync(m => m.Id == id);

            if (mediaItem == null)
            {
                return NotFound();
            }

            return Ok(mediaItem);
        }

        // Book endpoints

        [HttpGet("books")]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var books = await _dbContext.Books
                                        .Include(b => b.MediaType)
                                        .ToListAsync();
            return Ok(books);
        }

        [HttpGet("books/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _dbContext.Books
                                       .Include(b => b.MediaType)  
                                       .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost("books")]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Books.ToListAsync());
        }

        [HttpPut("books/{id}")]
        public async Task<ActionResult<List<Book>>> UpdateBook(int id, Book updatedBook)
        {
            var dbBook = await _dbContext.Books.FindAsync(id);

            if (dbBook == null)
            {
                return NotFound();
            }

            dbBook.Title = updatedBook.Title;
            dbBook.Description = updatedBook.Description;
            dbBook.ImageUrl = updatedBook.ImageUrl;
            dbBook.Author = updatedBook.Author;
            dbBook.ISBN = updatedBook.ISBN;
            dbBook.PageCount = updatedBook.PageCount;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Books.ToListAsync());
        }

        [HttpDelete("books/{id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int id)
        {
            var dbBook = await _dbContext.Books.FindAsync(id);

            if (dbBook == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(dbBook);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Books.ToListAsync());
        }

        // DVD endpoints

        [HttpGet("dvds")]
        public async Task<ActionResult<List<DVD>>> GetAllDvds()
        {
            var dvds = await _dbContext.DVDs
                                      .Include(d => d.MediaType)
                                      .ToListAsync();
            return Ok(dvds);
        }

        [HttpGet("dvds/{id}")]
        public async Task<ActionResult<DVD>> GetDvd(int id)
        {
            var dvd = await _dbContext.DVDs
                                      .Include(d => d.MediaType)
                                      .FirstOrDefaultAsync(d => d.Id == id);

            if (dvd == null)
            {
                return NotFound();
            }

            return Ok(dvd);
        }

        [HttpPost("dvds")]
        public async Task<ActionResult<List<DVD>>> AddDvd(DVD dvd)
        {
            _dbContext.DVDs.Add(dvd);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.DVDs.ToListAsync());
        }

        [HttpPut("dvds/{id}")]
        public async Task<ActionResult<List<DVD>>> UpdateDvd(int id, DVD updatedDvd)
        {
            var dbDvd = await _dbContext.DVDs.FindAsync(id);

            if (dbDvd == null)
            {
                return NotFound();
            }

            dbDvd.Title = updatedDvd.Title;
            dbDvd.Description = updatedDvd.Description;
            dbDvd.ImageUrl = updatedDvd.ImageUrl;
            dbDvd.Director = updatedDvd.Director;
            dbDvd.DurationMinutes = updatedDvd.DurationMinutes;
            dbDvd.ReleaseYear = updatedDvd.ReleaseYear;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.DVDs.ToListAsync());
        }

        [HttpDelete("dvds/{id}")]
        public async Task<ActionResult<List<DVD>>> DeleteDvd(int id)
        {
            var dbDvd = await _dbContext.DVDs.FindAsync(id);

            if (dbDvd == null)
            {
                return NotFound();
            }

            _dbContext.DVDs.Remove(dbDvd);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.DVDs.ToListAsync());
        }

        // Game endpoints

        [HttpGet("games")]
        public async Task<ActionResult<List<Game>>> GetAllGames()
        {
            var games = await _dbContext.Games
                                       .Include(g => g.MediaType)
                                       .ToListAsync();
            return Ok(games);
        }

        [HttpGet("games/{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _dbContext.Games
                                       .Include(g => g.MediaType) 
                                       .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPost("games")]
        public async Task<ActionResult<List<Game>>> AddGame(Game game)
        {
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Games.ToListAsync());
        }

        [HttpPut("games/{id}")]
        public async Task<ActionResult<List<Game>>> UpdateGame(int id, Game updatedGame)
        {
            var dbGame = await _dbContext.Games.FindAsync(id);

            if (dbGame == null)
            {
                return NotFound();
            }

            dbGame.Title = updatedGame.Title;
            dbGame.Description = updatedGame.Description;
            dbGame.ImageUrl = updatedGame.ImageUrl;
            dbGame.Platform = updatedGame.Platform;
            dbGame.Genre = updatedGame.Genre;
            dbGame.AgeRating = updatedGame.AgeRating;
            dbGame.Developer = updatedGame.Developer;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Games.ToListAsync());
        }

        [HttpDelete("games/{id}")]
        public async Task<ActionResult<List<Game>>> DeleteGame(int id)
        {
            var dbGame = await _dbContext.Games.FindAsync(id);

            if (dbGame == null)
            {
                return NotFound();
            }

            _dbContext.Games.Remove(dbGame);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Games.ToListAsync());
        }

        // Journal endpoints

        [HttpGet("journals")]
        public async Task<ActionResult<List<Journal>>> GetAllJournals()
        {
            var journals = await _dbContext.Journals
                                          .Include(j => j.MediaType)
                                          .ToListAsync();

            return Ok(journals);
        }

        [HttpGet("journals/{id}")]
        public async Task<ActionResult<Journal>> GetJournal(int id)
        {
            var journal = await _dbContext.Journals
                                          .Include(j => j.MediaType)
                                          .FirstOrDefaultAsync(j => j.Id == id);

            if (journal == null)
            {
                return NotFound();
            }

            return Ok(journal);
        }

        [HttpPost("journals")]
        public async Task<ActionResult<List<Journal>>> AddJournal(Journal journal)
        {
            _dbContext.Journals.Add(journal);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Journals.ToListAsync());
        }

        [HttpPut("journals/{id}")]
        public async Task<ActionResult<List<Journal>>> UpdateJournal(int id, Journal updatedJournal)
        {
            var dbJournal = await _dbContext.Journals.FindAsync(id);

            if (dbJournal == null)
            {
                return NotFound();
            }

            dbJournal.Title = updatedJournal.Title;
            dbJournal.Description = updatedJournal.Description;
            dbJournal.ImageUrl = updatedJournal.ImageUrl;
            dbJournal.Publisher = updatedJournal.Publisher;
            dbJournal.IssueNumber = updatedJournal.IssueNumber;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Journals.ToListAsync());
        }

        [HttpDelete("journals/{id}")]
        public async Task<ActionResult<List<Journal>>> DeleteJournal(int id)
        {
            var dbJournal = await _dbContext.Journals.FindAsync(id);

            if (dbJournal == null)
            {
                return NotFound();
            }

            _dbContext.Journals.Remove(dbJournal);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Journals.ToListAsync());
        }
    }
}
