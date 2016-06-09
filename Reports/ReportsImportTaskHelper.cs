using System;
using System.Linq;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;

namespace SitefinityWebApp.Reports
{
    public class ReportsImportTaskHelper
    {
        private const string taskKey = "239CE594-5613-42EC-AC57-8E2B33B28065";

        public static void Schedule(bool runImmediately = false)
        {
            SchedulingManager manager = SchedulingManager.GetManager();

            var task = ReportsImportTaskHelper.GetTask();

            if (task == null)
            {
                ReportsImportScheduledTask newTask = new ReportsImportScheduledTask()
                {
                    Key = taskKey
                };

                if (runImmediately)
                {
                    newTask.ExecuteTime = DateTime.UtcNow.AddSeconds(-10);
                }
                else
                {
                    newTask.ExecuteTime = DateTime.UtcNow.AddSeconds(30 * 60);
                }

                manager.AddTask(newTask);
                manager.SaveChanges();
            }
            else
            {
                if (runImmediately)
                {
                    if (!task.IsRunning || task.Status == TaskStatus.Failed)
                    {
                        manager.DeleteTaskData(task);

                        ReportsImportScheduledTask newTask = new ReportsImportScheduledTask()
                        {
                            Key = taskKey,
                            ExecuteTime = DateTime.UtcNow.AddSeconds(-10),
                            LastExecutedTime = DateTime.UtcNow
                        };

                        manager.AddTask(newTask);
                        manager.SaveChanges();
                    }
                }
            }
        }

        public static ScheduledTaskData GetTask()
        {
            SchedulingManager manager = SchedulingManager.GetManager();

            ScheduledTaskData task = manager.GetTaskData()
                .Where(i => i.Key == taskKey)
                .SingleOrDefault();

            return task;
        }
    }
}