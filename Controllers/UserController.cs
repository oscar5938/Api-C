using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers{
    public class UserController : Controller{
//[Route("[controller]")]

    //private readonly IServices<Users> _categoryService;
    private readonly DataContext _context;

    public UserController(/*IServices<Users> categoryService,*/ DataContext context)
    {
      //  _categoryService = categoryService;
        _context = context;
    }

    // GET /category/GetByProfile/{profileId}
    //[HttpGet("GetByUser/{profileId}")]
    public async Task<IActionResult> Index()
    {
        var category = await _context.Users.GetObjet();
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
}
}