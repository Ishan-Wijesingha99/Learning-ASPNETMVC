
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

        // this will be a POST action method
        // this is for when the form is submitted
        // we need to write [HttpPost] to access POST methods
        [HttpPost]
        // this is added just to prevent a certain hack
        [ValidateAntiForgeryToken]
        // the parameter is of type Category and is named obj
        public IActionResult Create(Category obj)
        {
            // here we are implementing server-side validation
           
            // let's throw an error if the Name and DisplayOrder property the user typed are exactly the same
            if(obj.Name == obj.DisplayOrder.ToString()) 
            {
                // if they are the same, execute this code
                // create custom error, this method takes key-value pair
                ModelState.AddModelError("SameNameAndDisplayOrderErr", "Name and DisplayOrder cannot be the same");
            }


            // if the user submits an empty form, than an error will be thrown
            // so, we check if obj is valid by checking ModelState.IsValid
            if(ModelState.IsValid)
            {
                // if user input is valid, we can go ahead and add the new Category

                // this is all we need to do to add a row to a database table
                // _db is the database, and then we go into the Categories table, and we add the object which was the argument passed into this method
                _db.Categories.Add(obj);

                // whenever we add, update or delete something in the database, we need to save the changes
                _db.SaveChanges();

                // after the form is posted and the new category is added to the database, instead of returning the current View, which in this case would be /Category/Create, we should redirect the user to the /Category/Index
                // the first argument is the action, second argument is the controller, so it should be RedirectToAction("Index", "Category"), but because we are already in the Category controller, we can omit it this time
                return RedirectToAction("Index");
            } else
            {
                // if user input is not valid, we just return the current view
                return View(obj);
            }

            
        }
    }
}
