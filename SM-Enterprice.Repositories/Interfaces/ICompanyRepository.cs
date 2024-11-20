using SM_Enterprice.Utilities.Entities;
using SM_Enterprice.Utilities.Entities.Models;
using SM_Enterprice.Utilities.Models.Company;
using SM_Enterprice.Utilities.ResposeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_Enterprice.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<ResponseModel> GetCompanies(CompanySearchModel searchModel);
        Task<ResponseModel> AddCompany(CompanyModel model);
        Task<ResponseModel> AddCompanyProduct(ProductModel productModel);
    }
}
