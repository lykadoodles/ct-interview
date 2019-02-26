using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ct.Interview.Web.Api.Interfaces;

namespace Ct.Interview.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsxListedCompaniesController : ControllerBase
    {
        private readonly IAsxListedCompaniesService _asxListedCompaniesService;

        public AsxListedCompaniesController(IAsxListedCompaniesService asxListedCompaniesService)
        {
            _asxListedCompaniesService = asxListedCompaniesService ?? throw new ArgumentNullException(nameof(asxListedCompaniesService)) ;
        }

        [HttpGet]
        public async Task<ActionResult<AsxListedCompanyResponse>> GetByAsxCode(string asxCode)
        {
            try
            {
                AsxListedCompany asxListedCompanies = await _asxListedCompaniesService.GetByAsxCode(asxCode).ConfigureAwait(false);

                AsxListedCompanyResponse asxListedCompaniesResponse = JsonConvert.DeserializeObject<AsxListedCompanyResponse>(asxListedCompanies.ToString());

                return Ok(asxListedCompaniesResponse);
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}
