using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

public class BooksController : Controller
{
    private readonly HttpClient _httpClient;
    Uri baseAddress = new Uri("http://localhost:5150/api");

    public BooksController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet("/books")]
    public async Task<IActionResult> GetAllBooks()
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/media/books");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(content);
            return View("Index", books);  
        }
        return View("Error");
    }


    [HttpGet("/books/{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/books/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(content);
            return View("Details", book);  
        }
        return View("Error");
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
        var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/media/books", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllBooks");  
        }
        return View("Error");
    }


    [HttpGet("/books/edit/{id}")]
    public async Task<IActionResult> EditBook(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/books/{id}");
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
        var response = await _httpClient.PutAsync(_httpClient.BaseAddress + "/media/books", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllBooks"); 
        }
        return View("Error");
    }


    [HttpGet("/books/delete/{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/books/{id}");
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
        var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/media/books/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllBooks");
        }

        return View("Error");
    }
}
