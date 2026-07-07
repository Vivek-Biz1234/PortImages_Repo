$(document).ready(function () {     
    bindDropdowns();          

    const encId = getQueryString("_pid");
    if (encId) {
        $("#hdnProductEncId").val(encId);
        loadProductForEdit(encId);
        $("#btnAddProduct").text("Update");
    }   
});

/* ==============================
   DROPDOWNS (AJAX)
================================*/
function bindDropdowns() {
    bindDropdown("USER", "#ddlClient_PM");
    bindDropdown("SHIP", "#ddlShip_PM");
    bindDropdown("MODEL", "#ddlModel_PM");
    bindDropdown("INVENTORYSTATUS", "#ddlInventoryStatus_PM");
    bindDropdown("VEHICLESTATUS", "#ddlVehicleStatus_PM");
    bindDropdown("INSORGANIZATION", "#ddlINSOrganization_PM");
    bindDropdown("INSDESTINATION", "#ddlINSDestination_PM");
    bindDropdown("INSSTATUS", "#ddlINSStatus_PM");
} 

/* ==============================
   VALIDATION
================================*/
function validateProduct() { 
    debugger;
    if (!validateRequired("ddlClient_PM", ["", "-1"], "Client is required")) return false;
    //if (!validateRequired("ddlShip_PM", ["", "-1"], "Ship is required")) return false;
    if (!validateRequired("ddlModel_PM", ["", "-1"], "Model is required")) return false;

    //if (!validateRequired("ddlInventoryStatus_PM", ["", "-1"], "Inventory Status is required")) return false;
    //if (!validateRequired("ddlVehicleStatus_PM", ["", "-1"], "Vehicle Status is required")) return false;
    //if (!validateRequired("ddlINSOrganization_PM", ["", "-1"], "INS Organization is required")) return false;
    //if (!validateRequired("ddlINSDestination_PM", ["", "-1"], "INS Destination is required")) return false;
    //if (!validateRequired("ddlINSStatus_PM", ["", "-1"], "INS Status is required")) return false;
    if (!validateRequired("txtChassisNo_PM", ["", " "], "Chassis No is required")) return false;    
    return true;
}

/* ==============================
   ADD / UPDATE
================================*/
$(document).on("click", "#btnAddProduct", function () {
    
    if (!validateProduct()) return;        
    let encId = $("#hdnProductEncId").val(); 
    let payload = {
        ID: 0,
        EncID: encId || null,
        ClientId: parseInt($("#ddlClient_PM").val()) || 0,
        ShipId: parseInt($("#ddlShip_PM").val()) || 0,
        ModelId: parseInt($("#ddlModel_PM").val()) || 0,
        InventoryStatusId: parseInt($("#ddlInventoryStatus_PM").val()) || null,
        VehicleStatusId: parseInt($("#ddlVehicleStatus_PM").val()) || null,

        InsOrganizationId: parseInt($("#ddlINSOrganization_PM").val()) || null,
        InsDestinationId: parseInt($("#ddlINSDestination_PM").val()) || null,
        InsStatusId: parseInt($("#ddlINSStatus_PM").val()) || null,
        InsDate: $("#txtINSDate_PM").val() || null,

        ChassisNo: $("#txtChassisNo_PM").val().trim() || "",
        NFCNo: $("#txtNFCNo_PM").val() || "",
        REFNo: $("#txtREFNo_PM").val() || "",

        ContainerNo: $("#txtContainerNo_PM").val() || "",
        VoyageNo: $("#txtVoyageNo_PM").val() || "",

        YardInNo: $("#txtYardInNo_PM").val() || "",
        YardInPlace: $("#txtYardInPlace_PM").val() || "",
        YardInDate: $("#txtYardInDate_PM").val() || null,
        YardOutDate: $("#txtYardOutDate_PM").val() || null,

        ScheduledShippingDate: $("#txtScheduledShippingDate_PM").val() || null,
        ShippingDate: $("#txtShippingDate_PM").val() || null,

        Mileage: parseInt($("#txtMileage_PM").val()) || 0,
        Location: $("#txtLocation_PM").val() || "",

        StoragePeriod: parseInt($("#txtStoragePeriod_PM").val()) || 0,
        InnerCargo: $("#ddlInnerCargo_PM").val() === "1",

        ReasonFailure: $("#txtReasonFailure_PM").val() || "",
        Notes: $("#txtNotes_PM").val() || ""
    };
    let _url = encId ? "/Product/UpdateProductAdditionalInfo" : "/Product/AddProductAdditionalInfo";
    $.ajax({
        url: _url,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(payload),
        success: function (res) {

            let type =
                res.status === 1 ? "success" :
                    res.status === 2 ? "warning" : "error";

            let title =
                res.status === 1 ? "Success" :
                    res.status === 2 ? "Already Exists" : "Error";

            ShowToast(type, title, res.message);

            if (res.status === 1) {
                //if (encId) {removeQueryString();}
                //resetProductForm();
            }
        }
    });

});

$(document).on("click", "#btnClearProduct", function () {
    resetProductForm();
});
 

function loadProductForEdit(encId) {
    $.get("/Product/GetProductByEncId", { pid: encId }, function (res) {
        if (res.status !== 1) {
            ShowToast("error", "Error", res.message);
            return;
        }
        const d = res.data; 
        // Dropdowns
        $("#ddlClient_PM").val(d.clientId == 0 ? -1 : d.clientId).trigger("chosen:updated");
        $("#ddlShip_PM").val(d.shipId == 0 ? -1 : d.shipId).trigger("chosen:updated");
        $("#ddlModel_PM").val(d.modelId == 0 ? -1 : d.modelId).trigger("chosen:updated");
        $("#ddlInventoryStatus_PM").val(d.inventoryStatusId == 0 ? -1 : d.inventoryStatusId).trigger("chosen:updated");
        $("#ddlVehicleStatus_PM").val(d.vehicleStatusId == 0 ? -1 : d.vehicleStatusId).trigger("chosen:updated");
        $("#ddlINSOrganization_PM").val(d.insOrganizationId == 0 ? -1 : d.insOrganizationId).trigger("chosen:updated");
        $("#ddlINSDestination_PM").val(d.insDestinationId == 0 ? -1 : d.insDestinationId).trigger("chosen:updated");
        $("#ddlINSStatus_PM").val(d.insStatusId == 0 ? -1 : d.insStatusId).trigger("chosen:updated");
        $("#ddlInnerCargo_PM").val(d.innerCargo ? "1" : "0").trigger("chosen:updated");

        // Text / Dates
        $("#txtChassisNo_PM").val(d.chassisNo);
        $("#txtNFCNo_PM").val(d.nfcno);
        $("#txtREFNo_PM").val(d.refno);
        $("#txtContainerNo_PM").val(d.containerNo);
        $("#txtVoyageNo_PM").val(d.voyageNo);
        $("#txtYardInNo_PM").val(d.yardInNo);
        $("#txtYardInPlace_PM").val(d.yardInPlace); 
        $("#txtYardInDate_PM").val(formatDateForInput(d.yardInDate));
        $("#txtYardOutDate_PM").val(formatDateForInput(d.yardOutDate));
        $("#txtScheduledShippingDate_PM").val(formatDateForInput(d.scheduledShippingDate));
        $("#txtShippingDate_PM").val(formatDateForInput(d.shippingDate));
        $("#txtINSDate_PM").val(formatDateForInput(d.insDate));

        $("#txtMileage_PM").val(d.mileage);
        $("#txtLocation_PM").val(d.location);
        $("#txtStoragePeriod_PM").val(d.storagePeriod);
        $("#txtReasonFailure_PM").val(d.reasonFailure);
        $("#txtNotes_PM").val(d.notes);

        //$("#lblProductHeader").text("Edit Product");
        //$("#spnProduct").text("Edit Product");

        $("#lblProductHeader").text("Add Product's Additional Info");
        $("#spnProduct").text("Add Product's Additional Info");
        $("#btnAddProduct").text("Update");
    });
}

function resetProductForm() { 
    // Clear hidden ID
    $("#hdnProductEncId").val(0);

    // Clear all text inputs
    $("#txtChassisNo_PM, #txtNFCNo_PM, #txtREFNo_PM, #txtContainerNo_PM, #txtVoyageNo_PM, #txtYardInNo_PM, #txtYardInPlace_PM, #txtMileage_PM, #txtLocation_PM, #txtReasonFailure_PM, #txtStoragePeriod_PM").val("");

    // Clear all date inputs
    $("#txtYardInDate_PM, #txtYardOutDate_PM, #txtScheduledShippingDate_PM, #txtShippingDate_PM, #txtINSDate_PM").val("");

    // Clear textarea
    $("#txtNotes_PM").val("");

    // Reset all dropdowns
    $("#ddlClient_PM, #ddlShip_PM, #ddlModel_PM, #ddlInventoryStatus_PM, #ddlVehicleStatus_PM, #ddlINSOrganization_PM, #ddlINSDestination_PM, #ddlINSStatus_PM, #ddlInnerCargo_PM").val("-1").trigger("chosen:updated");

    $("#lblProductHeader").text("Add Product");
    $("#spnProduct").text("Add Product");

    // Reset button text if using for update/add
    $("#btnAddProduct").text("Submit");

    // Remove any error classes or messages
    $(".input-error").removeClass("input-error");
    $(".error-text").remove();
     
}

