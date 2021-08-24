namespace WorkPlanning.API.Data.Context
{
    public interface IWorkPlanningDBSettings
    {
        string WorkPlanningCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
