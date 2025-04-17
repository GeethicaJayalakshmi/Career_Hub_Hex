using Career_Hub.Dao;
using Career_Hub.My_Exception;

namespace Career_Hub.Model
{
    public class JobListing
    {
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }

        public void ApplyToJob(int applicantID, string coverLetter)
        {
            if (Salary < 0)
            {
                throw new SalaryException();
            }

            var dbManager = new DBManager();
            var application = new JobApplication
            {
                JobID = this.JobID,
                ApplicantID = applicantID,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
            };
            dbManager.SubmitApplication(application);
        }

        public List<JobApplication> ViewApplications()
        {
            var dbManager = new DBManager();
            return dbManager.GetApplicationsByJob(this.JobID);
        }
    }
}
