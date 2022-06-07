using System.ComponentModel.DataAnnotations;

namespace ToDoApp
{
    public enum UrgencyLevels
    {
        [Display(Name = "Low")]
        Low = 1,
        [Display(Name = "Medium")]
        Medium,
        [Display(Name = "High")]
        High
    }
}
