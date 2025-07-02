using MediatR;
using Iam.Application.DTOs;

namespace Iam.Application.Commands
{
    public record ValidateRegisterCodeCommand(string TeamRegisterCode) : IRequest<GetCompanyResource>;
}
