using System;
using System.Threading;
using Telerik.Sitefinity.Scheduling;

namespace SitefinityWebApp.Reports
{
    public class ReportsImportScheduledTask : ScheduledTask
    {
        public ReportsImportScheduledTask()
            : base()
        {
            this.Title = typeof(ReportsImportScheduledTask).Name;
        }

        public override void ExecuteTask()
        {
            this.ImportReports();

            this.RescheduleTask();
        }

        protected override void UpdateProgress(int progress, string message = "")
        {
            base.UpdateProgress(progress, message);
        }

        protected virtual void ImportReports()
        {
            int totalCount = 25;
            for (int i = 0; i < totalCount; i++)
            {
                this.CreateReportItem(i);
                // Simulate long running process execution
                Thread.Sleep(1000);

                this.UpdateProgress((int)(((float)i / totalCount) * 100),
                    "Creating items...");
            }

            this.UpdateProgress(99, "Saving items...");

            this.SaveChanges();

            // Simulate long running process execution
            Thread.Sleep(3000);
        }

        private void CreateReportItem(int index)
        {
        }

        private void SaveChanges()
        {
        }

        private void RescheduleTask()
        {
            SchedulingManager schedulingManager = SchedulingManager.GetManager();

            ReportsImportScheduledTask newTask = new ReportsImportScheduledTask()
            {
                Key = this.Key,
                ExecuteTime = DateTime.UtcNow.AddSeconds(30 * 60),
                LastExecutedTime = this.LastExecutedTime
            };

            schedulingManager.AddTask(newTask);
            SchedulingManager.RescheduleNextRun();
            schedulingManager.SaveChanges();
        }

        public override string TaskName
        {
            get
            {
                return typeof(ReportsImportScheduledTask).FullName;
            }
        }
    }
}