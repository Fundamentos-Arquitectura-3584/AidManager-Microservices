using MediatR;
using AidManager.Iam.Application.Commands; // Changed namespace
using AidManager.Iam.Application.Interfaces; // Changed namespace
using AidManager.Iam.Domain.Entities; // Changed namespace
using System.Threading;
using System.Threading.Tasks;
// Potentially add: using Microsoft.Extensions.Logging; for logging

namespace AidManager.Iam.Application.Handlers // Changed namespace
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        // private readonly ILogger<CreateCompanyCommandHandler> _logger; // Optional: for logging

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository /*, ILogger<CreateCompanyCommandHandler> logger */)
        {
            _companyRepository = companyRepository;
            // _logger = logger;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            // Consider checking if a company with the same email already exists
            // var existingCompany = await _companyRepository.GetByEmailAsync(request.Email);
            // if (existingCompany != null) { /* Handle duplicate email, throw exception or return error */ }

            var company = new Company(request.CompanyName, request.Country, request.Email, request.UserId);

            await _companyRepository.AddAsync(company);
            // _logger.LogInformation("Company {CompanyId} created: {CompanyName}", company.Id, company.CompanyName);

            // EF Core populates the ID after AddAsync and SaveChangesAsync (if ID is DB-generated)
            return company.Id;
        }
    }

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, bool>
    {
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                // _logger.LogWarning("Company with Id {CompanyId} not found for deletion.", request.CompanyId);
                return false;
            }

            await _companyRepository.DeleteAsync(request.CompanyId);
            // _logger.LogInformation("Company {CompanyId} deleted.", request.CompanyId);
            return true;
        }
    }

    public class EditCompanyCommandHandler : IRequestHandler<EditCompanyCommand, bool>
    {
        private readonly ICompanyRepository _companyRepository;

        public EditCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<bool> Handle(EditCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                // _logger.LogWarning("Company with Id {CompanyId} not found for edit.", request.CompanyId);
                return false;
            }

            company.UpdateDetails(request.CompanyName, request.Country, request.Email);

            await _companyRepository.UpdateAsync(company);
            // _logger.LogInformation("Company {CompanyId} updated.", company.Id);
            return true;
        }
    }

    public class ValidateRegisterCodeCommandHandler : IRequestHandler<ValidateRegisterCodeCommand, Company?> // Return nullable Company
    {
        private readonly ICompanyRepository _companyRepository;

        public ValidateRegisterCodeCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Company?> Handle(ValidateRegisterCodeCommand request, CancellationToken cancellationToken) // Return nullable Company
        {
            var company = await _companyRepository.GetByTeamRegisterCodeAsync(request.TeamRegisterCode);
            // if (company == null) { _logger.LogInformation("Invalid registration code: {RegisterCode}", request.TeamRegisterCode); }
            // else { _logger.LogInformation("Valid registration code {RegisterCode} for company {CompanyId}", request.TeamRegisterCode, company.Id); }
            return company;
        }
    }
}
