using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM_Enterprice.Services.Interfaces;
using SM_Enterprice.Utilities.Entities;
using SM_Enterprice.Utilities.Entities.Models;
using SM_Enterprice.Utilities.Models.Company;
using System.Text.Json;

namespace SM_Enterprice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CompanyController(ICompanyService companyService, IHttpContextAccessor httpContext, IWebHostEnvironment hostEnvironment)
        {
            _companyService = companyService;
            _httpContext = httpContext;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [Route("~/api/Companies")]
        public async Task<IActionResult> GetCompanies(CompanySearchModel searchModel)
        {
            if (searchModel != null)
            {
                var result = await _companyService.GetCompanies(searchModel);
                if (result == null)
                {
                    return NotFound(new { ErrorMessage = "Couldn't find any results" });
                }
                return Ok(result);
            }
            return BadRequest(new { ErrorMessage = "Model is Invalid!" });
        }

        [HttpGet]
        [Route("Products/{id:Guid}", Name = "SpecificProduct")]
        public async Task<IActionResult> GetProducts()
        {
            //var er = _httpContext.HttpContext.Request;
            return Ok();
        }

        [HttpPost]
        [Route("AddCompany")]
        public async Task<IActionResult> AddCompany(CompanyModel model)
        {
            if (model != null)
            {
                var result = await _companyService.AddCompany(model);
                if (result.Status)
                {
                    return CreatedAtAction("", "Company", new { id = result.CreatedId }, result);
                }
            }
            return BadRequest(new { ErrorMessage = "Model is Invalid!" });
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddCompanyProduct([FromForm]ProductModel productModel)
        {
           
            if (productModel != null)
            {
                var result = await _companyService.AddCompanyProduct(productModel);
                
                if (result.Status)
                {
                    var requestContext = _httpContext.HttpContext.Request;
                    var uri = new UriBuilder(requestContext.Scheme,requestContext.Host.Host, requestContext.Host.Port ?? 443, "api/Company/Products/" + result.CreatedId.ToString());
                    //uri += "/Products/" + result.CreatedId.ToString();
                    return Created(uri.ToString(), result.Result);
                }
            }
            return BadRequest(new { ErrorMessage = $"{nameof(productModel)} is Invalid!" });
        }

    }
}
//https://www.youtube.com/watch?v=9OTSieiqiuY&t=19s