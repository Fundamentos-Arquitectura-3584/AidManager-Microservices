using System;

namespace AidManager.Iam.Domain.Entities // Changed namespace
{
    public class Company
    {
        public int Id { get; private set; }
        public string CompanyName { get; private set; }
        public string Country { get; private set; }
        public string Email { get; private set; }
        public int ManagerId { get; private set; }
        public string TeamRegisterCode { get; private set; }

        // Private constructor for EF Core or other ORMs
        private Company() { }

        public Company(string companyName, string country, string email, int managerId)
        {
            if (string.IsNullOrWhiteSpace(companyName))
                throw new ArgumentException("Company name cannot be empty.", nameof(companyName));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            CompanyName = companyName;
            Country = country;
            Email = email;
            ManagerId = managerId;
            TeamRegisterCode = GenerateRegisterCode();
        }

        // Public constructor for creating a company when the ID is known (e.g. when retrieving from DB)
        // This is typically used by ORM or mapping layers.
        public Company(int id, string companyName, string country, string email, int managerId, string teamRegisterCode)
        {
            Id = id;
            CompanyName = companyName;
            Country = country;
            Email = email;
            ManagerId = managerId;
            TeamRegisterCode = teamRegisterCode;
        }


        private string GenerateRegisterCode()
        {
            Guid guid = Guid.NewGuid();
            string base64Guid = Convert.ToBase64String(guid.ToByteArray());
            // Replace URL-unsafe characters and shorten to 15 chars
            string shortGuid = base64Guid.Replace("/", "_").Replace("+", "-").Substring(0, 15);
            return shortGuid;
        }

        public void UpdateDetails(string companyName, string country, string email)
        {
            if (string.IsNullOrWhiteSpace(companyName))
                throw new ArgumentException("Company name cannot be empty.", nameof(companyName));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            CompanyName = companyName;
            Country = country;
            Email = email;
        }

        public void AssignManager(int managerId)
        {
            // Potentially add validation for managerId if needed (e.g. > 0)
            ManagerId = managerId;
        }
    }
}
