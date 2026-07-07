let shipData = [];
let filteredData = [];
let pageSize = 50;
let currentPage = 1;

$(document).ready(function () {
    loadShipList();
    bindDropdowns();
});

/* ==============================
   VALIDATION
================================*/
function validateShip() {

    if (!validateRequired("ddlShipType", ["", "-1"], "Ship Type is required")) return false;
    if (!validateRequired("ddlShippingName", ["", "-1"], "Shipping Name is required")) return false;
    if (!validateRequired("ddlPortfrom", ["", "-1"], "Port From is required")) return false;
    if (!validateRequired("ddlTerminal", ["", "-1"], "Terminal is required")) return false;
    if (!validateRequired("ddlCountry", ["", "-1"], "Country is required")) return false;
    if (!validateRequired("txtShipName", ["", " "], "Ship Name is required")) return false;
    if (!validateRequired("txtDepDate", ["", " "], "Departure Date is required")) return false;
    if (!validateRequired("txtArvlDate", ["", " "], "Arrival Date is required")) return false;
    if (!validateRequired("txtFreight", ["", " "], "Ship Freight is required")) return false;
    if (!validateRequired("txtLoadingCapacity", ["", " "], "Loading Capacity is required")) return false;
    if (!validateRequired("ddlShipUse", ["", "-1"], "Ship Use is required")) return false;
    if (!validateRequired("ddlShipStatus", ["", "-1"], "Status is required")) return false;

    return true;
}

/* ==============================
   ADD / UPDATE
================================*/
$(document).on("click", "#btnShipAdd", function () {
    if (!validateShip()) return;
    debugger;
    let Status = $("#ddlShipStatus").val();
    let _ID = parseInt($("#hdnShipId").val()) || 0;
    let payload = {
        ID: _ID,
        ShipName: $("#txtShipName").val().trim(),
        ShipTypeId: parseInt($("#ddlShipType").val()),
        ShippingId: parseInt($("#ddlShippingName").val()),
        PortId: parseInt($("#ddlPortfrom").val()),
        TerminalId: parseInt($("#ddlTerminal").val()),
        CountryId: parseInt($("#ddlCountry").val()),
        ShipUseId: parseInt($("#ddlShipUse").val()),
        DepDate: $("#txtDepDate").val(),
        ArrDate: $("#txtArvlDate").val(),
        Freight: parseFloat($("#txtFreight").val()),
        LCapacity: parseInt($("#txtLoadingCapacity").val()),
        IsActive: Status === "1",
        CreatedBy: 0,
        UpdatedBy: 0
    };


    let url = payload.ID === 0 ? "/Ship/AddShip" : "/Ship/UpdateShip";

    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(payload),
        success: function (res) {

            let type =
                res.status === 1 ? "success" :
                    res.status === 2 ? "warning" :
                        res.status === -1 ? "info" : "error";

            let title =
                res.status === 1 ? "Success" :
                    res.status === 2 ? "Already Exists" :
                        res.status === -1 ? "Not Found" : "Error";

            if (res.status === 1) {
                resetShipForm();
                loadShipList();
            }

            ShowToast(type, title, res.message);
        }
    });
});

/* ==============================
   LOAD LIST
================================*/
function loadShipList() {
    $.ajax({
        url: "/Ship/GetShipList",
        type: "GET",
        success: function (res) {
            if (res.status === 1) {
                shipData = res.data || [];
                filteredData = shipData;
                currentPage = 1;
                renderTable();
                renderPageInfo();
            }
        }
    });
}

/* ==============================
   TABLE RENDER
================================*/
function renderTable() {

    let start = (currentPage - 1) * pageSize;
    let end = start + pageSize;
    let pageData = filteredData.slice(start, end);

    let tbody = "";

    $.each(pageData, function (i, row) {
        tbody += `
        <tr>
            <td>${start + i + 1}</td>
            <td>${row.shipName}</td>
            <td>${row.shipType}</td>
            <td>${row.shipping}</td>
            <td>${row.port}</td>
            <td>${row.terminal}</td>
            <td>${row.country}</td>
            <td>${row.shipUse}</td>
            <td>${row.depDate}</td>
            <td>${row.arrDate}</td>
            <td>${row.freight}</td>
            <td>${row.lCapacity}</td> 
            <td>${row.createdOn}</td>
                                            <td>${row.createdBy}</td>
           <td>
                                                ${row.isActive
                ? '<span class="text-success bg-success bg-opacity-10 default-badge">Active</span>'
                : '<span class="text-danger bg-danger bg-opacity-10 default-badge">Inactive</span>'
            }
                                            </td>
                                            <td>
                                                <div class="d-flex justify-content-start gap-3">
                                                    <button class="bg-transparent p-0 border-0 hover-text-success btnEdit" data-id="${row.id}" data-title="Edit">
                                                        <i class="ti ti-pencil fs-16"></i>
                                                    </button>
                                                    <button class="bg-transparent p-0 border-0 hover-text-danger btnDelete" data-id="${row.id}" data-title="Delete">
                                                        <i class="ti ti-trash fs-16">delete</i>
                                                    </button>
                                                </div>
                                            </td>
        </tr>`;
    });

    $("#VehicleStatusTable tbody").html(tbody);
    renderPagination();
}

/* ==============================
   PAGINATION
================================*/
function renderPagination() {

    let totalPages = Math.ceil(filteredData.length / pageSize);
    let html = "";

    for (let i = 1; i <= totalPages; i++) {
        html += `
        <li class="page-item">
            <button class="page-link ${i === currentPage ? 'active' : ''}"
                data-page="${i}">
                ${i}
            </button>
        </li>`;
    }

    $("#pagination").html(html);
}

$(document).on("click", ".page-link", function () {
    currentPage = parseInt($(this).data("page"));
    renderTable();
    renderPageInfo();
});
function renderPageInfo() {
    let page = parseInt(currentPage);
    let size = parseInt(pageSize);
    let total = filteredData.length;
    let start = (page - 1) * size + 1;
    let end = page * size;
    if (end > total) { end = total; }
    if (total === 0) {
        $("#lblpagedescription").html(`
                    <span class="text-danger">
                        <i class="ti ti-info-circle"></i>
                        No data found
                    </span>
                `);
        return;
    }
    let desc = `Showing ${start} to ${end} of ${total} entries`;
    $("#lblpagedescription").text(desc);
}

/* ==============================
   SEARCH
================================*/
$(document).on("keyup", "#txtSearchShip", function () {
    let value = $(this).val().toLowerCase().trim();

    filteredData = shipData.filter(row =>
        Object.values(row).some(v =>
            String(v).toLowerCase().includes(value)
        )
    );

    currentPage = 1;
    renderTable();
    renderPageInfo();
});

/* ==============================
   EDIT
================================*/
$(document).on("click", ".btnEdit", function () {
    let id = $(this).data("id");
    $.get("/Ship/GetShipById", { id }, function (res) {
        if (res.status === 1) {
            debugger;
            let d = res.data;
            bindTerminals(d.portId);
            $("#hdnShipId").val(d.id);
            $("#ddlShipType").val(d.shipTypeId).trigger("chosen:updated");
            $("#ddlShippingName").val(d.shippingId).trigger("chosen:updated");
            $("#ddlPortfrom").val(d.portId).trigger("chosen:updated");            
            $("#ddlTerminal").val(d.terminalId).trigger("chosen:updated");
            setTimeout(() => {
                if (d.terminalId) {
                    $("#ddlTerminal").val(d.terminalId).trigger("chosen:updated");
                }
            }, 300);
            $("#ddlCountry").val(d.countryId).trigger("chosen:updated");
            $("#txtShipName").val(d.shipName);
            $("#txtDepDate").val(formatDateForInput(d.depDate));
            $("#txtArvlDate").val(formatDateForInput(d.arrDate));
            $("#txtFreight").val(d.freight);
            $("#txtLoadingCapacity").val(d.lCapacity);
            $("#ddlShipUse").val(d.shipUseId).trigger("chosen:updated");
            $("#ddlShipStatus").val(res.data.isActive ? "1" : "0");
            $("#modalLabelShip").text("Edit Ship");
            $("#btnShipAdd").text("Update");

            new bootstrap.Modal("#frmshipmaster").show();
        }
    });
});

/* ==============================
   DELETE
================================*/
$(document).on("click", ".btnDelete", function () {
    let id = $(this).data("id");   
    ShowConfirmToast("Delete this ship?", () => {
        $.post("/Ship/DeleteShip",
            { Id: id },
            function (res) {
                let type = res.status === 1 ? "success" : res.status === -1 ? "warning" : "error";
                let title = res.status === 1 ? "Deleted!" : res.status === -1 ? "Not Found!" : "Failed!";

                ShowToast(type, title, res.message);

                if (res.status === 1) {
                    loadShipList();
                }
            },
            "json"
        );
    });
});

/* ==============================
   RESET FORM
================================*/

$(document).on("input change", "#frmshipmaster input, #frmshipmaster select", function () {
    $(this).removeClass("input-error");
    $(this).next(".error-text").remove();
});
$(document).on("click", '[data-bs-target="#frmshipmaster"]', function () {
    resetShipForm();
});
$("#frmshipmaster").on("hidden.bs.modal", function () {
    resetShipForm();
});
function resetShipForm() {

    $("#hdnShipId").val(0);
    $("#frmshipmaster input").val("");
    $("#frmshipmaster select").val("-1").trigger("chosen:updated");
    $(".error-text").remove();
    $(".input-error").removeClass("input-error");

    $("#modalLabelShip").text("Add Ship");
    $("#btnShipAdd").text("Submit");

    bootstrap.Modal.getInstance("#frmshipmaster")?.hide();
}

/* ==============================
   DROPDOWNS (AJAX)
================================*/
function bindDropdowns() {
    bindDropdown("SHIPTYPE", "#ddlShipType");
    bindDropdown("SHIPPING", "#ddlShippingName");
    bindDropdown("PORT", "#ddlPortfrom"); 
    //bindDropdown("TERMINAL", "#ddlTerminal");
    bindDropdown("COUNTRY", "#ddlCountry");
    bindDropdown("SHIPUSE", "#ddlShipUse");
} 

$(document).on("change", "#ddlPortfrom", function () {
    let portId = $(this).val();
    bindTerminals(portId);
});
function bindTerminals(portId) {
    if (parseInt(portId) > 0) {
        bindDropdown("TERMINAL", "#ddlTerminal", portId);
    }
}
