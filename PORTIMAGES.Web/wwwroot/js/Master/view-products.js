let productData = []; // Your model data goes here
let filteredProductData = [];
let pageSize = 50;
let currentPage = 1;
let totalCount = 0;

$(document).ready(function () {
    currentPage = 1;
    loadProductList();
});

function loadProductList() {
    let payload = {       
        pageIndex: currentPage,
        pageSize: pageSize
    };
    $.ajax({
        url: "/Product/GetProductList",
        type: "GET",
        data: payload,
        success: function (res) {
            if (res.status === 1) {
                productData = res.data || [];
                filteredProductData = productData;
                totalCount = filteredProductData[0].totalRows;
                //currentPage = 1;
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
    //let start = (currentPage - 1) * pageSize;
    //let end = start + pageSize;
    //let pageData = filteredProductData.slice(start, end);
    let tbody = "";

    $.each(filteredProductData, function (i, row) { 
        let badgeClass = "bg-secondary";

        if (row.productSource === "ADMIN_PANEL")
            badgeClass = "bg-primary";

        else if (row.productSource === "USER_PANEL")
            badgeClass = "bg-success";

        else if (row.productSource === "API")
            badgeClass = "bg-warning";


        tbody += `
        <tr>
            <td>
                <div class="d-flex justify-content-start gap-3">
                    <button class="bg-transparent p-0 border-0 hover-text-success btnEditProduct" data-id="${row.encID}" data-title="Edit">
                        <i class="ti ti-pencil fs-16"></i>
                    </button>
                    <button class="bg-transparent p-0 border-0 hover-text-danger btnDeleteProduct" data-id="${row.encID}" data-title="Delete">
                        <i class="ti ti-trash fs-16"></i>
                    </button>
                    <a class="bg-transparent p-0 border-0 hover-text-danger" target="_blank" href="/Product/AddPortImage?_pid=${row.encID}" data-id="${row.encID}" data-title="FileUpload">
                        <i class="ti ti-upload fs-16"></i>
                    </a>                    
                </div>
            </td>
            <td>${((currentPage - 1) * pageSize) + i + 1}</td>
            <td>${row.chassisNo}</td> 
            <td>${row.invoicePrice}</td> 
            <td>${row.clientName}</td> 
            <td>${row.shipName}</td> 
            <td>${row.terminalName}</td> 
            <td>${row.modelName}</td> 
            <td>${row.makerName}</td> 
            <td>${row.inventoryStatus}</td> 
            <td>${row.vehicleStatus}</td> 
            <td>${row.insOrganization}</td> 
            <td>${row.insDestination}</td> 
            <td>${row.insStatus}</td> 
            <td>${formatDate(row.insDate)}</td> 
            <td>${row.nfcno || "-"}</td> 
            <td>${row.refno || "-"}</td> 
            <td>${row.yardInNo || "-"}</td> 
            <td>${row.yardInPlace || "-"}</td> 
            <td>${formatDate(row.yardInDate)}</td> 
            <td>${formatDate(row.yardOutDate)}</td> 
            <td>${formatDate(row.scheduledShippingDate)}</td> 
            <td>${formatDate(row.shippingDate)}</td> 
            <td>${row.voyageNo || "-"}</td> 
            <td>${row.storagePeriod || "-"}</td> 
            <td>${row.containerNo || "-"}</td> 
            <td>${row.mileage || "-"}</td> 
            <td>${row.innerCargo ? 'Yes' : 'No'}</td> 
            <td>${row.reasonFailure || "-"}</td> 
            <td>${formatDate(row.createdOn)}</td>
            <td>${row.createdBy || "-"}</td>
            <td>
                <span class="badge ${badgeClass}">${row.productSource}</span>
            </td>
            
           <td>
                                                ${row.isActive
                ? '<span class="text-success bg-success bg-opacity-10 default-badge">Active</span>'
                : '<span class="text-danger bg-danger bg-opacity-10 default-badge">Inactive</span>'
            }
                                            </td> 
        </tr>`;
    });

    $("#ProductTable tbody").html(tbody);
    renderPagination();
}

/* ==============================
   PAGINATION
================================*/
function renderPagination() {
    let totalPages = Math.ceil(totalCount / pageSize);
    let html = "";

    for (let i = 1; i <= totalPages; i++) {
        html += `
        <li class="page-item">
            <button class="page-link ${i === currentPage ? 'active' : ''}" data-page="${i}">${i}</button>
        </li>`;
    }

    $("#pagination").html(html);
}

$(document).on("click", ".page-link", function () {
    currentPage = parseInt($(this).data("page"));
    loadProductList();
    //renderTable();
    //renderPageInfo();
});

function renderPageInfo() {
    let total = totalCount;
    let start = (currentPage - 1) * pageSize + 1;
    let end = currentPage * pageSize;
    if (end > total) end = total;

    if (total === 0) {
        $("#lblpagedescription").html(`
            <span class="text-danger">
                <i class="ti ti-info-circle"></i>
                No data found
            </span>`);
        return;
    }

    $("#lblpagedescription").text(`Showing ${start} to ${end} of ${total} entries`);
}

/* ==============================
   SEARCH
================================*/
$(document).on("keyup", "#txtSearchProduct", function () {
    let value = $(this).val().toLowerCase().trim();

    filteredProductData = productData.filter(row =>
        Object.values(row).some(v =>
            String(v || "").toLowerCase().includes(value)
        )
    );

    currentPage = 1;
    renderTable();
    renderPageInfo();
});

/* ==============================
   EDIT
================================*/
$(document).on("click", ".btnEditProduct", function () {
    const encId = $(this).data("id");

    // redirect securely
    //window.location.href = `/Product/AddProduct?_pid=${encId}`;
    window.location.href = `/Product/AddProductAdditionalInfo?_pid=${encId}`;
});


/* ==============================
   DELETE
================================*/
$(document).on("click", ".btnDeleteProduct", function () {
    let id = $(this).data("id");
    ShowConfirmToast("Delete this product?", () => {
        $.post("/Product/DeleteProduct",
            { Pid: id },
            function (res) {
                let type = res.status === 1 ? "success" : res.status === -1 ? "warning" : "error";
                let title = res.status === 1 ? "Deleted!" : res.status === -1 ? "Not Found!" : "Failed!";

                ShowToast(type, title, res.message);

                if (res.status === 1) {
                    loadProductList();
                }
            },
            "json"
        );
    });
});
