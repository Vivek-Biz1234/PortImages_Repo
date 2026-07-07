using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.Products.Interfaces;
using PORTIMAGES.Common.Helpers;
using System.Security.Claims;
namespace PORTIMAGES.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin,Staff")]
    [Authorize(AuthenticationSchemes = "StaffScheme")]
    public class ProductAdditionalInfoController : Controller
    {
        private readonly IProductAdditionalInfoRepository _productAdditionalInfoRepository;
        public ProductAdditionalInfoController(IProductAdditionalInfoRepository productAdditionalInfoRepository)
        {
            _productAdditionalInfoRepository = productAdditionalInfoRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AssignTerminalToProduct([FromBody] CommonAssignmentRequestDTO req)
        {
            req.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            req.ProductId= int.Parse(CryptoHelper.Decrypt(req.EncId));
            var result = await _productAdditionalInfoRepository.AssignTerminalAsync(req);
            return Json(result); 
        }
        [HttpPost]
        public async Task<IActionResult> MarkProductFoundOrNotFound([FromBody] CommonAssignmentRequestDTO req)
        {
            req.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            req.ProductId = int.Parse(CryptoHelper.Decrypt(req.EncId));
            var result = await _productAdditionalInfoRepository.MarkProductFoundOrNotFoundAsync(req);
            return Json(result);
        }


    }
}
