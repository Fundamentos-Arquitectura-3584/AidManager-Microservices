using MediatR;
using Iam.Application.Commands;
using Iam.Application.Contracts;
using Iam.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Iam.Application.Handlers
{
    public class EditCompanyCommandHandler : IRequestHandler<EditCompanyCommand, GetCompanyResource>
    {
        private readonly ICompanyRepository _companyRepository;

        public EditCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<GetCompanyResource> Handle(EditCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return null; // Or throw an exception
            }

            company.UpdateCompanyDetails(request.CompanyName, request.Country, request.Email);
            await _companyRepository.UpdateAsync(company);

            return new GetCompanyResource(company.Id, company.CompanyName, company.Country, company.Email, company.ManagerId, company.TeamRegisterCode);
        }
    }
}
