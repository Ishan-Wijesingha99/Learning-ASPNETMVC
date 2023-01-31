
// this is needed to access the [key] property in the class
using System.ComponentModel.DataAnnotations;



namespace BulkyBookWebApp.Models
{
    public class Category
    {
        // inside this Category class, we need to create a property for each column in the SQL table

        // every row will have an Id property (column value)
        // if you add [key] right on top of Id, it will automatically recognise Id as the primary key
        [Key]
        public int Id { get; set; }

        // every row will have a Name property (column value)
        // we also want to make Name a required property, so we just add [Required] right on top of Name
        [Required]
        public string Name { get; set; }

        // every row will have a DisplayOrder property (column value)
        public int DisplayOrder { get; set; }

        // every row will have a CreatedDateTime property (column value) which signifies the time it was created, just like MongoDB
        // to set a default value, we just do = DateTime.Now;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
