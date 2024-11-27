using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HybridCacheTest.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly SomeDataService _someDataService;
    
    

    public IndexModel(ILogger<IndexModel> logger, SomeDataService someDataService)
    {
        _logger = logger;
        _someDataService = someDataService;
    }

    public async Task OnGet(int id, CancellationToken cancellation)
    {
        Stopwatch stopwatch = Stopwatch.StartNew(); 
       var data = await _someDataService.GetSomeData(id, cancellation);
       ViewData["Data"] = data;
       stopwatch.Stop();    
       TimeSpan timeSpan = stopwatch.Elapsed;
       ViewData["Time"] = timeSpan.TotalMilliseconds;
    }
}