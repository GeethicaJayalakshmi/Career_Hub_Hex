using Career_Hub.Dao;
using Career_Hub.My_Exception;

namespace Career_Hub.Model
{
    public class Applicant
    {
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }

        public void RegisterApplicant(string email, string firstName, string lastName, string phone, string resume)
        {
            if (!IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }

            var dbManager = new DBManager();
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Resume = resume;
            dbManager.RegisterApplicant(this);
        }

        public void SubmitJobApplication(int jobID, string coverLetter)
        {
            var dbManager = new DBManager();
            var application = new JobApplication
            {
                JobID = jobID,
                ApplicantID = this.ApplicantID,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
            };
            dbManager.SubmitApplication(application);
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }
    }
}
