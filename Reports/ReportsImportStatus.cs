namespace SitefinityWebApp.Reports
{
    public class ReportsImportStatus
    {
        public int CurrentProgress { get; set; }

        public string StatusMessage { get; set; }

        public bool HasFailed { get; set; }

        public bool IsRunning { get; set; }

        public System.DateTime? LastExecutedTime { get; set; }

        public System.DateTime NextExecuteTime { get; set; }
    }
}