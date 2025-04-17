using Career_Hub.Dao;
using Career_Hub.My_Exception;

namespace Career_Hub.Model
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        public void AddJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            if (salary < 0)
            {
                throw new SalaryException();
            }

            var dbManager = new DBManager();
            var job = new JobListing
            {
                CompanyID = this.CompanyID,
                JobTitle = jobTitle,
                JobDescription = jobDescription,
                JobLocation = jobLocation,
                Salary = salary,
                JobType = jobType,
                PostedDate = DateTime.Now
            };
            dbManager.AddJobListing(job);
        }

        public List<JobListing> ViewPostedJobs()
        {
            var dbManager = new DBManager();
            return dbManager.GetJobsByCompany(this.CompanyID);
        }
    }
}
