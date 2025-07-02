using MediatR;
using Iam.Application.DTOs;

namespace Iam.Application.Queries
{
    public record GetCompanyByEmailQuery(string Email) : IRequest<GetCompanyResource>;
}
