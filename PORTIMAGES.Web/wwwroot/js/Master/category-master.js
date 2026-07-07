        let categoryData = [];
        let filteredCategoryData = [];
        let pageSize = 50;
        let currentPage = 1;

        $(document).ready(function () {
            LoadCategory();
        });

        /* ==============================
           VALIDATION
        ================================*/
        function ValidateCategory() {

            if (!validateRequired("txtCategory", ["", "-1"], "Category is required")) return false;
            if (!validateRequired("ddlCategoryStatus", ["", "-1"], "Status is required")) return false;

            return true;
        }

        /* ==============================
           ADD / UPDATE
        ================================*/
        $(document).on("click", "#btnAddCategory", function () {
            if (!ValidateCategory()) return;
            debugger;
            let Status = $("#ddlCategoryStatus").val();
            let _ID = parseInt($("#hdnCategoryId").val()) || 0;
            let payload = {

                ID: _ID,
                CategoryName: $("#txtCategory").val(),
                Titletag: $("#txtCategoryTitle").val(),
                KeywordTag: $("#txtCategoryKeyword").val(),
                Description: $("#txtCategoryDescription").val(),
                IsActive: Status === "1" ? true : false,
                createdBy: 0
            };

            let url = payload.ID == 0 ? "/Master/AddCategory" : "/Master/UpdateCategory";

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
                        resetSubcategoryForm();
                        LoadCategory();
                    }

                    ShowToast(type, title, res.message);
                }
            });
        });

        /* ==============================
           LOAD LIST
        ================================*/
        function LoadCategory() {
            $.ajax({
                url: "/Master/GetCategoryList",
                type: "GET",
                success: function (res) {
                    debugger;
                    if (res.status === 1) {
                        categoryData = res.data || [];
                        filteredCategoryData = categoryData;
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
            let pageData = filteredCategoryData.slice(start, end);

            let tbody = "";

            $.each(pageData, function (i, row) {
                tbody += `
                                 <tr>
                                       <td>${start + i + 1}</td>
                                       <td>${row.categoryName}</td>
                                       <td>${row.titleTag}</td>
                                       <td>${row.keywordTag}</td> 
                                       <td>${row.createdOn}</td>
                                       <td>${row.createdBy}</td>
                                       <td>
                                          ${row.isActive
                        ? '<span class="text-success bg-success bg-opacity-10 default-badge">Active</span>'
                        : '<span class="text-danger bg-danger bg-opacity-10 default-badge">Inactive</span>'}
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

            $("#CategoryTable tbody").html(tbody);
            renderPagination();
        }
        function renderPageInfo() {
            let page = parseInt(currentPage);
            let size = parseInt(pageSize);
            let total = filteredCategoryData.length;
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
            let totalPages = Math.ceil(filteredCategoryData.length / pageSize);
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
        $(document).on("keyup", "#txtSearchCategory", function () {
            let value = $(this).val().toLowerCase().trim();

            filteredCategoryData = categoryData.filter(row =>
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

            $.get("/Master/GetCategoryByID", { id: id }, function (res) {

                if (res.status === 1) {

                    let d = res.data;

                    $("#hdnCategoryId").val(d.id);
                    $("#txtCategory").val(d.categoryName);
                    $("#txtCategoryTitle").val(d.titletag);
                    $("#txtCategoryKeyword").val(d.keywordTag);
                    $("#txtCategoryDescription").val(d.description);
                    $("#ddlCategoryStatus").val(d.isActive ? "1" : "0");

                    $("#modalLabelCategory").text("Edit Category");
                    $("#btnAddCategory").text("Update");

                    new bootstrap.Modal(
                        document.getElementById("fSCmaster")
                    ).show();
                }
            });
        });

        /* ==============================
           DELETE
        ================================*/
        $(document).on("click", ".btnDelete", function () {
            let id = $(this).data("id");

            ShowConfirmToast("Delete this Category?", () => {
                $.ajax({
                    url: "/Master/DeleteCategory",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify({ id: id }),
                    success: function (res) {
                        if (res.status === 1) {
                            LoadCategory();
                        }
                        ShowToast("success", "Deleted", res.message);
                    }
                });
            });
        });

        /* ==============================
           RESET FORM
        ================================*/

        $(document).on("input change", "#fSCmaster input, #fSCmaster select", function () {
            $(this).removeClass("input-error");
            $(this).next(".error-text").remove();
        });
        $(document).on("click", '[data-bs-target="#fSCmaster"]', function () {
            resetSubcategoryForm();
        });
        function resetSubcategoryForm() {

            $("#hdnCategoryId").val(0);
            $("#fSCmaster input").val("");
            $("#fSCmaster select").val("-1");
            $(".error-text").remove();
            $(".input-error").removeClass("input-error");

            $("#modalLabelCategory").text("Add Category");
            $("#btnAddCategory").text("Submit");

            bootstrap.Modal.getInstance("#fSCmaster")?.hide();
        } 