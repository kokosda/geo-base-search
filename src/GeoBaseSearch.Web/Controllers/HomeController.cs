using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using GeoBaseSearch.Web.Models;

namespace GeoBaseSearch.Web.Controllers;

public class HomeController : Controller
{
	private readonly IConfiguration _configuration;

	public HomeController(IConfiguration configuration)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
	}

	public IActionResult Index()
	{
		var appSettings = new AppSettingsModel
		{
			BaseApiUrl = _configuration["BaseApiUrl"]
		};

		var result = new HomeViewModel { AppSettingsString = JsonSerializer.Serialize(appSettings) };
		return View(result);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
	}
}