using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionService.Controllers;
using TransactionService.Data;
using TransactionService.Models;

namespace Tests
{
    public class TransactionControllerTests
    {
        private readonly DbContextOptions<TransactionDbContext> _dbContextOptions;

        public TransactionControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TransactionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;
        }

        private TransactionDbContext GetInMemoryDbContext()
        {
            return new TransactionDbContext(_dbContextOptions);
        }

        [Fact]
        public void Borrow_ReturnsOk_WhenBorrowSucceeds()
        {
            int mediaItemId = 1; 
            var dbContext = GetInMemoryDbContext();  
            var transactionService = new TransactionService.Services.TransactionService(dbContext);
            var controller = new TransactionsController(transactionService);

            var existingTransaction = dbContext.Transactions
                .FirstOrDefault(t => t.MediaItemId == mediaItemId);

            var result = controller.Borrow(mediaItemId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Media item borrowed successfully.", okResult.Value);

            var transaction = dbContext.Transactions
                .FirstOrDefault(t => t.MediaItemId == mediaItemId);

            Assert.NotNull(transaction); 
            Assert.Equal(mediaItemId, transaction.MediaItemId); 
            Assert.NotNull(transaction.BorrowDate); 
            Assert.Null(transaction.ReturnDate); 
            Assert.Equal("Borrowed", transaction.Status);
        }

        [Fact]
        public void Borrow_ReturnsBadRequest_WhenMediaItemAlreadyBorrowed()
        {
            int mediaItemId = 1;
            var dbContext = GetInMemoryDbContext();  
            var transactionService = new TransactionService.Services.TransactionService(dbContext);
            var controller = new TransactionsController(transactionService);

            dbContext.Transactions.Add(new Transaction
            {
                MediaItemId = mediaItemId,
                BorrowDate = DateTime.Now.AddDays(-7),
                DueDate = DateTime.Now.AddDays(7),
                Status = "Borrowed",
                ReturnDate = null
            });
            dbContext.SaveChanges();

            var result = controller.Borrow(mediaItemId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to borrow media.", badRequestResult.Value);
        }


        [Fact]
        public void Return_ReturnsOk_WhenReturnSucceeds()
        {
            int transactionId = 1;
            var dbContext = GetInMemoryDbContext();  
            var transactionService = new TransactionService.Services.TransactionService(dbContext);
            var controller = new TransactionsController(transactionService);

            dbContext.Transactions.Add(new Transaction
            {
                Id = transactionId,
                MediaItemId = 1,
                BorrowDate = DateTime.Now.AddDays(-7),
                DueDate = DateTime.Now.AddDays(7),
                Status = "Borrowed",
                ReturnDate = null 
            });
            dbContext.SaveChanges();

            var result = controller.Return(transactionId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Media item returned successfully.", okResult.Value);

            var transaction = dbContext.Transactions.FirstOrDefault(t => t.Id == transactionId);

            Assert.NotNull(transaction.ReturnDate); 
            Assert.Equal("Returned", transaction.Status); 
        }

        [Fact]
        public void Return_ReturnsBadRequest_WhenReturnFails()
        {
            int transactionId = 1;
            var dbContext = GetInMemoryDbContext();  
            var transactionService = new TransactionService.Services.TransactionService(dbContext);
            var controller = new TransactionsController(transactionService);

            dbContext.Transactions.Add(new Transaction
            {
                Id = transactionId,
                MediaItemId = 1,
                BorrowDate = DateTime.Now.AddDays(-7),
                DueDate = DateTime.Now.AddDays(7),
                Status = "Returned",
                ReturnDate = DateTime.Now 
            });
            dbContext.SaveChanges();

            var result = controller.Return(transactionId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to return media.", badRequestResult.Value);
        }


        [Fact]
        public void Renew_ReturnsOk_WhenRenewSucceeds()
        {
            int transactionId = 1;
            var dbContext = GetInMemoryDbContext();  
            var transactionService = new TransactionService.Services.TransactionService(dbContext);
            var controller = new TransactionsController(transactionService);

            dbContext.Transactions.Add(new Transaction
            {
                Id = transactionId,
                MediaItemId = 1,
                BorrowDate = DateTime.Now.AddDays(-7),
                DueDate = DateTime.Now.AddDays(7),
                Status = "Borrowed",
                ReturnDate = null 
            });
            dbContext.SaveChanges();

            var result = controller.Renew(transactionId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Media item renewed successfully.", okResult.Value);

            var transaction = dbContext.Transactions.FirstOrDefault(t => t.Id == transactionId);

            Assert.NotNull(transaction.DueDate); 
            Assert.Equal(DateTime.Now.AddDays(21).Date, transaction.DueDate.Date); 
            Assert.Equal("Renewed", transaction.Status); 
        }

        [Fact]
        public void Renew_ReturnsBadRequest_WhenRenewFails()
        {
            int transactionId = 1;
            var dbContext = GetInMemoryDbContext();  
            var transactionService = new TransactionService.Services.TransactionService(dbContext);
            var controller = new TransactionsController(transactionService);

            dbContext.Transactions.Add(new Transaction
            {
                Id = transactionId,
                MediaItemId = 1,
                BorrowDate = DateTime.Now.AddDays(-7),
                DueDate = DateTime.Now.AddDays(7),
                Status = "Returned",
                ReturnDate = DateTime.Now 
            });
            dbContext.SaveChanges();

            var result = controller.Renew(transactionId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to renew media.", badRequestResult.Value); 
        }


        [Fact]
        public void GetAllTransactions_ReturnsOk_WithTransactions()
        {
            var dbContext = GetInMemoryDbContext();  
            var transactionService = new TransactionService.Services.TransactionService(dbContext);
            var controller = new TransactionsController(transactionService);

            dbContext.Transactions.Add(new Transaction { Id = 1, MediaItemId = 1, Status = "Borrowed", BorrowDate = DateTime.Now.AddDays(-7), DueDate = DateTime.Now.AddDays(7), ReturnDate = null });
            dbContext.Transactions.Add(new Transaction { Id = 2, MediaItemId = 2, Status = "Returned", BorrowDate = DateTime.Now.AddDays(-10), DueDate = DateTime.Now.AddDays(4), ReturnDate = DateTime.Now });
            dbContext.SaveChanges();

            var result = controller.GetAllTransactions();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var transactions = Assert.IsType<List<Transaction>>(okResult.Value);
            Assert.Equal(2, transactions.Count); 

            Assert.Contains(transactions, t => t.Status == "Borrowed");
            Assert.Contains(transactions, t => t.Status == "Returned");
        }

        [Fact]
        public void GetAllTransactions_ReturnsOk_WithNoTransactions()
        {
            var dbContext = GetInMemoryDbContext();
            var transactionService = new TransactionService.Services.TransactionService(dbContext);
            var controller = new TransactionsController(transactionService);

            var result = controller.GetAllTransactions();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var transactions = Assert.IsType<List<Transaction>>(okResult.Value);
            Assert.Empty(transactions);
        }
    }
}
