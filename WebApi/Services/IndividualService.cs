
using CoreLib.Interfaces.Services;
using CoreLib.Models;
using TodoWebApi.Services.Bases;

namespace TodoWebApi.Services
{
    public class IndividualService : BaseEntityService<Individual, int>, IIndividualService
    {
        private readonly IIndividualRepository _IndividualRepository;
        public IndividualService(IIndividualRepository repository) : base(repository)
        {
            _IndividualRepository = repository;
        }
    }
}
