
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

        // this will be the GET endpoint for /Category/Index
        [HttpGet]
        public IActionResult Index()
        {

            // you can easily query the SQL database using FromSql(), it's that easy!      
            List<Category> objCategoryList = _db.Categories.FromSql($"SELECT * FROM Categories").ToList();

            // we can pass objCategoryList as an argument into View() so that it will be available in that particular View file
            return View(objCategoryList);
        }



        // this is for creating a Category (GET and POST)

        // this will be the endpoint for /Category/Create
        // this will be a GET action method
        // add [HttpGet] just to avoid possible errors, even though its assumed a GET request by default
        [HttpGet]
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



        // this will be for updating/editing a Category (GET and POST)

        // this will be the endpoint for /Category/Edit
        // GET
        // here we need the id of the Category so that we know which one to edit
        // add [HttpGet] just to avoid possible errors, even though its assumed a GET request by default
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            // check if id is not null and if it's not 0
            if(id == null || id == 0)
            {
                return NotFound();
            }

            // here we retrieve a single table row from the database

            // if we use .SingleOrDefault(), this returns a single row and will throw an exception if more than one row is returned, this method will return empty if no rows are found
            // var categoryFromDb = _db.Categories.SingleOrDefault(u=>u.Id==id);

            // if we use .Single(), this returns a single row and will throw an exception if no rows are found
            // var categoryFromDb = _db.Categories.Single(id);

            // if we use .FirstOrDefault(), this returns a single row, but if more than one row is found, it will only return the first one, not throwing an exception
            // var categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);

            // if we use .First(), this returns a single row and will throw an exception if no rows are found
            // var categoryFromDb = _db.Categories.First(id);

            // we will be using .Find(), you pass in the primary id and it will find the row that has that primary key
            var categoryFromDb = _db.Categories.Find(id);

            // check if nothing was returned from the database
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST
        // this is for when the form is submitted
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            // here we are implementing server-side validation

            // let's throw an error if the Name and DisplayOrder property the user typed are exactly the same
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // if they are the same, execute this code
                // create custom error, this method takes key-value pair
                ModelState.AddModelError("SameNameAndDisplayOrderErr", "Name and DisplayOrder cannot be the same");
            }


            // if the user submits an empty form, than an error will be thrown
            // so, we check if obj is valid by checking ModelState.IsValid
            if (ModelState.IsValid)
            {
                // if user input is valid, we can go ahead and update the Category

                // this is all we need to do to UPDATE a row in a table
                // _db is the database, and then we go into the Categories table, then we update it with the new obj
                // what happens here behind the scenes is that SQL matches up the primary key of obj against the rows in the database, once it finds a match, it updates that one with the new obj
                _db.Categories.Update(obj);

                // whenever we add, update or delete something in the database, we need to save the changes
                _db.SaveChanges();

                // after the form is posted and the category is updated, instead of returning the current View, which in this case would be /Category/Edit/{id}, we should redirect the user to the /Category/Index
                // the first argument is the action, second argument is the controller, so it should be RedirectToAction("Index", "Category"), but because we are already in the Category controller, we can omit it this time
                return RedirectToAction("Index");
            }
            else
            {
                // if user input is not valid, we just return the current view
                return View(obj);
            }

        }



        // this will be for deleting a Category (GET and POST)

        // this will be the endpoint for /Category/Edit
        // GET
        // here we need the id of the Category so that we know which one to delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            // check if id is not null and if it's not 0
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // retrieve Category from database using .Find(primaryKey)
            var categoryFromDb = _db.Categories.Find(id);

            // check if nothing was returned from the database
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            // return View with Category found in database
            return View(categoryFromDb);
        }

        // POST
        // this is for when the form is submitted
        [HttpPost]
        [ValidateAntiForgeryToken]
        // you actually can't have two methods that have the same name AND the same parameters in the same controller file, so we won't call this Delete(), we'll call it DeletePost()
        public IActionResult DeletePost(int? id)
        {

            // retrieve the Category from the database
            var singleCategory = _db.Categories.Find(id);

            // check if nothing was returned from database
            if(singleCategory == null)
            {
                return NotFound();
            }

            // this is all you need to do to delete a row from the database
            _db.Categories.Remove(singleCategory);

            // always need to save the change
            _db.SaveChanges();

            // after deleting, user should be redirected to /Category/Index page
            return RedirectToAction("Index");
        }

    }
}
