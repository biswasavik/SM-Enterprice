using Microsoft.Extensions.Configuration;
using SM_Enterprice.Repositories.Interfaces;
using SM_Enterprice.Utilities.Entities;
using SM_Enterprice.Utilities.Entities.Models;
using SM_Enterprice.Utilities.Models.Company;
using SM_Enterprice.Utilities.Models.Product;
using SM_Enterprice.Utilities.ResposeModel;
using SM_Enterprice.Utilities.SM_Enter_Context;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace SM_Enterprice.Repositories.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _companyContext;
        private IWebHostEnvironment _hostEnvironment;
        public CompanyRepository(CompanyContext companyContext, IWebHostEnvironment hostEnvironment)
        {
            _companyContext = companyContext;
            _hostEnvironment = hostEnvironment;
        }

        //https://www.youtube.com/watch?v=sVyNC6oB6ig
        public async Task<ResponseModel> AddCompany(CompanyModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.RequestTime = DateTime.Now;
            List<string> errors = new List<string>();
            Company company = new();
            try
            {
                var filterData = _companyContext.Companies.SingleOrDefault(x => x.RegistrationCode == model.RegistrationCode && model.Name.Equals(model.Name));
                if(filterData == null)
                {
                    company = ConvertDBType(model);
                    company.Id = Guid.NewGuid();
                    company.CreatedDate = DateTime.Now;

                   await _companyContext.AddAsync(company);
                   await _companyContext.SaveChangesAsync();
                }
                responseModel.Status = filterData == null;
                responseModel.ResponseTime = DateTime.Now;
            }
            catch(Exception ex)
            {
                errors.Add(ex.InnerException?.Message ?? ex.Message );
                responseModel.Status = false;
            }
            responseModel.Errors = errors;
            responseModel.Result = company;
            return responseModel;
        }

        public async Task<ResponseModel> GetCompanies(CompanySearchModel searchModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> AddCompanyProduct(ProductModel productModel)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.RequestTime = DateTime.Now;
            List<string> errors = new List<string>();
            Product product = new();
            try
            {
                var filterData = _companyContext.Products.SingleOrDefault(x => x.ProductCode == productModel.ProductCode);
                if (filterData == null)
                {
                    product = ConvertDBType(productModel);
                    product.Id = Guid.NewGuid();
                    product.Created = DateTime.Now;
                    
                    await _companyContext.Products.AddAsync(product);


                    var productImage = await AddProductImages(product.Id, productModel.Image);
                    if (productImage.Id != Guid.Empty && !String.IsNullOrEmpty(productImage.URL))
                    {
                        await _companyContext.ProductGalleries.AddAsync(productImage);
                    }

                    await _companyContext.SaveChangesAsync();
                }
                responseModel.Status = filterData == null;
                responseModel.ResponseTime = DateTime.Now;
            }
            catch (Exception ex) 
            {
                errors.Add(ex.InnerException?.Message ?? ex.Message);
                responseModel.Status = false;
            }
            responseModel.Errors = errors;
            responseModel.Result = product;
            responseModel.CreatedId = product.Id;
            return responseModel;
        }

        #region Private Methods
        private Company ConvertDBType(CompanyModel model)
        {
            Company comp = new Company();
            if(model == null) return comp;
            try
            {
                //comp.Id = Guid.NewGuid();
                comp.Name = model.Name;
                comp.Description = model.Description;
                comp.RegistrationCode = model.RegistrationCode;
                comp.ContactNo = model.ContactNo;
                comp.Address = model.Address;
                comp.Type = model.Type;
                comp.ManagerName = model.ManagerName;
                comp.ManagerContactNo = model.ManagerContactNo;
                comp.IsActive = model.IsActive;
                //comp.CreatedDate = model.CreatedDate;
                //comp.ModifiedDate = model.ModifiedDate;
            }
            catch (Exception ex) 
            { 
                throw;
            }
            return comp;
        }

        private Product ConvertDBType(ProductModel model)
        {
            Product product = new Product();
            if (model == null) return product;
            try
            {
                product.Name = model.Name;
                product.CompanyId = model.CompanyId;
                product.Description = model.Description;
                product.ProductCode = model.ProductCode;
                product.Price = model.Price;
                product.AmountPerBox = model.AmountPerBox;
                product.IsActive = model.IsActive;
            }
            catch (Exception ex)
            {
                throw;
            }
            return product;
        }

        private async Task<ProductGallery> AddProductImages(Guid? productId, IFormFile image)
        {
            ProductGallery productGallery = new();
            try
            {
                if (image != null && productId.HasValue) 
                {
                    string folderPath = "Images/Products/";
                    string imageName = Guid.NewGuid().ToString() + "_" + (image.FileName);
                    folderPath += imageName;
                    string url = Path.Combine(_hostEnvironment.WebRootPath, folderPath);
                    await image.CopyToAsync(new FileStream(url, FileMode.Create));

                    productGallery.Id = Guid.NewGuid();
                    productGallery.Name = imageName;
                    productGallery.URL = url;
                    productGallery.ProductId = productId.Value;
                }
            }
            catch (Exception ex) 
            { 
                throw;
            }
            return productGallery;
        }
        #endregion
    }
}
