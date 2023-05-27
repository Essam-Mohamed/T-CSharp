namespace Employee_App.model
{
    internal class EmployeAttendance
    {
        public string? EmployeeName { get; set; }
        public string? DeletedOn { get; set; }

        public List<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
