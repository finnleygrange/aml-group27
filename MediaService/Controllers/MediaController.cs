using MediaService.Data;
using MediaService.DTOs;
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

        [HttpGet]
        public async Task<ActionResult<List<MediaItem>>> GetAllMediaItems()
        {
            var mediaItems = await _dbContext.MediaItems.ToListAsync();

            var mediaDtos = mediaItems.Select(m => new MediaItemDto
            {
                Title = m.Title,
                Description = m.Description
            }).ToList();

            return Ok(mediaItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MediaItem>> GetMediaItem(int id)
        {
            var mediaItem = await _dbContext.MediaItems.FindAsync(id);

            if (mediaItem == null) { return NotFound(); }

            var mediaDto = new MediaItemDto
            {
                Title = mediaItem.Title,
                Description = mediaItem.Description
            };

            return Ok(mediaItem);
        }

        [HttpPost]
        public async Task<ActionResult<List<MediaItem>>> AddMediaItem(MediaItemDto mediaItemDto)
        {
            var mediaItem = new MediaItem
            {
                Title = mediaItemDto.Title,
                Description = mediaItemDto.Description
            };

            _dbContext.MediaItems.Add(mediaItem);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.MediaItems.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<MediaItem>>> UpdateMediaItem(MediaItem updatedMediaItem)
        {
            var dbMediaItem = await _dbContext.MediaItems.FindAsync(updatedMediaItem.Id);

            if (dbMediaItem == null) { return NotFound(); }

            dbMediaItem.Title = updatedMediaItem.Title;
            dbMediaItem.Description = updatedMediaItem.Description;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.MediaItems.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<MediaItem>>> DeleteMediaItem(int id)
        {
            var dbMediaItem = await _dbContext.MediaItems.FindAsync(id);

            if (dbMediaItem == null) { return NotFound(); }

            _dbContext.MediaItems.Remove(dbMediaItem);

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.MediaItems.ToListAsync());
        }



    }

}
