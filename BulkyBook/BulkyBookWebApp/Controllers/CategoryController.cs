
// need this for ApplicationDbContext
using BulkyBookWeb.Data;
using BulkyBookWebApp.Models;

using Microsoft.AspNetCore.Mvc;

// need this to use FromSql()
using Microsoft.EntityFrameworkCore;



namespace BulkyBookWebApp.Controllers
{
    public class CategoryController : Controller
    {
        // you need this for Dependency injection
        // you need this to access the SQL database, which is _db
        private readonly ApplicationDbContext _db;

        // you need this for Dependency injection
        // you need this to access the SQL database, which is _db
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            // now we have access to the database, it's _db
        }

        // this will be the endpoint for /Category/Index
        public IActionResult Index()
        {

            // you can easily query the SQL database using FromSql(), it's that easy!      
            List<Category> objCategoryList = _db.Categories.FromSql($"SELECT * FROM Categories").ToList();

            // we can pass objCategoryList as an argument into View() so that it will be available in that particular View file
            return View(objCategoryList);
        }

        // this will be the endpoint for /Category/Create
        // this will be a GET action method
        public IActionResult Create()
        {


            return View();
        }
    }
}
