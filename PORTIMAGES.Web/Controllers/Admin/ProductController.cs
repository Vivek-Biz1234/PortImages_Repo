using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.Products.Interfaces;
using PORTIMAGES.Common.Helpers;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PORTIMAGES.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin,Staff")]
    [Authorize(AuthenticationSchemes = "StaffScheme")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFilesRepository _productFilesRepository;
        private readonly IProductAdditionalInfoRepository _productAdditionalInfoRepository;
        public ProductController(IProductRepository productRepository, IProductFilesRepository productFilesRepository, IProductAdditionalInfoRepository productAdditionalInfoRepository)
        {
            _productRepository = productRepository;
            _productFilesRepository = productFilesRepository;
            _productAdditionalInfoRepository= productAdditionalInfoRepository;
        }

        #region Product Master

        public IActionResult AddProductByDocs()
        {
            return View();
        }

        public IActionResult AddProductByYardArrival()
        {
            return View();
        }
        public IActionResult AddProductAdditionalInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAdditionalInfo([FromBody] AddProductRequestDTO dto)
        {
            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _productRepository.AddProductAsync(dto);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductAdditionalInfo([FromBody] AddProductRequestDTO dto)
        {
            dto.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            dto.ID = long.Parse(CryptoHelper.Decrypt(dto.EncID));

            var result = await _productRepository.UpdateProductAsync(dto);
            return Json(result);
        }

        public IActionResult ViewProducts()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList([FromQuery] ViewProductsRequestDTO req)
        {
            var result = await _productRepository.GetProductListAsync(req);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByEncId(string pid)
        {
            var id = long.Parse(CryptoHelper.Decrypt(pid));
            var response = await _productRepository.GetProductByIdAsync(id);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string pid)
        {
            int deletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var id = long.Parse(CryptoHelper.Decrypt(pid));
            var result = await _productRepository.DeleteProductAsync(id, deletedBy);
            return Json(result);
        }

        #endregion

        #region Product Files
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductImage([FromForm] UploadProductImageRequesDTO request)
        {
            request.ProductId = long.Parse(CryptoHelper.Decrypt(request.EncID));
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _productFilesRepository.AddProductImageAsync(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductImages(string pid)
        {
            long productId = long.Parse(CryptoHelper.Decrypt(pid));
            var res = await _productFilesRepository.GetProductImagesAsync(productId);
            return Json(res);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(string pid)
        {
            int deletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            long imageid = long.Parse(pid);
            var result = await _productFilesRepository.DeleteProductImageAsync(imageid, deletedBy);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductInnerDetails(string pid)
        {
            long productId = long.Parse(CryptoHelper.Decrypt(pid));
            var res = await _productFilesRepository.GetProductInnerDetailsAsync(productId);
            return Json(res);
        }

        #endregion

        #region Assign Consignee To Products
        public IActionResult AssignConsignee()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductConsigneeList(ProductConsigneeRequestDTO req)
        {
            var result = await _productRepository.GetProductConsigneeListAsync(req);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> AssignConsignee([FromBody] AssignProdConsigneeRequestDTO dto)
        {
            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            dto.ProductId = int.Parse(CryptoHelper.Decrypt(dto.EncId));
            var result = await _productRepository.AssignConsigneeAsync(dto);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductConsigneeById(string encId)
        {
            var productId = int.Parse(CryptoHelper.Decrypt(encId));
            var result = await _productRepository.GetProductConsigneeByIdAsync(productId);
            return Json(result);
        }

        #region Old
        [HttpPost]
        public async Task<IActionResult> AssignConsignee_Old([FromBody] AssignProdConsigneeRequestDTO_Old dto)
        {
            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _productRepository.AssignConsigneeAsync_Old(dto);
            return Json(result);
        }
        #endregion

        #endregion

        #region Assign Broker To Products
        public IActionResult AssignBroker()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductBrokerList(ProductBrokerRequestDTO req)
        {
            var result = await _productRepository.GetProductBrokerListAsync(req);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignBroker([FromBody] AssignProdBrokerRequestDTO dto)
        {
            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _productRepository.AssignBrokerAsync(dto);
            return Json(result);
        }
        #endregion

        #region Created By Vivek on 01-May-2026
        [HttpGet]
        public async Task<IActionResult> AddPortImage(string _pid)
        {
            ProductInnerDetailsDTO model = new ProductInnerDetailsDTO();

            if (!string.IsNullOrEmpty(_pid))
            {
                int productId = int.Parse(CryptoHelper.Decrypt(_pid));
                var response = await _productFilesRepository.GetProductInnerDetailsAsync(productId);
                if (response.Status == 1 && response.Data != null && response.Data.Any())
                {
                    model = response.Data.FirstOrDefault();
                }
            }
            return View(model);
        }
        #endregion

        #region Created By Vivek on 06-May-2026
        [HttpGet]
        public IActionResult ConfirmCarAvailability()
        {          
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetFoundCars([FromQuery] FoundCarRequestDTO req)
        {
            var response = await _productAdditionalInfoRepository.GetFoundCarsAsync(req);
            return Json(response); 
        }
        #endregion


    }
}
