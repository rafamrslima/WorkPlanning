using WorkPlanning.API.Data.Context;

namespace WorkPlanning.Data.Context
{
    public class WorkPlanningDBSettings : IWorkPlanningDBSettings
    {
        public string WorkPlanningCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    } 
}
