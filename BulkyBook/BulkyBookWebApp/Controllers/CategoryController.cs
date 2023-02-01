
// need this for ApplicationDbContext
using BulkyBookWeb.Data;
using BulkyBookWebApp.Models;
using Microsoft.AspNetCore.Mvc;



namespace BulkyBookWebApp.Controllers
{
    public class CategoryController : Controller
    {
        // you need this for Dependency injection
        private readonly ApplicationDbContext _db;

        // you need this for Dependency injection
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            // now we have access to the database, it's _db
        }

        public IActionResult Index()
        {
            // convert categories table in database to a list
            // you do no have to write a SQL SELECT statement to get all the categories from the database, just do this
            // IEnumerable<Category> is just a strongly typed datatype, we could've used var, but better to use IEnumerable<Category>
            IEnumerable<Category> objCategoryList = _db.Categories;

            Console.WriteLine(objCategoryList);

            // we can pass objCategoryList as an argument into View() so that it will be available in that particular View file
            return View();
        }
    }
}
