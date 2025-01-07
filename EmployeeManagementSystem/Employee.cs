using System;

namespace EmployeeManagementSystem
{
    public class LocalEmployee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string EGN { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public int RemainingLeaveDays { get; set; }
    }

    public class ArchivedEmployee
    {
        public int ArchiveId { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string EGN { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public DateTime ArchivedDate { get; set; }
    }

}
