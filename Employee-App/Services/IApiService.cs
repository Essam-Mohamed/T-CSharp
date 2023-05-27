using Employee_App.model;

namespace Employee_App.Services
{
    internal interface IApiService
    {
        Task<List<Employee>> GetEmployeesData();
    }
}
