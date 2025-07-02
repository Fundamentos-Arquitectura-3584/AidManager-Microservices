using MediatR;
using Iam.Application.DTOs;

namespace Iam.Application.Queries
{
    public record GetCompanyByIdQuery(int CompanyId) : IRequest<GetCompanyResource>;
}
