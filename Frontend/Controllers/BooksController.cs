using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

public class BooksController : Controller
{
    private readonly HttpClient _mediaHttpClient;
    private readonly HttpClient _transactionHttpClient;

    Uri mediaServiceBaseAddress = new Uri("http://localhost:5150/api");
    Uri transactionServiceBaseAddress = new Uri("http://localhost:5151/api");  // Assuming TransactionService runs on port 5151

    public BooksController(HttpClient mediaHttpClient, HttpClient transactionHttpClient)
    {
        _mediaHttpClient = mediaHttpClient;
        _transactionHttpClient = transactionHttpClient;
        _mediaHttpClient.BaseAddress = mediaServiceBaseAddress;
        _transactionHttpClient.BaseAddress = transactionServiceBaseAddress;
    }

    [HttpGet("/books")]
    public async Task<IActionResult> GetAllBooks(string searchQuery)
    {
        var response = await _mediaHttpClient.GetAsync(_mediaHttpClient.BaseAddress + "/media/books");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(content);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                ViewData["SearchQuery"] = searchQuery;
                books = books.Where(b =>
                    (b.Title != null && b.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) ||
                    (b.Description != null && b.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            return View("Index", books);
        }
        return View("Error");
    }

    [HttpGet("/books/details/{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var response = await _mediaHttpClient.GetAsync(_mediaHttpClient.BaseAddress + $"/media/books/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(content);
            return View("Details", book);
        }
        return View("Error");
    }

    [HttpPost("/books/borrow/{id}")]
    public async Task<IActionResult> BorrowBook(int id)
    {
        // Step 1: Call TransactionService to borrow the book
        var response = await _transactionHttpClient.PostAsync(
            _transactionHttpClient.BaseAddress + $"/transactions/borrow?mediaItemId={id}",
            null);  // No body, just the mediaItemId as a query parameter

        // Step 2: Handle the result
        if (response.IsSuccessStatusCode)
        {
            TempData["Message"] = "Book borrowed successfully.";
        }
        else
        {
            TempData["Message"] = "Failed to borrow the book. It might already be borrowed.";
        }

        // Step 3: Refresh the book details page to update the status
        return RedirectToAction("GetBook", new { id });
    }

    [HttpGet("/books/create")]
    public IActionResult CreateBook()
    {
        return View("Create", new Book());
    }

    [HttpPost("/books/create")]
    public async Task<IActionResult> CreateBook(Book book)
    {
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
        var response = await _mediaHttpClient.PostAsync(_mediaHttpClient.BaseAddress + "/media/books", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllBooks");
        }
        return View("Error");
    }

    [HttpGet("/books/edit/{id}")]
    public async Task<IActionResult> EditBook(int id)
    {
        var response = await _mediaHttpClient.GetAsync(_mediaHttpClient.BaseAddress + $"/media/books/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(content);
            return View("Edit", book);
        }
        return View("Error");
    }

    [HttpPost("/books/edit/{id}")]
    public async Task<IActionResult> EditBook(Book book)
    {
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
        var response = await _mediaHttpClient.PutAsync(_mediaHttpClient.BaseAddress + "/media/books", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllBooks");
        }
        return View("Error");
    }

    [HttpGet("/books/delete/{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var response = await _mediaHttpClient.GetAsync(_mediaHttpClient.BaseAddress + $"/media/books/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(content);
            return View("Delete", book);
        }
        return View("Error");
    }

    [HttpPost("/books/delete/{id}/confirm")]
    public async Task<IActionResult> DeleteBookConfirmed(int id)
    {
        var response = await _mediaHttpClient.DeleteAsync(_mediaHttpClient.BaseAddress + $"/media/books/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllBooks");
        }

        return View("Error");
    }
}
