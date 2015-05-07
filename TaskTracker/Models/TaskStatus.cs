using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public enum TaskStatus
    {
        [Display(Name = "Not started")]
        NotStarted = 0,
        [Display(Name = "Process")]
        Process = 1,
        [Display(Name = "Finished")]
        Finished = 2,
        [Display(Name = "Delayed")]
        Delayed = 3
    }
}