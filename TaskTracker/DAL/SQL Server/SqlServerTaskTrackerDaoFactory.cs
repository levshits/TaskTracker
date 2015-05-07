namespace TaskTracker.DAL.SQL_Server
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