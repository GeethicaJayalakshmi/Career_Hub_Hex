using Career_Hub.Model;

namespace Career_Hub.Dao
{
    public interface IDBManager
    {
        void InitializeDatabase();

        void AddCompany(Company company);
        List<Company> GetAllCompanies();

        void AddJobListing(JobListing job);
        List<JobListing> GetAllJobListings();
        List<JobListing> GetJobsByCompany(int companyID);

        void RegisterApplicant(Applicant applicant);
        List<Applicant> GetAllApplicants();

        void SubmitApplication(JobApplication application);
        List<JobApplication> GetApplicationsByJob(int jobID);
    }
}
