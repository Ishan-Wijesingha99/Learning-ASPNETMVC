// this is where the website is built

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// here we create an app, just like how we create an express app
var app = builder.Build();



// Configure the HTTP request pipeline.
// the .NET Core pipeline just refers to all the middleware that is added to a request
// when a browser sends a http request to the server, it must first pass through this "pipeline"
// middleware is executed on the request, and once all the middleware has been executed, the request reaches the server
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// all of these are middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// this code here is responsible for routing in MVC

// model - for every database you have, there will be a model
// all the tables in your database will have a C# class file each, and all the properties of that C# class file will be the columns of the table

// view - this is just the user interface (HTML and CSS)

// the model and view never interact directly, they only interact via the controller

// controller - the controller is what receives http requests from the client, the controller also sends back http responses to the client
// this is usually what happens...
// 1 - controller receives http request from client
// 2 - based on the specific request, the controller interacts with the model and gets the specific data it needs from the model
// 3 - controller than sends that specific data to the view, the view sends back specific files/UI based on the data it was given
// 4 - once controller gets the files/UI from the view, it sends a http response to the client based off those files/UI 

// examples of routing in ASP.NET MVC
// https://localhost:4000/Category/Index/3
// https://localhost:4000/{controller}/{action}/{id}
// first thing after / will always be the controller
// controller and action are not optional, id is, we have set the default values to be "Home" and "Index" respectively
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
