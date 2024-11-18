using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class MediaController : Controller
{
    private readonly HttpClient _httpClient;
    Uri baseAddress = new Uri("http://localhost:5150/api");

    public MediaController(HttpClient httpClient)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet("/media")]
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/media");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var mediaItems = JsonConvert.DeserializeObject<List<MediaItem>>(content);
            return View(mediaItems);
        }
        return View("Error");
    }

    [HttpGet("/media/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var mediaItem = JsonConvert.DeserializeObject<MediaItem>(content);
            return View(mediaItem);
        }
        return View("Error");
    }

    [HttpGet("/media/create")]
    public IActionResult Create()
    {
        return View(new MediaItem());
    }

    [HttpPost("/media/create")]
    public async Task<IActionResult> Create(MediaItem mediaItem)
    {
        var content = new StringContent(JsonConvert.SerializeObject(mediaItem), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/media", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View("Error");
    }

    [HttpGet("/media/edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var mediaItem = JsonConvert.DeserializeObject<MediaItem>(content);
            return View(mediaItem);
        }
        return View("Error");
    }

    [HttpPost("/media/edit/{id}")]
    public async Task<IActionResult> Edit(MediaItem mediaItem)
    {
        var content = new StringContent(JsonConvert.SerializeObject(mediaItem), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(_httpClient.BaseAddress + "/media", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View("Error");
    }

    [HttpGet("/media/delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var mediaItem = JsonConvert.DeserializeObject<MediaItem>(content);
            return View(mediaItem);
        }
        return View("Error");
    }

    [HttpPost("/media/delete/{id}/confirm")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/media/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View("Error");
    }



}
