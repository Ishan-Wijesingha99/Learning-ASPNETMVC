
// this is where we connect to SQL database, this is where we use the connection string

// need this to access models
using BulkyBookWebApp.Models;

// we need Entity Framework Core for this, download the NuGet Package
using Microsoft.EntityFrameworkCore;


namespace BulkyBookWeb.Data;

public class ApplicationDbContext :DbContext
{
	// this is a constructor
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)	
	{

	}

    // for every model you create, you'll need to create a Db set in this file
	// remember, if the model name is Category, by convention, the table must be called Categories
	public DbSet<Category> Categories { get; set; }	
}
