using MediaService.Controllers;
using MediaService.Data;
using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class MediaControllerTests
    {
        private MediaServiceDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<MediaServiceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new MediaServiceDbContext(options);
        }

        [Fact]
        public async Task GetAllMediaItems_ReturnsOk_WhenItemsExist()
        {
            var dbContext = GetInMemoryDbContext();
            dbContext.MediaItems.Add(new MediaItem
            {
                Id = 1,
                Title = "Test Media",           
                Author = "Test Author",         
                Description = "Test Description", 
                Genre = "Test Genre",           
                MediaType = new MediaType { Name = "Book" }
            });
            await dbContext.SaveChangesAsync();

            var controller = new MediaController(dbContext);

            var result = await controller.GetAllMediaItems();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var mediaItems = Assert.IsType<List<MediaItem>>(okResult.Value); 
            Assert.NotEmpty(mediaItems);
        }



        [Fact]
        public async Task GetMediaItem_ReturnsMediaItem_WhenIdExists()
        {
            var dbContext = GetInMemoryDbContext();
            dbContext.MediaItems.Add(new MediaItem
            {
                Id = 1,
                Title = "Test Media",
                Author = "Test Author",      
                Description = "Test Description", 
                Genre = "Test Genre",       
                MediaType = new MediaType { Name = "Book" }
            });
            await dbContext.SaveChangesAsync();

            var controller = new MediaController(dbContext);

            var result = await controller.GetMediaItem(1);

            Assert.NotNull(result);
            Assert.Equal(200, (result.Result as OkObjectResult).StatusCode);
        }


        [Fact]
        public async Task GetMediaItem_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var dbContext = GetInMemoryDbContext();
            var controller = new MediaController(dbContext);

            var result = await controller.GetMediaItem(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddMediaItem_CreatesNewMediaItem()
        {
            var dbContext = GetInMemoryDbContext();
            var controller = new MediaController(dbContext);
            var newMedia = new Book
            {
                Title = "New Media",
                Author = "New Author",
                Description = "Description for new media",
                Genre = "New Genre",
                PageCount = 200,
                ISBN = "978-3-16-148410-0",
                MediaTypeId = 1

            };

            var result = await controller.AddBook(newMedia); 

            Assert.IsType<OkObjectResult>(result.Result);
            var books = await dbContext.Books.ToListAsync();
            Assert.Single(books); 
            Assert.Equal("New Media", books[0].Title);
        }


        [Fact]
        public async Task UpdateBook_UpdatesExistingBook()
        {
            var dbContext = GetInMemoryDbContext();
            var book = new Book
            {
                Id = 1,
                Title = "Old Book Title",
                Author = "Old Author",
                Description = "Old Description",
                Genre = "Old Genre",
                ISBN = "1234567890",
                PageCount = 100
            };
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            var updatedBook = new Book
            {
                Id = 1,
                Title = "Updated Book Title", 
                Author = "Updated Author",
                Description = "Updated Description",
                Genre = "Updated Genre",
                ISBN = "0987654321",
                PageCount = 200
            };
            var controller = new MediaController(dbContext);

            var result = await controller.UpdateBook(1, updatedBook); 

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var bookFromDb = await dbContext.Books.FindAsync(1);
            Assert.Equal("Updated Book Title", bookFromDb?.Title); 
            Assert.Equal("Updated Author", bookFromDb?.Author);
        }


        [Fact]
        public async Task UpdateBook_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var dbContext = GetInMemoryDbContext();
            var updatedBook = new Book
            {
                Id = 99, 
                Title = "Updated Book Title",
                Author = "Updated Author",
                Description = "Updated Description",
                Genre = "Updated Genre",
                ISBN = "0987654321",
                PageCount = 200
            };
            var controller = new MediaController(dbContext);

            var result = await controller.UpdateBook(99, updatedBook); 

            Assert.IsType<NotFoundResult>(result.Result); 
        }

        [Fact]
        public async Task DeleteBook_DeletesExistingBook()
        {
            var dbContext = GetInMemoryDbContext();
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Author = "Test Author",
                Description = "Test Description",
                Genre = "Test Genre",
                ISBN = "1234567890",
                PageCount = 100
            };
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            var controller = new MediaController(dbContext);

            var result = await controller.DeleteBook(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var deletedBook = await dbContext.Books.FindAsync(1);
            Assert.Null(deletedBook);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var dbContext = GetInMemoryDbContext();
            var controller = new MediaController(dbContext);

            var result = await controller.DeleteBook(99);

            Assert.IsType<NotFoundResult>(result.Result); 
        }
    }
}
