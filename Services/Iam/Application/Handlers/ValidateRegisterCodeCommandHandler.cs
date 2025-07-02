using MediatR;
using Iam.Application.Commands;
using Iam.Application.Contracts;
using Iam.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Iam.Application.Handlers
{
    public class ValidateRegisterCodeCommandHandler : IRequestHandler<ValidateRegisterCodeCommand, GetCompanyResource>
    {
        private readonly ICompanyRepository _companyRepository;

        public ValidateRegisterCodeCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<GetCompanyResource> Handle(ValidateRegisterCodeCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByTeamRegisterCodeAsync(request.TeamRegisterCode);
            if (company == null)
            {
                return null; // Or throw an exception if code not found
            }

            return new GetCompanyResource(company.Id, company.CompanyName, company.Country, company.Email, company.ManagerId, company.TeamRegisterCode);
        }
    }
}
