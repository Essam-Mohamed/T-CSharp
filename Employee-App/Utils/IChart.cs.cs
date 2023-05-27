using Employee_App.DTO;

namespace Employee_App.Utils
{
    internal interface IChartService
    {
        public void GeneratePieChart(List<EmployeeWithTotalWorksHours> employees);
    }
}
