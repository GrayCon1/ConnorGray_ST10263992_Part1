using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10263992.Models;

namespace ST10263992.Pages;

public class MyWorkModel : PageModel
{
    private readonly ILogger<MyWorkModel> _logger;

    public MyWorkModel(ILogger<MyWorkModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        ViewData["Products"] = ProductTable.GetAllProducts();
    }
}

