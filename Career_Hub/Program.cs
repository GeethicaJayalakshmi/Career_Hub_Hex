using System;
using Career_Hub.Dao;
using Career_Hub.Model;
using Career_Hub.My_Exception;
using System.Text.RegularExpressions;

namespace Career_Hub
{
    class Program
    {
        static void Main(string[] args)
        {
            DBManager dbManager = new DBManager();

            while (true)
            {
                Console.WriteLine("\n--- CAREER HUB ---");
                Console.WriteLine();
                Console.WriteLine("1. Initialize Database");
                Console.WriteLine("2. Add Company");
                Console.WriteLine("3. View All Companies");
                Console.WriteLine("4. Post Job Listing");
                Console.WriteLine("5. View All Job Listings");
                Console.WriteLine("6. View Jobs by Company");
                Console.WriteLine("7. Register Applicant");
                Console.WriteLine("8. View All Applicants");
                Console.WriteLine("9. View Applications by Job ID");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            dbManager.InitializeDatabase();
                            break;
                        case 2:
                            AddCompany();
                            break;
                        case 3:
                            ViewAllCompanies();
                            break;
                        case 4:
                            InsertJobListing();
                            break;
                        case 5:
                            ViewAllJobs();
                            break;
                        case 6:
                            ViewJobsByCompany();
                            break;
                        case 7:
                            RegisterApplicant();
                            break;
                        case 8:
                            ViewAllApplicants();
                            break;
                        case 9:
                            ViewApplicationsByJob();
                            break;
                        case 0:
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (SalaryException)
                {
                    Console.WriteLine("Salary cannot be negative.");
                }
                catch (DatabaseConnectionException)
                {
                    Console.WriteLine("Database connection failed.");
                }
                catch (InvalidEmailException)
                {
                    Console.WriteLine("Invalid email format.");
                }
                catch (FileUploadException)
                {
                    Console.WriteLine("Invalid resume format. Only .pdf or .docx files are allowed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";  // Regex pattern for valid email
            return Regex.IsMatch(email, pattern);
        }

        static void AddCompany()
        {
            Console.WriteLine("Enter Company ID:");
            int companyID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Company Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Location:");
            string location = Console.ReadLine();

            Company company = new Company
            {
                CompanyID = companyID,
                CompanyName = name,
                Location = location
            };

            DBManager dbManager = new DBManager();
            dbManager.AddCompany(company);
            Console.WriteLine("Company added successfully.");
        }

        static void RegisterApplicant()
        {
            Console.WriteLine("Enter Applicant ID:");
            int applicantID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();

            // Validate email format
            if (!IsValidEmail(email))
            {
                throw new InvalidEmailException();  
            }

            Console.WriteLine("Enter Phone:");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter Resume (filename):");
            string resumeFileName = Console.ReadLine();

            // Validate the file extension for resume upload
            if (!IsValidFileFormat(resumeFileName))
            {
                throw new FileUploadException();  
            }

            Applicant applicant = new Applicant
            {
                ApplicantID = applicantID,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Resume = resumeFileName
            };

            DBManager dbManager = new DBManager();
            dbManager.RegisterApplicant(applicant);
            Console.WriteLine("Applicant registered successfully.");
        }

        public static bool IsValidFileFormat(string fileName)
        {
            string[] validExtensions = { ".pdf", ".docx" };
            string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
            return Array.Exists(validExtensions, ext => ext == fileExtension);
        }

        static void InsertJobListing()
        {
            Console.WriteLine("Enter Job ID:");
            int jobID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Company ID:");
            int companyID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Job Title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Job Description:");
            string description = Console.ReadLine();
            Console.WriteLine("Enter Job Location:");
            string location = Console.ReadLine();
            Console.WriteLine("Enter Salary:");
            decimal salary = Convert.ToDecimal(Console.ReadLine());

            if (salary < 0)
                throw new SalaryException();

            Console.WriteLine("Enter Job Type:");
            string type = Console.ReadLine();

            JobListing job = new JobListing
            {
                JobID = jobID,
                CompanyID = companyID,
                JobTitle = title,
                JobDescription = description,
                JobLocation = location,
                Salary = salary,
                JobType = type,
                PostedDate = DateTime.Now
            };

            DBManager dbManager = new DBManager();
            dbManager.AddJobListing(job);
            Console.WriteLine("Job listing added successfully.");
        }

        static void ViewAllJobs()
        {
            DBManager dbManager = new DBManager();
            var jobs = dbManager.GetAllJobListings();
            Console.WriteLine("\nJob Listings:");
            foreach (var job in jobs)
            {
                Console.WriteLine($"{job.JobID} - {job.JobTitle} at Company {job.CompanyID}");
                Console.WriteLine($"Location: {job.JobLocation}, Salary: {job.Salary}, Type: {job.JobType}");
                Console.WriteLine($"Posted: {job.PostedDate}, Description: {job.JobDescription}");
                Console.WriteLine("-----------------------------");
            }
        }

        static void ViewJobsByCompany()
        {
            Console.WriteLine("Enter Company ID to view jobs:");
            int companyID = Convert.ToInt32(Console.ReadLine());

            DBManager dbManager = new DBManager();
            var jobs = dbManager.GetJobsByCompany(companyID);
            Console.WriteLine($"\nJobs for Company ID {companyID}:");
            foreach (var job in jobs)
            {
                Console.WriteLine($"{job.JobID} - {job.JobTitle} ({job.JobLocation})");
            }
        }

        static void ViewAllCompanies()
        {
            DBManager dbManager = new DBManager();
            var companies = dbManager.GetAllCompanies();
            Console.WriteLine("\nCompanies:");
            foreach (var c in companies)
                Console.WriteLine($"{c.CompanyID} - {c.CompanyName} ({c.Location})");
        }

        static void ViewAllApplicants()
        {
            DBManager dbManager = new DBManager();
            var applicants = dbManager.GetAllApplicants();
            Console.WriteLine("\nApplicants:");
            foreach (var a in applicants)
                Console.WriteLine($"{a.ApplicantID}: {a.FirstName} {a.LastName} - {a.Email} - {a.Phone}");
        }

        static void ViewApplicationsByJob()
        {
            Console.WriteLine("Enter Job ID to view applications:");
            int jobID = Convert.ToInt32(Console.ReadLine());

            DBManager dbManager = new DBManager();
            var apps = dbManager.GetApplicationsByJob(jobID);
            Console.WriteLine($"\nApplications for Job ID {jobID}:");
            foreach (var app in apps)
            {
                Console.WriteLine($"{app.ApplicationID} - Applicant ID: {app.ApplicantID}");
                Console.WriteLine($"Date: {app.ApplicationDate}, Cover Letter: {app.CoverLetter}");
                Console.WriteLine("-----------------------------");
            }
        }
    }
}
