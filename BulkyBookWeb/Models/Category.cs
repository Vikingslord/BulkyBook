using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key] //Primary Key
        public long Id { get; set; }

        [Required] //Name cannot be NULL as it is (REQUIRED)
        public string Name { get; set; }

        [DisplayName("Display Order")] //Showing Display Order instead of DisplayOrder
        [Range(1, 100, ErrorMessage = "Display Order has to be in range of 1 to 100")] //Range :)
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now; //setting the current date
    }
}
