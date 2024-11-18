using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

public class JournalsController : Controller
{
    private readonly HttpClient _httpClient;
    Uri baseAddress = new Uri("http://localhost:5150/api");

    public JournalsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet("/journals")]
    public async Task<IActionResult> GetAllJournals()
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/media/journals");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var journals = JsonConvert.DeserializeObject<List<Journal>>(content);
            return View("Index", journals); 
        }
        return View("Error");
    }

    [HttpGet("/journals/{id}")]
    public async Task<IActionResult> GetJournal(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/journals/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var journal = JsonConvert.DeserializeObject<Journal>(content);
            return View("Details", journal); 
        }
        return View("Error");
    }

    [HttpGet("/journals/create")]
    public IActionResult CreateJournal()
    {
        return View("Create", new Journal());
    }

    [HttpPost("/journals/create")]
    public async Task<IActionResult> CreateJournal(Journal journal)
    {
        var content = new StringContent(JsonConvert.SerializeObject(journal), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/media/journals", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllJournals");
        }
        return View("Error");
    }

    [HttpGet("/journals/edit/{id}")]
    public async Task<IActionResult> EditJournal(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/journals/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var journal = JsonConvert.DeserializeObject<Journal>(content);
            return View("Edit", journal);
        }
        return View("Error");
    }

    [HttpPost("/journals/edit/{id}")]
    public async Task<IActionResult> EditJournal(Journal journal)
    {
        var content = new StringContent(JsonConvert.SerializeObject(journal), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(_httpClient.BaseAddress + "/media/journals", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllJournals");
        }
        return View("Error");
    }

    [HttpGet("/journals/delete/{id}")]
    public async Task<IActionResult> DeleteJournal(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/journals/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var journal = JsonConvert.DeserializeObject<Journal>(content);
            return View("Delete", journal); 
        }
        return View("Error"); 
    }

    [HttpPost("/journals/delete/{id}/confirm")]
    public async Task<IActionResult> DeleteJournalConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/media/journals/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllJournals"); 
        }
        return View("Error");
    }
}
