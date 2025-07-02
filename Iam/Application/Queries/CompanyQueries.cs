using MediatR;
using AidManager.Iam.Application.DTOs; // Changed namespace

namespace AidManager.Iam.Application.Queries // Changed namespace
{
    // Query to get a company by its email. Returns a GetCompanyResource DTO, or null if not found.
    public record GetCompanyByEmailQuery(string Email) : IRequest<GetCompanyResource?>; // Return nullable DTO

    // Query to get a company by its ID. Returns a GetCompanyResource DTO, or null if not found.
    public record GetCompanyByIdQuery(int CompanyId) : IRequest<GetCompanyResource?>; // Return nullable DTO
}
