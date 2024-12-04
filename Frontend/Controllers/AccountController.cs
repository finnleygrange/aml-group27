using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TransactionService.Models;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly Uri _baseAddress = new Uri("http://localhost:5182/api");

    public AccountController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = _baseAddress;
    }
}
