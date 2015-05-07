using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace TaskTracker.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public String FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public String LastName { get; set; }
        [Display(Name = "Middle name")]
        public String MiddleName { get; set; }
    }
}