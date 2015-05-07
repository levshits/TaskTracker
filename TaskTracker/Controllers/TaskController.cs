using System.Collections.Generic;
using System.Web.Mvc;
using TaskTracker.DAL;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult Index()
        {
            var factory = TaskTrackerDaoFactory.GetInstance();
            var taskAccessComponent = factory.GetTaskAccessComponent();
            var tasks = taskAccessComponent.GetTasks();
            return View(tasks);
        }

        public ActionResult Create()
        {
            var factory = TaskTrackerDaoFactory.GetInstance();
            var personAccessComponent = factory.GetPersonAccessComponent();
            var persons = personAccessComponent.GetPersons();
            var possiblePersons = new List<PersonViewModel>();
            foreach (var person in persons)
            {
                possiblePersons.Add(new PersonViewModel(person));
            }
            ViewBag.Persons = possiblePersons;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            try
            {
                var factory = TaskTrackerDaoFactory.GetInstance();
                var taskAccessComponent = factory.GetTaskAccessComponent();
                if (task.Executor != null)
                {
                    task.ExecutorId = task.Executor.Id;
                }
                taskAccessComponent.AddTask(task);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var factory = TaskTrackerDaoFactory.GetInstance();
            var taskAccessComponent = factory.GetTaskAccessComponent();
            var task = taskAccessComponent.GetTask(id);
            var personAccessComponent = factory.GetPersonAccessComponent();
            var persons = personAccessComponent.GetPersons();
            var possiblePersons = new List<PersonViewModel>();
            foreach (var person in persons)
            {
                possiblePersons.Add(new PersonViewModel(person));
            }
            ViewBag.Persons = possiblePersons;
            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(int id, Task task)
        {
            try
            {
                var factory = TaskTrackerDaoFactory.GetInstance();
                var taskAccessComponent = factory.GetTaskAccessComponent();
                taskAccessComponent.UpdateTask(task);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var factory = TaskTrackerDaoFactory.GetInstance();
            var taskAccessComponent = factory.GetTaskAccessComponent();
            var task = taskAccessComponent.GetTask(id);
            return View(task);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var factory = TaskTrackerDaoFactory.GetInstance();
                var taskAccessComponent = factory.GetTaskAccessComponent();
                taskAccessComponent.DeleteTask(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
