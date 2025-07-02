using MediatR;

namespace Iam.Application.Commands
{
    public record DeleteCompanyCommand(int CompanyId) : IRequest<bool>;
}
