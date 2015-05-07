namespace TaskTracker.DAL
{
    public interface ITaskTrackerDaoFactory
    {
        IPersonAccessComponent GetPersonAccessComponent();
        ITaskAccessComponent GetTaskAccessComponent();
    }
}
