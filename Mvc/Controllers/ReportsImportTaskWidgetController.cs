using SitefinityWebApp.Reports;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Scheduling.Model;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "ReportsImportTaskWidget", Title = "ReportsImportTaskWidget", SectionName = "MvcWidgets")]
    public class ReportsImportTaskWidgetController : Controller
    {
        public ActionResult Index()
        {
            return View("Default");
        }

        public ActionResult StartTask()
        {
            ReportsImportTaskHelper.Schedule(true);

            return View("Default");
        }

        [ActionName("status")]
        public JsonResult GetStatus()
        {
            var task = ReportsImportTaskHelper.GetTask();

            ReportsImportStatus status = new ReportsImportStatus();
            status.CurrentProgress = task.Progress;
            status.StatusMessage = task.StatusMessage;

            status.LastExecutedTime = task.LastExecutedTime;
            status.NextExecuteTime = task.ExecuteTime;

            status.HasFailed = task.Status == TaskStatus.Failed;
            status.IsRunning = task.IsRunning;

            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}