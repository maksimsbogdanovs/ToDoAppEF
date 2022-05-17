using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDoListModel
    {

        [Key]
        public int ToDoId { get; set; }

        [Required, MaxLength(5000)]
        [DisplayName("Things To-Do")]
        public string ToDoListItem { get; set; }

        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [DisplayName("Urgency level")]
        public string? Urgencylevel { get; set; }


    }
}
