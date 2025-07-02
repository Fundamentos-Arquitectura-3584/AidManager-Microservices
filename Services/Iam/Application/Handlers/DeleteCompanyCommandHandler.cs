using MediatR;
using Iam.Application.Commands;
using Iam.Application.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Iam.Application.Handlers
{
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
                return false; // Or throw an exception
            }

            await _companyRepository.DeleteAsync(request.CompanyId);
            return true;
        }
    }
}
