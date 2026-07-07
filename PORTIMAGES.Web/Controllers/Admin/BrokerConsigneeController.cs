using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.BrokerConsignee.DTOs;
using PORTIMAGES.Application.BrokerConsignee.Interfaces;
using PORTIMAGES.Common.Helpers;
using System.Security.Claims;

namespace PORTIMAGES.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin,Staff")]
    [Authorize(AuthenticationSchemes = "StaffScheme")]
    public class BrokerConsigneeController : Controller
    {
        private readonly IConsigneeMasterRepository _consigneeMasterRepository;
        private readonly IBrokerMasterRepository _brokerMasterRepository;
        public BrokerConsigneeController(IConsigneeMasterRepository consigneeMasterRepository, IBrokerMasterRepository brokerMasterRepository)
        {
            _consigneeMasterRepository = consigneeMasterRepository;
            _brokerMasterRepository = brokerMasterRepository;
        }

        #region Consignee Master
        public IActionResult ConsigneeMaster()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> AddConsignee([FromBody] ConsigneeMasterRequestDTO dto)
        {
            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _consigneeMasterRepository.AddConsigneeAsync(dto);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateConsignee([FromBody] ConsigneeMasterRequestDTO dto)
        {
            dto.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _consigneeMasterRepository.UpdateConsigneeAsync(dto);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsigneeList()
        {
            var result = await _consigneeMasterRepository.GetConsigneeListAsync();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsigneeById(string consigneeId)
        {
            var ID=Convert.ToInt32(CryptoHelper.Decrypt(consigneeId));
            var response = await _consigneeMasterRepository.GetConsigneeByIdAsync(ID);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConsignee(string consigneeId)
        {
            var ID = Convert.ToInt32(CryptoHelper.Decrypt(consigneeId));
            int deletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _consigneeMasterRepository.DeleteConsigneeAsync(ID, deletedBy);
            return Json(result);
        }
        #endregion

        #region Broker Master
        public IActionResult BrokerMaster()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBroker([FromBody] BrokerMasterRequestDTO dto)
        {
            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _brokerMasterRepository.AddBrokerAsync(dto);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBroker([FromBody] BrokerMasterRequestDTO dto)
        {
            dto.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _brokerMasterRepository.UpdateBrokerAsync(dto);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBrokerList()
        {
            var result = await _brokerMasterRepository.GetBrokerListAsync();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBrokerById(string brokerId)
        {
            var ID = Convert.ToInt32(CryptoHelper.Decrypt(brokerId));
            var response = await _brokerMasterRepository.GetBrokerByIdAsync(ID);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBroker(string brokerId)
        {
            var ID = Convert.ToInt32(CryptoHelper.Decrypt(brokerId));
            int deletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _brokerMasterRepository.DeleteBrokerAsync(ID, deletedBy);
            return Json(result);
        }
        #endregion
    }
}
