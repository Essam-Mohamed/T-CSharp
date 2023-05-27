using Employee_App.DTO;
using Employee_App.model;
using Employee_App.Services;
using Employee_App.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Employee_App
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var Services = new ServiceCollection();


            // Register the services and dependencies
            Services.AddScoped<IApiService, ApiService>();
            Services.AddScoped<IHtml, Html>();
            Services.AddScoped<IChartService, Chart>();


            // Build the service provider
            var ServiceProvider = Services.BuildServiceProvider();


            //Resolve from the service provider
            var ApiService = ServiceProvider.GetService<IApiService>();
            var HtmlService = ServiceProvider.GetService<IHtml>();
            var ChartService = ServiceProvider.GetService<IChartService>();


            // Get the employees data from the API
            var employees = await ApiService.GetEmployeesData();
            if (employees.Count != 0)
            {
                List<EmployeAttendance> EmployeeAttendances = new List<EmployeAttendance>();
                SetEmployeeAttendanceData(employees, EmployeeAttendances);

                List<EmployeeWithTotalWorksHours> employeeWithTotalHours = new List<EmployeeWithTotalWorksHours>();
                foreach (var employee in EmployeeAttendances)
                {
                    string name = employee.EmployeeName;
                    double sumHours = 0;
                    int totalHours;
                    foreach (var att in employee.Attendances)
                    {
                        DateTime startTime;
                        DateTime endTime;
                        DateTime.TryParse(att.StartDate, out startTime);
                        DateTime.TryParse(att.EndDate, out endTime);
                        double hours = (endTime - startTime).TotalHours;

                        sumHours += hours;
                    }
                    totalHours =(int)sumHours;
                    EmployeeWithTotalWorksHours emp = new EmployeeWithTotalWorksHours();
                    emp.EmployeeName = name;
                    emp.TotalWorkHours = totalHours;
                    employeeWithTotalHours.Add(emp);

                }
                SortEmployees(employeeWithTotalHours);

                HtmlService.GenerateHtmlTable(employeeWithTotalHours);

                ChartService.GeneratePieChart(employeeWithTotalHours);

            }



        }

        public static void SortEmployees(List<EmployeeWithTotalWorksHours> employeeWithTotalHours)
        {
            employeeWithTotalHours.Sort((emp1, emp2) => emp2.TotalWorkHours - emp1.TotalWorkHours);

        }
        public static void SetEmployeeAttendanceData(List<Employee> employees, List<EmployeAttendance> EmployeeAttendances)
        {
            foreach (var employee in employees)
            {
                string? name = employee.EmployeeName;
                var emp = EmployeeAttendances.Find(e => e.EmployeeName == name);
                Attendance attendance = new Attendance()
                {
                    Id=employee.Id,
                    StartDate = employee.StarTimeUtc,
                    EndDate = employee.EndTimeUtc,
                    EntryNotes = employee.EntryNotes
                };
                if (emp != null)
                {
                    emp.Attendances.Add(attendance);
                }
                else
                {
                    EmployeAttendance newEmployee = new EmployeAttendance()
                    {
                        EmployeeName = name,
                        DeletedOn = employee.DeletedOn,
                    };
                    newEmployee.Attendances.Add(attendance);
                    EmployeeAttendances.Add(newEmployee);
                }
            }
        }
    }
}