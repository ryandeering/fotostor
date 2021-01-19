using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Server.Services;
using _4thYearProject.Shared.Models;
using _4thYearProject.Server.Components;

namespace _4thYearProject.Server.Pages
{
    public partial class EmployeeOverview : ComponentBase
    {


        public IEnumerable<Employee> Employees { get; set; }
        protected AddEmployeeDialog AddEmployeeDialog { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {

            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }


        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        public async Task AddEmployeeDialog_OnDialogClose()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            StateHasChanged();
        }



    }
}
