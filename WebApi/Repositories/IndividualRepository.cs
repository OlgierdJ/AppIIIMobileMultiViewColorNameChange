using CoreLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CoreLib.Extensions.LINQExtensions;
using CoreLib.Interfaces.Services;
using TodoWebApi.Repositories.Bases;
using TodoWebApi.EFCore;

namespace TodoWebApi.Repositories
{
    public class IndividualRepository : BaseEntityRepository<Individual>, IIndividualRepository
    {
        public IndividualRepository(IndividualDbContext context) : base(context)
        {
        }

        public override Task<IEnumerable<Individual>> GetAllWithIncludeAsync(Expression<Func<Individual, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<Individual> GetSingleWithIncludeAsync(Expression<Func<Individual, bool>> expression)
        {
            return await QueryAll()
                .WhereIf(expression)
                .FirstOrDefaultAsync();
        }
    }
}
