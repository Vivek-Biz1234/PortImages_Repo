let terminalData = [];
let pageSize = 100;
let currentPage = 1;
let filteredData = [];

$(document).ready(function () {
    bindDropdowns();
});

function LoadproductList() {
    let terminalid = $("#ddlterminal").val();
    let Insorgainazationid = $("#ddlorganization").val();
    let InsDestinationId = $("#ddldestinations").val();
    let InventoryStatusId = $("#ddlInventoryStatus").val();
    let ModelId = $("#ddlmodel").val();
    let VehicleStatusId = $("#ddlvehiclestatus").val();
    let InsStatusId = $("#ddlINSStatus").val();
    let ShipId = $("#ddlShip").val();
    let YardInDate = $("#txtYardInDate").val();
    let YardOutDate = $("#txtYardOutDate").val();
    let InsDateFrom = $("#txtInsDateFrom").val();
    let InsDateTo = $("#txtInsDateTo").val();
    let ChassisNo = $("#txtChassisNo").val();
    let VoyageNo = $("#txtvoyageNo").val();
    let ContainerNo = $("#txtcontainerNo").val();
    let payload = {
        terminalid: terminalid,
        insorganizationid: Insorgainazationid,
        insdestinationid: InsDestinationId,
        inventorystatusid: InventoryStatusId,
        modelid: ModelId,
        vehiclestatusid: VehicleStatusId,
        insstatusid: InsStatusId,
        shipid: ShipId,
        yardInDate: YardInDate,
        yardOutDate: YardOutDate,
        insDateFrom: InsDateFrom,
        insDateTo: InsDateTo,
        chassisNo: ChassisNo,
        voyageNo: VoyageNo,
        containerNo: ContainerNo
    };

    $.ajax({
        url: "/User/GetProductList",
        type: "GET",
        data: (payload),
        success: function (resp) {
            if (resp.status === 1) {
                filteredData = resp.data || [];
                currentPage = 1;
                RenderTable();
                renderPageInfo();
            }

            let type = resp.status === 1 ? "success" :
                resp.status === 2 ? "warning" : "error";

            let title = resp.status === 1 ? "Success" :
                resp.status === 2 ? "Already Exists" : "Error";

            ShowToast(type, title, resp.message);
        }
    });


}
function RenderTable() {

    let tbody = "";
    let start = (currentPage - 1) * pageSize;
    let end = start + pageSize;
    let pageData = filteredData.slice(start, end);
    debugger;
    $.each(pageData, function (i, row) {
        tbody += `
                                         <tr>
                                                      <td>${start + i + 1}</td>
                                                        <td>${row.chassisNo}</td>
                                                        <td>${row.shipName}</td>
                                                        <td>${row.terminalName}</td>
                                                       <td class="Photo__row">
                                                        ${row.imageCount > 0
            ? `<a class="bg-transparent p-0 border-0 hover-text-danger" target="_blank" href="/User/ProductDetails?_pid=${row.encID}" data-title="View Images (${row.imageCount})">
                                                                <img src=${row.firstImagePath} class="rounded-2">
                                                                </a>`
            : `<button class="bg-transparent p-0 border-0 hover-text-danger" data-title="No Images"><i class="ti ti-eye-off fs-16"></i></button>`
            }
                                                        </td>
                                                        <td>${row.modelName}</td>
                                                        <td>${row.makerName}</td>
                                                        <td>${row.inventoryStatus}</td>
                                                        <td>${row.vehicleStatus}</td>
                                                        <td>${row.insOrganization}</td>
                                                        <td>${row.insDestination}</td>
                                                        <td>${row.insStatus}</td>
                                                        <td>${row.insDate}</td>
                                                        <td>${row.nfcno}</td>
                                                        <td>${row.refno}</td>
                                                        <td>${row.yardInNo}</td>
                                                        <td>${row.yardInPlace}</td>
                                                        <td>${row.yardInDate}</td>
                                                        <td>${row.yardOutDate}</td>
                                                        <td>${row.scheduledShippingDate}</td>
                                                        <td>${row.shippingDate}</td>
                                                        <td>${row.voyageNo}</td>
                                                        <td>${row.storagePeriod}</td>
                                                        <td>${row.containerNo}</td>
                                                        <td>${row.mileage}</td>
                                                        <td>${row.innerCargo ? 'Yes' : 'No'}</td>
                                                        <td>${row.reasonFailure}</td>
                                        </tr >`;


    });
    $("#myTable tbody").html(tbody);
    RenderPagination();
}
function RenderPagination() {
    let totalPages = Math.ceil(filteredData.length / pageSize);
    let pageHtml = "";
    for (let i = 1; i <= totalPages; i++) {
        pageHtml += `
                                                            <li class="page-item">
                                                                <button class="page-link ${i === currentPage ? 'active' : ''} page-btn" data-page="${i}">
                                                                    ${i}
                                                                </button>
                                                            </li>
                                                        `;
    }
    $("#pagination").html(pageHtml);
}
$(document).on("click", ".page-btn", function () {
    currentPage = parseInt($(this).data("page"));
    RenderTable();
    renderPageInfo();
});
function renderPageInfo() {
    let page = parseInt(currentPage);
    let size = parseInt(pageSize);
    let total = filteredData.length;
    let start = (page - 1) * size + 1;
    let end = page * size;
    if (end > total) { end = total };
    if (total === 0) {
        $("#lblpagedescription_myproduct").html(`
                                    <span class="text-danger">
                                        <i class="ti ti-info-circle"></i>
                                        No data found
                                    </span>
                                `);
        return;
    }
    let desc = `Showing ${start} to ${end} of ${total} entries`;
    $("#lblpagedescription_myproduct").text(desc);
}
function bindDropdowns() {
    bindDropdown("TERMINAL", "#ddlterminal", null, null, 0);
    bindDropdown("INSORGANIZATION", "#ddlorganization", null, null, 0);
    bindDropdown("INSDESTINATION", "#ddldestinations", null, null, 0);
    bindDropdown("INVENTORYSTATUS", "#ddlInventoryStatus", null, null, 0);
    bindDropdown("INSSTATUS", "#ddlINSStatus", null, null, 0);
    bindDropdown("SHIP", "#ddlShip", null, null, 0);
    bindDropdown("MODEL", "#ddlmodel", null, null, 0);
    bindDropdown("VEHICLESTATUS", "#ddlvehiclestatus", null, null, 0);
}
$(document).on("click", "#btnSearch_MyProduct", function () {
    LoadproductList();
});
$(document).on("click", "#btnClear_MyProduct", function () {
    clearControls();
});
function clearControls() {
    $("#ddlterminal").val("0");
    $("#ddlorganization").val("0");
    $("#ddldestinations").val("0");
    $("#ddlInventoryStatus").val("0");
    $("#ddlmodel").val("0");
    $("#ddlvehiclestatus").val("0");
    $("#ddlINSStatus").val("0");
    $("#ddlShip").val("0");
    $("#txtYardInDate").val("");
    $("#txtYardOutDate").val("");
    $("#txtInsDateFrom").val("");
    $("#txtInsDateTo").val("");
    $("#txtChassisNo").val("");
    $("#txtvoyageNo").val("");
    $("#txtcontainerNo").val("");
}

$(document).on("click", "#btnDownloadCurrentPage", function () {
    debugger;
    let start = (currentPage - 1) * pageSize;
    let end = start + pageSize;

    let pageData = filteredData.slice(start, end);

    // Only cars with images
    let validCars = pageData.filter(x => x.imageCount > 0);

    if (validCars.length === 0) {
        ShowToast("warning", "No Images", "No images available on this page");
        return;
    }

    let encIDList = validCars.map(x => x.encID);

    $.ajax({
        url: "/User/PrepareImageDownload",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(encIDList),
        xhrFields: {
            responseType: 'blob' // important for receiving binary file
        },
        success: function (res, status, xhr) {
            // Trigger browser download
            let filename = "Photos.zip";
            let blob = new Blob([res], { type: 'application/zip' });
            let link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = filename;
            link.click();
            ShowToast("success", "OK", "Download started");
        },
        error: function () {
            ShowToast("error", "Error", "Request failed");
        }
    });
});


//$(document).on("click", "#btnDownloadCurrentPage", function () {

//    let start = (currentPage - 1) * pageSize;
//    let end = start + pageSize;

//    let pageData = filteredData.slice(start, end);

//    if (pageData.length === 0) {
//        ShowToast("warning", "No Data", "No products on current page");
//        return;
//    }

//    let encIDList = pageData.map(x => x.encID);

//    alert(
//        "Page Size: " + pageSize +
//        "\nCurrent Page: " + currentPage +
//        "\nCars Found: " + encIDList.length +
//        "\n\nChassis:\n" + encIDList.join(", ")
//    );
//});

//$(document).on("click", "#btnDownloadCurrentPage", function () {

//    let start = (currentPage - 1) * pageSize;
//    let end = start + pageSize;

//    let pageData = filteredData.slice(start, end);

//    let validCars = pageData.filter(x => x.imageCount > 0);

//    if (validCars.length === 0) {
//        ShowToast("warning", "No Images", "No images available on this page");
//        return;
//    }

//    let encIDList = validCars.map(x => x.encID);

//    $.ajax({
//        url: "/User/PrepareImageDownload",
//        type: "POST",
//        contentType: "application/json",
//        data: JSON.stringify(encIDList),
//        success: function (res) {
//            ShowToast("success", "OK", res.message);
//        },
//        error: function () {
//            ShowToast("error", "Error", "Request failed");
//        }
//    });
//});

