using MediatR;
using Iam.Application.DTOs;

namespace Iam.Application.Commands
{
    public record EditCompanyCommand(int CompanyId, string CompanyName, string Country, string Email) : IRequest<GetCompanyResource>;
}
