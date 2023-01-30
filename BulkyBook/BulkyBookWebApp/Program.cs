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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
