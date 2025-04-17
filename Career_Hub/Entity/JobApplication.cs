namespace Career_Hub.Model
{
    public class JobApplication
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CoverLetter { get; set; }

        public void PrintSubmissionDetails()
        {
            Console.WriteLine($"Application ID {ApplicationID} JobI D {JobID} by Applicant ID {ApplicantID}.");
        }
    }
}
