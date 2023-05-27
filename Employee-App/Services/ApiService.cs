using Employee_App.model;
using System.Text.Json;

namespace Employee_App.Services
{
    internal class ApiService : IApiService
    {
        private readonly string apiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";


        public async Task<List<Employee>> GetEmployeesData()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(data)??new List<Employee>();
                return employees;
            }
            return new List<Employee>();

        }
    }
}
