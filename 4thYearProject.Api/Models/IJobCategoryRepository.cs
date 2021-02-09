using _4thYearProject.Shared.Models;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models
{
    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();
        JobCategory GetJobCategoryById(int jobCategoryId);
    }
}
