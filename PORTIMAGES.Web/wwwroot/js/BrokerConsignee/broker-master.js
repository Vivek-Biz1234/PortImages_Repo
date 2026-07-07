let brokerData = [];
let filteredBrokerData = [];
let pageSize = 50;
let currentPage = 1;

$(document).ready(function () {
    LoadBroker();
});

/* ==============================
   VALIDATION
================================*/
function ValidateBroker() {

    if (!validateRequired("txtBrokerName", [""], "Broker Name is required")) return false;
    if (!validateEmail("txtBrokerEmail", "Email is required!", "Enter a valid email address")) return false;
    if (!validateContactNumber("txtBrokerContact", "Contact number is required !", "Enter a valid contact number !")) return false;
    if (!validateRequired("txtBrokerAddress", [""], "Broker Address is required")) return false;
    if (!validateRequired("ddlBrokerStatus", ["", "-1"], "Status is required")) return false;

    return true;
}

/* ==============================
   ADD / UPDATE
================================*/
$(document).on("click", "#btnAddBroker", function () {
    if (!ValidateBroker()) return;
    let Status = $("#ddlBrokerStatus").val();
    let _ID = parseInt($("#hdnBrokerId").val()) || 0;
    let payload = {

        ID: _ID,
        fullName: $("#txtBrokerName").val().trim(),
        email:    $("#txtBrokerEmail").val().trim(),
        mobile:   $("#txtBrokerContact").val().trim(),
        address:  $("#txtBrokerAddress").val().trim(),
        IsActive: Status === "1" ? true : false,
        createdBy: 0
    };



    let url = payload.ID == 0 ? "/BrokerConsignee/AddBroker" : "/BrokerConsignee/UpdateBroker";

    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(payload),
        success: function (res) {
            debugger;
            let type =
                res.status === 1 ? "success" :
                    res.status === 2 ? "warning" :
                        res.status === -1 ? "info" : "error";

            let title =
                res.status === 1 ? "Success" :
                    res.status === 2 ? "Already Exists" :
                        res.status === -1 ? "Not Found" : "Error";

            if (res.status === 1) {
                resetBrokerForm();
                LoadBroker();
            }

            ShowToast(type, title, res.message);
        }
    });
});

/* ==============================
   LOAD LIST
================================*/
function LoadBroker() {
    $.ajax({
        url: "/BrokerConsignee/GetBrokerList",
        type: "GET",
        success: function (res) {
            if (res.status === 1) {
                brokerData = res.data || [];
                filteredBrokerData = brokerData;
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
    let pageData = filteredBrokerData.slice(start, end);

    let tbody = "";

    $.each(pageData, function (i, row) {
        tbody += `
                                 <tr>
                                       <td>${start + i + 1}</td>
                                       <td>${row.fullName}</td> 
                                       <td>${row.email}</td> 
                                       <td>${row.mobile}</td> 
                                       <td>${row.address}</td> 
                                       <td>${row.createdOn}</td>
                                       <td>${row.createdBy}</td>
                                       <td>
                                          ${row.isActive
                ? '<span class="text-success bg-success bg-opacity-10 default-badge">Active</span>'
                : '<span class="text-danger bg-danger bg-opacity-10 default-badge">Inactive</span>'}
                                       </td>
                                          <td>
                                           <div class="d-flex justify-content-start gap-3">
                                                    <button class="bg-transparent p-0 border-0 hover-text-success btnEdit" data-id="${row.encID}" data-title="Edit">
                                                            <i class="ti ti-pencil fs-16"></i>
                                                     </button>
                                                <button class="bg-transparent p-0 border-0 hover-text-danger btnDelete" data-id="${row.encID}" data-title="Delete">
                                                       <i class="ti ti-trash fs-16">delete</i>
                                               </button>
                                          </div>
                                       </td>
                                </tr>`;
    });

    $("#BrokerTable tbody").html(tbody);
    renderPagination();
}
function renderPageInfo() {
    let page = parseInt(currentPage);
    let size = parseInt(pageSize);
    let total = filteredBrokerData.length;
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
   PAGINATION
================================*/
function renderPagination() {
    let totalPages = Math.ceil(filteredBrokerData.length / pageSize);
    let pagHtml = "";

    for (let i = 1; i <= totalPages; i++) {
        pagHtml += `
                                     <li class="page-item">
                                     <button class="page-link ${i === currentPage ? 'active' : ''} page-btn"
                                        data-page="${i}">
                                       ${i}
                                    </button>
                                </li>`;
    }

    $("#pagination").html(pagHtml);
}

$(document).on("click", ".page-link", function () {
    currentPage = parseInt($(this).data("page"));
    renderTable();
    renderPageInfo();
});

/* ==============================
   SEARCH
================================*/
$(document).on("keyup", "#txtSearchBroker", function () {
    let value = $(this).val().toLowerCase().trim();

    filteredBrokerData = brokerData.filter(row =>
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

    $.get("/BrokerConsignee/GetBrokerByID", { brokerId: id }, function (res) {

        if (res.status === 1) {

            let d = res.data;
            $("#hdnBrokerId").val(d.id);
            $("#txtBrokerName").val(d.fullName);
            $("#txtBrokerEmail").val(d.email);
            $("#txtBrokerContact").val(d.mobile);
            $("#txtBrokerAddress").val(d.address);
            $("#ddlBrokerStatus").val(d.isActive ? "1" : "0");

            $("#modalLabelBroker").text("Edit Broker");
            $("#btnAddBroker").text("Update");

            new bootstrap.Modal(
                document.getElementById("fSbrokermaster")
            ).show();
        }
    });
});

/* ==============================
   DELETE
================================*/
$(document).on("click", ".btnDelete", function () {
    debugger;
    let id = $(this).data("id");

    ShowConfirmToast("Delete this Broker?", () => {
        $.ajax({
            url: "/BrokerConsignee/DeleteBroker",
            type: "POST",
            data: { brokerId: id },
            success: function (res) {
                if (res.status === 1) {
                    LoadBroker();
                }
                ShowToast("success", "Deleted", res.message);
            }
        });
    });
});

/* ==============================
   RESET FORM
================================*/

$(document).on("input change", "#fSbrokermaster input,#fSbrokermaster textarea, #fSbrokermaster select", function () {   
    $(this).removeClass("input-error");
    $(this).next(".error-text").remove();
    $(this).closest(".mb-3").find(".alert-icon").addClass("d-none");
});
$(document).on("click", '[data-bs-target="#fSbrokermaster"]', function () {  
    resetBrokerForm();
});
$("#fSbrokermaster").on("hidden.bs.modal", function () {
    resetBrokerForm();
});
function resetBrokerForm() { 
    $("#hdnBrokerId").val(0);
    $("#fSbrokermaster input").val("");
    $("#fSbrokermaster textarea").val("");
    $("#fSbrokermaster select").val("-1");
    $(".error-text").remove();
    $(".alert-icon").addClass("d-none");
    $(".input-error").removeClass("input-error");

    $("#modalLabelBroker").text("Add Broker");
    $("#btnAddBroker").text("Submit");

    bootstrap.Modal.getInstance("#fSbrokermaster")?.hide();
} 