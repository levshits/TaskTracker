using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.DAL
{
    public interface IPersonAccessComponent
    {
        IEnumerable<Person> GetPersons();
        Person GetPerson(int id);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(int id);
    }
}
