namespace AidManager.Iam.Application.DTOs // Changed namespace
{
    public record GetCompanyResource(int Id, string CompanyName, string Country, string Email, int ManagerId, string TeamRegisterCode);

    public record UpdateCompanyResource(string CompanyName, string Country, string Email);
}
