$(document).on("click", "#btnValidateVehicleImport", function () {    
    let btn = this;
    let fileInput = $("#fileVehicleImport")[0]; 
    if (fileInput.files.length === 0) {  
        ShowToast("warning", "Warning", "Please select excel file.");
        return;
    }

    let file = fileInput.files[0];
    let extension = file.name.split('.').pop().toLowerCase();

    if (extension !== "xlsx" && extension !== "xls") {

        ShowToast("warning", "Invalid File", "Only .xlsx and .xls files allowed.");
        return;
    }
    let formData = new FormData();
    formData.append("file", file); 
    setButtonState(btn, true, "Validating...");

    $.ajax({ 
        url: "/Import/ValidateVehicleImport",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,

        success: function (res) {
             
            if (res.status !== 1) {

                ShowToast("error", "Error", res.message);
                return;
            }

            // =========================
            // SUMMARY
            // =========================

            $("#lblTotalRows").text(res.data.totalRows);
            $("#lblValidRows").text(res.data.validRows);
            $("#lblInvalidRows").text(res.data.invalidRows);
            $("#lblDuplicateRows").text(res.data.duplicateRows);

            // =========================
            // ERROR TABLE
            // =========================

            let tbody = "";

            if (res.data.errors.length > 0) {

                $("#divNoImportErrors").addClass("d-none");
                $("#tblVehicleImportErrors").closest(".table-responsive").removeClass("d-none");

                $.each(res.data.errors, function (i, item) {

                    tbody += `
                            <tr>
                                <td>${item.rowNumber}</td>
                                <td>${item.columnName}</td>
                                <td>
                                    <span class="text-danger">
                                        ${item.errorMessage}
                                    </span>
                                </td>
                            </tr>
                        `;
                });

                $("#tblVehicleImportErrors tbody").html(tbody);

                $("#btnImportVehicleData").prop("disabled", true);

                ShowToast(
                    "warning",
                    "Validation Failed",
                    "Please fix validation errors."
                );
            }
            else {

                $("#tblVehicleImportErrors tbody").html("");

                $("#divNoImportErrors").removeClass("d-none");

                $("#tblVehicleImportErrors")
                    .closest(".table-responsive")
                    .addClass("d-none");

                $("#btnImportVehicleData").prop("disabled", false);

                ShowToast(
                    "success",
                    "Validated",
                    "File validated successfully."
                );
            }
        },

        error: function () {              
            ShowToast("error", "Error", "Something went wrong.");
        },
        complete: function () {
            setButtonState(btn, false);
        }
    });
});


/* ==============================
   IMPORT FILE
===============================*/
$(document).on("click", "#btnImportVehicleData", function () {
    let btn = this;
    let fileInput = $("#fileVehicleImport")[0];

    if (fileInput.files.length === 0) {

        ShowToast("warning", "Warning", "Please select excel file.");
        return;
    }

    let formData = new FormData();

    formData.append("file", fileInput.files[0]);     
    setButtonState(btn, true, "Importing...");

    $.ajax({

        url: "/Import/ImportVehicleData",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,

        success: function (res) {
            let type =
                res.status === 1 ? "success" :
                    res.status === 2 ? "warning" :
                        "error";

            let title =
                res.status === 1 ? "Success" :
                    res.status === 2 ? "Warning" :
                        "Error";

            ShowToast(type, title, res.message);

            if (res.status === 1) {

                $("#fileVehicleImport").val("");

                $("#lblTotalRows").text("0");
                $("#lblValidRows").text("0");
                $("#lblInvalidRows").text("0");
                $("#lblDuplicateRows").text("0");
                $("#tblVehicleImportErrors tbody").html("");
                $("#btnImportVehicleData").prop("disabled", true);
                $("#divNoImportErrors").addClass("d-none");
            }
        },

        error: function () {
            ShowToast("error", "Error", "Something went wrong.");
        },
        complete: function () {
            setButtonState(btn, false);
        }
    });
});