using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Queries;
using System.Security.Claims;

namespace PORTIMAGES.Web.Controllers.Admin
{  
    [Authorize(Roles = "Admin,Staff")]
    [Authorize(AuthenticationSchemes = "StaffScheme")]
    public class MasterController : Controller
    {
        private readonly IMediator _mediator;
        public MasterController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region Terminal Master
        public IActionResult TerminalMaster()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetTerminalList()
        {
            var response = await _mediator.Send(new GetTerminalListQuery());
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTerminalById(int ID)
        {
            var response = await _mediator.Send(new GetTerminalByIdQuery(ID));
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddTerminal([FromBody] AddTerminalCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTerminal([FromBody] UpdateTerminalCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTerminal([FromBody] DeleteTerminalCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        #endregion 
        #region Inventory Status Master
        public IActionResult InventoryStatusMaster()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetInventoryStatusList()
        {
            var response = await _mediator.Send(new GetInventoryStatusListQuery());
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddInventoryStatus([FromBody] AddInventoryStatusCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetInventoryStatusById(int ID)
        {
            var response = await _mediator.Send(new GetInventoryStatusByIdQuery(ID));
            return Json(response);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateInventoryStatus([FromBody] UpdateInventoryStatusCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInventoryStatus([FromBody] DeleteInventoryStatusCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }
        #endregion  
        #region Vehicle's Status

        public IActionResult VehicleStatus()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleStatus([FromBody] AddVehicleStatusCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetVehicleStatusList()
        {
            var response = await _mediator.Send(new GetVehicleStatusListQuery());
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleStatusById(int ID)
        {
            var response = await _mediator.Send(new GetVehicleStatusByIdQuery(ID));
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicleStatus([FromBody] UpdateVehicleStatusCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteVehicleStatus([FromBody] DeleteVehicleStatusCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        #endregion 
        #region INSDestionation

        public IActionResult INSDestionationMaster()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddINSDestionation([FromBody] AddINSDestionationCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetINSDestionationList()
        {
            var response = await _mediator.Send(new GetINSDestinationListQuery());
            return Json(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetINSDestionationById(int ID)
        {
            var response = await _mediator.Send(new GetINSDestinationByIdQuery(ID));
            return Json(response);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateINSDestinationStatus([FromBody] UpdateINSDestinationCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteINSDestination([FromBody] DeleteINSDestinationCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }
         
        #endregion 
        #region INSStatus

        public IActionResult INSStatusMaster()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddINSStatus([FromBody] AddINSStatusCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetINSStatusList()
        {
            var response = await _mediator.Send(new GetINSStatusListQuery());
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetINSStatusById(int ID)
        {
            var response = await _mediator.Send(new GetINSStatusByIdQuery(ID));
            return Json(response);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateINSStatus([FromBody] UpdateINSStatusCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteINSStatus([FromBody] DeleteINSStatusCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        #endregion 
        #region INS Organisation

        public IActionResult INSOrganisation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddINSOrganisationStatus([FromBody] AddINSOrganizationCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetINSOrganisationList()
        {
            var response = await _mediator.Send(new GetINSOrganizationListQuery());
            return Json(response);
        }



        [HttpGet]
        public async Task<IActionResult> GetINSOrganisationById(int ID)
        {
            var response = await _mediator.Send(new GetINSOrganizationByIdQuery(ID));
            return Json(response);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateINSOrganisationStatus([FromBody] UpdateINSOrganizationCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteINSOrganisationStatus([FromBody] DeleteINSOrganizationCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        #endregion 
        #region CountryMaster Created By Akash

        public IActionResult CountryMaster()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody] AddCountryCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountryById(int ID)
        {
            var response = await _mediator.Send(new GetCountryByIdQuery(ID));
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountryList()
        {
            var response = await _mediator.Send(new GetCountryListQuery());
            return Json(response);
        }

        public async Task<IActionResult> DeleteCountry([FromBody] DeleteCountryCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        #endregion 
        #region ShipType

        public IActionResult ShipTypeMaster()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddShipTypeStatus([FromBody] AddShipTypeCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetShipTypeStatusList()
        {
            var response = await _mediator.Send(new GetShipTypeListQuery());
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetShipTypeById(int ID)
        {
            var response = await _mediator.Send(new GetShipTypeByIdQuery(ID));
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShipTypeStatus([FromBody] UpdateShipTypeCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteShipTypeStatus([FromBody] DeleteShipTypeCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        #endregion 
        #region ShipUse


        public IActionResult ShipUseMaster()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddShipUseStatus([FromBody] AddShipUseCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetShipUseList()
        {
            var response = await _mediator.Send(new GetShipUseListQuery());
            return Json(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetShipUseById(int ID)
        {
            var response = await _mediator.Send(new GetShipUseListByIdQuery(ID));
            return Json(response);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateShipUseStatus([FromBody] UpdateShipUseCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteShipUseStatus([FromBody] DeleteShipUseCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        #endregion 
        #region Category Cerated By Akash

        public IActionResult CategoryMaster()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request)
        {
            request.CreatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            var response = await _mediator.Send(new GetCategoryListQuery());
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryByID(int ID)
        {
            var response = await _mediator.Send(new GetCategoryByIdQuery(ID));
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request)
        {
            request.UpdatedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand request)
        {
            request.DeletedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(request);
            return Json(result);
        }

        #endregion

    }
}
