using IRS.API.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;
using CoreLib.Interfaces.Services;
using CoreLib.Models;
using TodoWebApi.Services;

namespace IRS.API.Controllers.EntityControllers
{
    [Route("[controller]")]
    [ApiController]
    public class IndividualController : BaseEntityController<Individual, int>
    {
        public IndividualController(IIndividualService service, IPushNotificationService notificationService) : base(service, notificationService)
        {
        }
    }
}
