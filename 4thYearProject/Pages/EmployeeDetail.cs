using System.Threading.Tasks;
using _4thYearProject.Shared.Models;
using _4thYearProject.Server.Services;
using Microsoft.AspNetCore.Components;

namespace _4thYearProject.Server.Pages
{
    public partial class EmployeeDetail : ComponentBase{ 

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {

            Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
        }



}
}
