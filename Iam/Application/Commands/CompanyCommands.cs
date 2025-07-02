using MediatR;
using AidManager.Iam.Domain.Entities; // Changed namespace for Company

namespace AidManager.Iam.Application.Commands // Changed namespace
{
    // Command to create a new company. Returns the ID of the newly created company.
    public record CreateCompanyCommand(string CompanyName, string Country, string Email, int UserId) : IRequest<int>;

    // Command to delete a company by its ID. Returns a boolean indicating success.
    public record DeleteCompanyCommand(int CompanyId) : IRequest<bool>;

    // Command to edit an existing company's details. Returns a boolean indicating success.
    public record EditCompanyCommand(int CompanyId, string CompanyName, string Country, string Email) : IRequest<bool>;

    // Command to validate a team register code. Returns the Company entity if the code is valid, otherwise null.
    public record ValidateRegisterCodeCommand(string TeamRegisterCode) : IRequest<Company?>; // Return nullable Company
}
