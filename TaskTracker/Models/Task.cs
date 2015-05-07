using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Volume")]
        public float Volume { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End name")]
        public DateTime? EndDate { get; set; }
        [Required]
        [Display(Name = "Status")]
        public TaskStatus Status { get; set; }
        public int? ExecutorId { get; set; }
        [Display(Name = "Executor")]
        public Person Executor { get; set; }
    }
}