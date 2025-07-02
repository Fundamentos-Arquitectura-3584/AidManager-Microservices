using AidManager.Iam.Domain.Entities; // Changed namespace
using System.Threading.Tasks;

namespace AidManager.Iam.Application.Interfaces // Changed namespace
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(int id);
        Task<Company> GetByEmailAsync(string email);
        Task<Company> GetByTeamRegisterCodeAsync(string teamRegisterCode);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
    }
}
