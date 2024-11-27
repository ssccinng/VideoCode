using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace HybirdCacheVideoTest.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DataService _dataService;

    public IndexModel(ILogger<IndexModel> logger, DataService dataService)
    {
        _logger = logger;
        _dataService = dataService;
    }

    public async Task OnGet(int id)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var datas = await _dataService.GetSomeData(id);
        stopwatch.Stop();
        ViewData["Time"] = stopwatch.ElapsedMilliseconds;
        ViewData["Data"] = string.Join(",", datas.Select(d => d.ToString()));
        
        
    }
}