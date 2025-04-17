using System;
using System.Collections.Generic;
using Career_Hub.Model;
using Career_Hub.My_Exception;
using Career_Hub.Util;
using Microsoft.Data.SqlClient;

namespace Career_Hub.Dao
{
    public class DBManager : IDBManager
    {
        public void InitializeDatabase()
        {
            try
            {
                string createCompanies = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Companies')
                    BEGIN
                        CREATE TABLE Companies (
                            CompanyID INT PRIMARY KEY,
                            CompanyName VARCHAR(100),
                            Location VARCHAR(100)
                        );
                    END";

                string createJobs = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Jobs')
                    BEGIN
                        CREATE TABLE Jobs (
                            JobID INT PRIMARY KEY,
                            CompanyID INT,
                            JobTitle VARCHAR(100),
                            JobDescription TEXT,
                            JobLocation VARCHAR(100),
                            Salary DECIMAL(18, 2),
                            JobType VARCHAR(50),
                            PostedDate DATETIME,
                            FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID)
                        );
                    END";

                using (var connection = DBConnUtil.GetConnection())
                {
                    using (var cmd = new SqlCommand(createCompanies, connection))
                        cmd.ExecuteNonQuery();

                    using (var cmd = new SqlCommand(createJobs, connection))
                        cmd.ExecuteNonQuery();
                }

                Console.WriteLine("Database initialized successfully.");
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }

        public void AddCompany(Company company)
        {
            try
            {
                string query = "INSERT INTO Companies (CompanyID, CompanyName, Location) VALUES (@CompanyID, @CompanyName, @Location)";
                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                    cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    cmd.Parameters.AddWithValue("@Location", company.Location);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }

        public List<Company> GetAllCompanies()
        {
            var list = new List<Company>();
            try
            {
                string query = "SELECT * FROM Companies";
                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Company
                        {
                            CompanyID = reader.GetInt32(0),
                            CompanyName = reader.GetString(1),
                            Location = reader.GetString(2)
                        });
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }

        public void AddJobListing(JobListing job)
        {
            try
            {
                string query = @"
                    INSERT INTO Jobs (JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate)
                    VALUES (@JobID, @CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";

                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@JobID", job.JobID);
                    cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                    cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                    cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                    cmd.Parameters.AddWithValue("@Salary", job.Salary);
                    cmd.Parameters.AddWithValue("@JobType", job.JobType);
                    cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }

        public List<JobListing> GetAllJobListings()
        {
            var list = new List<JobListing>();
            try
            {
                string query = "SELECT * FROM Jobs";
                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new JobListing
                        {
                            JobID = reader.GetInt32(0),
                            CompanyID = reader.GetInt32(1),
                            JobTitle = reader.GetString(2),
                            JobDescription = reader.GetString(3),
                            JobLocation = reader.GetString(4),
                            Salary = reader.GetDecimal(5),
                            JobType = reader.GetString(6),
                            PostedDate = reader.GetDateTime(7)
                        });
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }

        public List<JobListing> GetJobsByCompany(int companyID)
        {
            var list = new List<JobListing>();
            try
            {
                string query = "SELECT * FROM Jobs WHERE CompanyID = @CompanyID";
                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CompanyID", companyID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new JobListing
                            {
                                JobID = reader.GetInt32(0),
                                CompanyID = reader.GetInt32(1),
                                JobTitle = reader.GetString(2),
                                JobDescription = reader.GetString(3),
                                JobLocation = reader.GetString(4),
                                Salary = reader.GetDecimal(5),
                                JobType = reader.GetString(6),
                                PostedDate = reader.GetDateTime(7)
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }

        public void RegisterApplicant(Applicant applicant)
        {
            try
            {
                string query = @"
                    INSERT INTO Applicants (ApplicantID, FirstName, LastName, Email, Phone, Resume)
                    VALUES (@ApplicantID, @FirstName, @LastName, @Email, @Phone, @Resume)";

                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ApplicantID", applicant.ApplicantID);
                    cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", applicant.LastName);
                    cmd.Parameters.AddWithValue("@Email", applicant.Email);
                    cmd.Parameters.AddWithValue("@Phone", applicant.Phone);
                    cmd.Parameters.AddWithValue("@Resume", applicant.Resume);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new InvalidEmailException();
            }
        }

        public List<Applicant> GetAllApplicants()
        {
            var list = new List<Applicant>();
            try
            {
                string query = "SELECT * FROM Applicants";
                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Applicant
                        {
                            ApplicantID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            Phone = reader.GetString(4),
                            Resume = reader.GetString(5)
                        });
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }

        public void SubmitApplication(JobApplication application)
        {
            try
            {
                string query = @"
                    INSERT INTO Applications (ApplicationID, JobID, ApplicantID, ApplicationDate, CoverLetter)
                    VALUES (@ApplicationID, @JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
                    cmd.Parameters.AddWithValue("@JobID", application.JobID);
                    cmd.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                    cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                    cmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }

        public List<JobApplication> GetApplicationsByJob(int jobID)
        {
            var list = new List<JobApplication>();
            try
            {
                string query = "SELECT * FROM Applications WHERE JobID = @JobID";
                using (var connection = DBConnUtil.GetConnection())
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@JobID", jobID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new JobApplication
                            {
                                ApplicationID = reader.GetInt32(0),
                                JobID = reader.GetInt32(1),
                                ApplicantID = reader.GetInt32(2),
                                ApplicationDate = reader.GetDateTime(3),
                                CoverLetter = reader.GetString(4)
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException();
            }
        }
    }
}
