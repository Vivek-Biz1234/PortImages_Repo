using Azure.Core;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using PORTIMAGES.Application.User.Commands;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Application.User.Queries;
using PORTIMAGES.Common.Helpers;
using System.Security.Claims;

namespace PORTIMAGES.Web.Controllers.User
{
    [Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = "UserScheme")]
    public class UserController : Controller
    {
        public readonly IMediator _mediator;
        private readonly IImageDownloadRepository _imageDownloadRepository;
        public UserController(IMediator mediator, IImageDownloadRepository imageDownloadRepository)
        {
            this._mediator = mediator;
            this._imageDownloadRepository = imageDownloadRepository;
        }
        public IActionResult DashBoard()
        {
            return View();
        }

        #region MyProduct

        public IActionResult MyProduct()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetProductList(GetMyProductListQuery req)
        {
            req.ClientId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _mediator.Send(req);
            return Json(response);
        }

        #endregion
        #region Enquiry

        [HttpPost]
        public async Task<IActionResult> AddInquiry([FromBody] AddInquiryCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            request.ClientID = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(int ID)
        {
            ID = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _mediator.Send(new GetInquiryByIdQuery(ID));
            return Json(response);
        }
        #endregion 
        #region Product-Details  

        [HttpGet]
        public async Task<IActionResult> ProductDetails(string _pid)
        {
            long productId = long.Parse(CryptoHelper.Decrypt(_pid));
            long clientId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var response = await _mediator.Send(new GetProductDetailsByIdQuery
            {
                ProductId = productId,
                ClientId = clientId
            });

            if (response.Status != 1 || response.Data == null)
                return NotFound();

            return View(response.Data);
        }
        #endregion 
        #region Download-Images (STEP-3)

        [HttpPost]
        public async Task<IActionResult> PrepareImageDownload([FromBody] List<string> encIDs)
        {
            if (encIDs == null || encIDs.Count == 0)
                return BadRequest("No cars selected");

            try
            {
                var productIds = encIDs.Select(x => long.Parse(CryptoHelper.Decrypt(x))).ToList();
                var clientId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var zipBytes = await _imageDownloadRepository.PrepareImagesZipAsync(productIds, clientId);

                if (zipBytes == null || zipBytes.Length == 0)
                    return BadRequest("No images available for selected cars");

                return File(zipBytes, "application/zip", "Photos.zip");
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString().Substring(0, 8);
                return StatusCode(500, $"Error preparing download. Please contact support with Error ID: {errorId}");
            }
        }

        #endregion

        #region ImportExcel Created By Vivek on 18-May-2026
        public IActionResult VehicleImport()
        {
            return View();
        }
        #endregion
    }
}
