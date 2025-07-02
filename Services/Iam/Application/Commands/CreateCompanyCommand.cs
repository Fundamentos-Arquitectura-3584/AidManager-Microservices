using MediatR;
using Iam.Application.DTOs;

namespace Iam.Application.Commands
{
    public record CreateCompanyCommand(string CompanyName, string Country, string Email, int UserId) : IRequest<GetCompanyResource>;
}
