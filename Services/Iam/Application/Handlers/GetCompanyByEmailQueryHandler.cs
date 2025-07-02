using MediatR;
using Iam.Application.Queries;
using Iam.Application.Contracts;
using Iam.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Iam.Application.Handlers
{
    public class GetCompanyByEmailQueryHandler : IRequestHandler<GetCompanyByEmailQuery, GetCompanyResource>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByEmailQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<GetCompanyResource> Handle(GetCompanyByEmailQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByEmailAsync(request.Email);
            if (company == null)
            {
                return null; // Or throw an exception
            }

            return new GetCompanyResource(company.Id, company.CompanyName, company.Country, company.Email, company.ManagerId, company.TeamRegisterCode);
        }
    }
}
