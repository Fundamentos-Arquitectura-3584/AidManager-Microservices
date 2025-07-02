using MediatR;
using AidManager.Iam.Application.Queries;   // Changed namespace
using AidManager.Iam.Application.DTOs;    // Changed namespace
using AidManager.Iam.Application.Interfaces; // Changed namespace
using AidManager.Iam.Domain.Entities;     // Changed namespace (though Company entity isn't directly used here, good for consistency)
using System.Threading;
using System.Threading.Tasks;
// Potentially add: using Microsoft.Extensions.Logging;

namespace AidManager.Iam.Application.Handlers // Changed namespace
{
    public class GetCompanyByEmailQueryHandler : IRequestHandler<GetCompanyByEmailQuery, GetCompanyResource?> // Return nullable DTO
    {
        private readonly ICompanyRepository _companyRepository;
        // private readonly ILogger<GetCompanyByEmailQueryHandler> _logger; // Optional

        public GetCompanyByEmailQueryHandler(ICompanyRepository companyRepository /*, ILogger<GetCompanyByEmailQueryHandler> logger */)
        {
            _companyRepository = companyRepository;
            // _logger = logger;
        }

        public async Task<GetCompanyResource?> Handle(GetCompanyByEmailQuery request, CancellationToken cancellationToken) // Return nullable DTO
        {
            var company = await _companyRepository.GetByEmailAsync(request.Email);
            if (company == null)
            {
                // _logger.LogInformation("Company with email {Email} not found.", request.Email);
                return null;
            }

            return new GetCompanyResource(company.Id, company.CompanyName, company.Country, company.Email, company.ManagerId, company.TeamRegisterCode);
        }
    }

    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, GetCompanyResource?> // Return nullable DTO
    {
        private readonly ICompanyRepository _companyRepository;
        // private readonly ILogger<GetCompanyByIdQueryHandler> _logger; // Optional

        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository /*, ILogger<GetCompanyByIdQueryHandler> logger */)
        {
            _companyRepository = companyRepository;
            // _logger = logger;
        }

        public async Task<GetCompanyResource?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken) // Return nullable DTO
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                // _logger.LogInformation("Company with ID {CompanyId} not found.", request.CompanyId);
                return null;
            }

            return new GetCompanyResource(company.Id, company.CompanyName, company.Country, company.Email, company.ManagerId, company.TeamRegisterCode);
        }
    }
}
