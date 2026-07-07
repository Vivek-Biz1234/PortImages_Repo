let consigneeData = [];
let filteredConsigneeData = [];
let pageSize = 50;
let currentPage = 1;

$(document).ready(function () {
    LoadConsignee();
});

/* ==============================
   VALIDATION
================================*/
function ValidateConsignee() {

    if (!validateRequired("txtConsigneeName", [""], "Consignee Name is required")) return false;           
    if (!validateEmail("txtConsigneeEmail", "Email is required!", "Enter a valid email address")) return false; 
    if (!validateContactNumber("txtConsigneeContact", "Contact number is required !", "Enter a valid contact number !")) return false;
    if (!validateRequired("txtConsigneeAddress", [""], "Consignee Address is required")) return false; 
    if (!validateRequired("ddlConsigneeStatus", ["", "-1"], "Status is required")) return false; 

    return true;
}

/* ==============================
   ADD / UPDATE
================================*/
$(document).on("click", "#btnAddConsignee", function () {
    if (!ValidateConsignee()) return;
    let Status = $("#ddlConsigneeStatus").val();
    let _ID = parseInt($("#hdnConsigneeId").val()) || 0;
    let payload = {

        ID: _ID, 
        fullName: $("#txtConsigneeName").val().trim(),
        email: $("#txtConsigneeEmail").val().trim(),
        mobile: $("#txtConsigneeContact").val().trim(),
        address: $("#txtConsigneeAddress").val().trim(),
        IsActive: Status === "1" ? true : false,
        createdBy: 0
    };



    let url = payload.ID == 0 ? "/BrokerConsignee/AddConsignee" : "/BrokerConsignee/UpdateConsignee";

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
                resetConsigneeForm();
                LoadConsignee();
            }

            ShowToast(type, title, res.message);
        }
    });
});

/* ==============================
   LOAD LIST
================================*/
function LoadConsignee() {
    $.ajax({
        url: "/BrokerConsignee/GetConsigneeList",
        type: "GET",
        success: function (res) { 
            if (res.status === 1) {
                consigneeData = res.data || [];
                filteredConsigneeData = consigneeData;
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
    let pageData = filteredConsigneeData.slice(start, end);

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

    $("#ConsigneeTable tbody").html(tbody);
    renderPagination();
}
function renderPageInfo() {
    let page = parseInt(currentPage);
    let size = parseInt(pageSize);
    let total = filteredConsigneeData.length;
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
    let totalPages = Math.ceil(filteredConsigneeData.length / pageSize);
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
$(document).on("keyup", "#txtSearchConsignee", function () {
    let value = $(this).val().toLowerCase().trim();

    filteredConsigneeData = consigneeData.filter(row =>
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

    $.get("/BrokerConsignee/GetConsigneeByID", { consigneeId: id }, function (res) {

        if (res.status === 1) { 
            let d = res.data; 
            $("#hdnConsigneeId").val(d.id);
            $("#txtConsigneeName").val(d.fullName); 
            $("#txtConsigneeEmail").val(d.email); 
            $("#txtConsigneeContact").val(d.mobile);  
            $("#txtConsigneeAddress").val(d.address); 
            $("#ddlConsigneeStatus").val(d.isActive ? "1" : "0");

            $("#modalLabelConsignee").text("Edit Consignee");
            $("#btnAddConsignee").text("Update");

            new bootstrap.Modal(
                document.getElementById("fSconsigneemaster")
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

    ShowConfirmToast("Delete this Consignee?", () => {
        $.ajax({
            url: "/BrokerConsignee/DeleteConsignee",
            type: "POST", 
            data: { consigneeId: id },
            success: function (res) {
                if (res.status === 1) {
                    LoadConsignee();
                }
                ShowToast("success", "Deleted", res.message);
            }
        });
    });
});

/* ==============================
   RESET FORM
================================*/

$(document).on("input change", "#fSconsigneemaster input,#fSconsigneemaster textarea, #fSconsigneemaster select", function () {
    $(this).removeClass("input-error");
    $(this).next(".error-text").remove();
});
$(document).on("click", '[data-bs-target="#fSconsigneemaster"]', function () {
    resetConsigneeForm();
});
$("#fSconsigneemaster").on("hidden.bs.modal", function () {
    resetConsigneeForm();
});
function resetConsigneeForm() {

    $("#hdnConsigneeId").val(0);
    $("#fSconsigneemaster input").val("");
    $("#fSconsigneemaster textarea").val("");
    $("#fSconsigneemaster select").val("-1");
    $(".error-text").remove();
    $(".input-error").removeClass("input-error");

    $("#modalLabelConsignee").text("Add Consignee");
    $("#btnAddConsignee").text("Submit");

    bootstrap.Modal.getInstance("#fSconsigneemaster")?.hide();
} 