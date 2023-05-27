namespace Employee_App.DTO
{
    internal class EmployeeWithTotalWorksHours //: IComparable
    {
        public string? EmployeeName { get; set; }
        public int TotalWorkHours { get; set; }

        //public int CompareTo(object? obj)
        //{
        //    EmployeeWithTotalWorksHours emp = obj as EmployeeWithTotalWorksHours;
        //    if (emp is null) return 1;
        //    return TotalWorkHours.CompareTo(emp.TotalWorkHours);
        //}
    }
}
