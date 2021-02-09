using _4thYearProject.Server.Services;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Pages
{
    public partial class EmployeeDetail : ComponentBase
    {

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
