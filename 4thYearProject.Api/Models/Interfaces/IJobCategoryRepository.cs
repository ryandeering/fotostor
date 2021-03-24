namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;
    using System.Collections.Generic;

    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();

        JobCategory GetJobCategoryById(int jobCategoryId);
    }
}
