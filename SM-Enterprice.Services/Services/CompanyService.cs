using SM_Enterprice.Repositories.Interfaces;
using SM_Enterprice.Services.Interfaces;
using SM_Enterprice.Utilities.Entities;
using SM_Enterprice.Utilities.Entities.Models;
using SM_Enterprice.Utilities.Models.Company;
using SM_Enterprice.Utilities.ResposeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_Enterprice.Services.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;   
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ResponseModel> AddCompany(CompanyModel model)
        {
            return await _companyRepository.AddCompany(model);
        }

        public async Task<ResponseModel> AddCompanyProduct(ProductModel productModel)
        {
            return await _companyRepository.AddCompanyProduct(productModel);
        }

        public async Task<ResponseModel> GetCompanies(CompanySearchModel searchModel)
        {
            return await _companyRepository.GetCompanies(searchModel);
        }
    }
}
