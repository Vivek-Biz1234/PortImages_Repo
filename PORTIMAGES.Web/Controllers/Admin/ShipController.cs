using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.DTOs.PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.Interfaces;
using System.Security.Claims;

namespace PORTIMAGES.Web.Controllers.Admin
{  
    [Authorize(Roles = "Admin,Staff")]
    [Authorize(AuthenticationSchemes = "StaffScheme")]
    public class ShipController : Controller
    {
        private readonly IShipRepository _shipRepository;
        private readonly IPortRepository _portRepository;
        private readonly IShippingRepository _shippingRepository;
        private readonly IMakerRepository _makerRepository;
        private readonly IModelRepository _modelRepository;
        public ShipController(IShipRepository shipRepository, IPortRepository portRepository, IShippingRepository shippingRepository
            , IMakerRepository makerRepository, IModelRepository modelRepository)
        {
            this._shipRepository = shipRepository;
            this._portRepository = portRepository;
            this._shippingRepository = shippingRepository;
            this._makerRepository = makerRepository; 
            this._modelRepository = modelRepository;
        }
        #region ShipMaster
        public IActionResult ShipMaster()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddShip([FromBody] ShipRequestDTO request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _shipRepository.AddShipAsync(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShip([FromBody] ShipRequestDTO request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);            
            var result = await _shipRepository.UpdateShipAsync(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetShipById(int ID)
        { 
            var response = await _shipRepository.GetShipByIdAsync(ID);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetShipList()
        { 
            var response = await _shipRepository.GetShipListAsync();
            return Json(response);
        }

        public async Task<IActionResult> DeleteShip(int Id)
        {
            int DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _shipRepository.DeleteShipAsync(Id, DeletedBy);
            return Json(result);
        }

        #endregion  
        #region PortMaster
        public IActionResult PortMaster()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPort([FromBody] PortRequestDTO request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _portRepository.AddPortAsync(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePort([FromBody] PortRequestDTO request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _portRepository.UpdatePortAsync(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPortById(int ID)
        {
            var response = await _portRepository.GetPortByIdAsync(ID);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPortList()
        {
            var response = await _portRepository.GetPortListAsync();
            return Json(response);
        }

        public async Task<IActionResult> DeletePort(int Id)
        {
            int DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _portRepository.DeletePortAsync(Id, DeletedBy);
            return Json(result);
        }

        #endregion 
        #region ShippingMaster

        public IActionResult ShippingMaster()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddShipping([FromBody] ShippingRequestDTO request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _shippingRepository.AddShippingAsync(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShipping([FromBody] ShippingRequestDTO request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _shippingRepository.UpdateShippingAsync(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetShippingById(int ID)
        {
            var response = await _shippingRepository.GetShippingByIdAsync(ID);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetShippingList()
        {
            var response = await _shippingRepository.GetShippingListAsync();
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteShipping(int Id)
        {
            int DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _shippingRepository.DeleteShippingAsync(Id, DeletedBy);
            return Json(result);
        }

        #endregion 
        #region MakerMaster

        public IActionResult MakerMaster()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddMaker([FromForm] MakerRequestDTO request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _makerRepository.AddMakerAsync(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMaker([FromForm] MakerRequestDTO request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _makerRepository.UpdateMakerAsync(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMakerById(long id)
        {
            var response = await _makerRepository.GetMakerByIdAsync(id);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMakerList()
        {
            var response = await _makerRepository.GetMakerListAsync();
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMaker(long id)
        {
            int deletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _makerRepository.DeleteMakerAsync(id, deletedBy);             
            return Json(result);
        }

        #endregion 
        #region MODEL MASTER

        [HttpGet]
        public IActionResult ModelMaster()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetModelList()
        {
            var result = await _modelRepository.GetModelListAsync();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetModelById(long id)
        {
            var result = await _modelRepository.GetModelByIdAsync(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddModel([FromBody] ModelRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return Json(new { status = -99, message = "Invalid data" });

            dto.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _modelRepository.AddModelAsync(dto);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateModel([FromBody] ModelRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return Json(new { status = -99, message = "Invalid data" });

            dto.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _modelRepository.UpdateModelAsync(dto);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteModel(long id)
        {
            int deletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _modelRepository.DeleteModelAsync(id, deletedBy);
            return Json(result);
        }

        #endregion


    }
}
