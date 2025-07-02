using AidManager.Iam.Application.Interfaces; // Corrected
using AidManager.Iam.Domain.Entities;    // Corrected
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AidManager.Iam.Infrastructure.Persistence;

namespace AidManager.Iam.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IamDbContext _context;

        public CompanyRepository(IamDbContext context)
        {
            _context = context;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            // Assuming IamDbContext will have a DbSet<Company> named Companies
            // If not, _context.Set<Company>() is the way to go.
            // For now, I'll keep _context.Set<Company>() as it's safer until DbSet is confirmed.
            return await _context.Set<Company>().FindAsync(id);
        }

        public async Task<Company> GetByEmailAsync(string email)
        {
            return await _context.Set<Company>().FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Company> GetByTeamRegisterCodeAsync(string teamRegisterCode)
        {
            return await _context.Set<Company>().FirstOrDefaultAsync(c => c.TeamRegisterCode == teamRegisterCode);
        }

        public async Task AddAsync(Company company)
        {
            await _context.Set<Company>().AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Set<Company>().Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _context.Set<Company>().FindAsync(id);
            if (company != null)
            {
                _context.Set<Company>().Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}
