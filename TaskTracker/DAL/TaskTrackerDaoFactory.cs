using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTracker.DAL
{
    public abstract class TaskTrackerDaoFactory:ITaskTrackerDaoFactory
    {
        private static ITaskTrackerDaoFactory instance = new SqlServerTaskTrackerDaoFactory();
        private TaskTrackerDaoFactory(){}

        public static ITaskTrackerDaoFactory GetInstance()
        {
            //Edit for adding new data sources
            return instance;
        }

        public abstract IPersonAccessComponent GetPersonAccessComponent();
        
        public abstract ITaskAccessComponent GetTaskAccessComponent();

    }
}