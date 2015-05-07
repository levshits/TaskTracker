using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }

        public PersonViewModel()
        {
            
        }
        public PersonViewModel(Person person)
        {
            Id = person.Id;
            FullName = String.Format("{0} {1} {2}", person.LastName, person.FirstName, person.MiddleName);
        }
    }
}