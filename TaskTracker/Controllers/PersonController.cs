using System.Collections.Generic;
using System.Web.Mvc;
using TaskTracker.DAL;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            ITaskTrackerDaoFactory factory = TaskTrackerDaoFactory.GetInstance();
            IPersonAccessComponent personAccessComponent = factory.GetPersonAccessComponent();
            IEnumerable<Person> persons = personAccessComponent.GetPersons();
            return View(persons);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            try
            {
                ITaskTrackerDaoFactory factory = TaskTrackerDaoFactory.GetInstance();
                IPersonAccessComponent personAccessComponent = factory.GetPersonAccessComponent();
                personAccessComponent.AddPerson(person);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ITaskTrackerDaoFactory factory = TaskTrackerDaoFactory.GetInstance();
            IPersonAccessComponent personAccessComponent = factory.GetPersonAccessComponent();
            var person = personAccessComponent.GetPerson(id);
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(int id, Person person)
        {
            try
            {
                ITaskTrackerDaoFactory factory = TaskTrackerDaoFactory.GetInstance();
                IPersonAccessComponent personAccessComponent = factory.GetPersonAccessComponent();
                personAccessComponent.UpdatePerson(person);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            ITaskTrackerDaoFactory factory = TaskTrackerDaoFactory.GetInstance();
            IPersonAccessComponent personAccessComponent = factory.GetPersonAccessComponent();
            var person = personAccessComponent.GetPerson(id);
            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ITaskTrackerDaoFactory factory = TaskTrackerDaoFactory.GetInstance();
                IPersonAccessComponent personAccessComponent = factory.GetPersonAccessComponent();
                personAccessComponent.DeletePerson(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
