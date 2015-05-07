using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskTracker.DAL.SQL_Server;

namespace TaskTracker.DAL
{
    public class SqlServerTaskTrackerDaoFactory:ITaskTrackerDaoFactory
    {
        private IPersonAccessComponent personAccessComponent = new SqlServerPersonAccessComponent();
        private ITaskAccessComponent taskAccessComponent = new SqlServerTaskAccessComponent();

        public IPersonAccessComponent GetPersonAccessComponent()
        {
            return personAccessComponent;
        }

        public ITaskAccessComponent GetTaskAccessComponent()
        {
            return taskAccessComponent;
        }
    }
}