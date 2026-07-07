$(document).ready(function () { 
    bindUserDetails();
});

function validateContols() {            
    if (!validateContactNumber("txtmobile", "Contact number is required !", "Enter a valid contact number !")) return false;
    if (!validateRequired("txtdescription", [], "Enter description !!")) return false;
    return true;
}

$(document).on("input change","#txtmobile,#txtdescription",function () {
        $(this).removeClass("input-error");
        $(this).next(".error-text").remove();
    }
);

function bindUserDetails() {
    $.ajax({
        url: "/User/GetUserById",
        type: "GET", 
        success: function (res) {

            if (res) {
                if (res.status === 1) {
                    $("#txtcompany").val(res.data.company);
                    $("#txtname").val(res.data.name);
                    $("#txtemail").val(res.data.email);
                }
                else {
                    ShowToast("info", "Not Found", res.message);
                }

            }
        }
    });
}

$(document).on("click", "#btnAddInq", function () {
    if (!validateContols()) {
        return;
    }
    debugger;
    let payload = {
        mobileNo: $("#txtmobile").val(),
        description: $("#txtdescription").val()
    };
    $.ajax({
        url: "/User/AddInquiry",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(payload),
        success: function (resp) {

            let type =
                resp.status === 1 ? "success" :
                    resp.status === 2 ? "warning" :
                        "error";

            let title =
                resp.status === 1 ? "Success" :
                    resp.status === 2 ? "Already Exists" :
                        "Error";

            if (resp.status === 1) {
                ResetInquiryForm();

            }
            ShowToast(type, title, resp.message);
        }
    });
});
function ResetInquiryForm() {
    $("#txtmobile,#txtdescription").removeClass("input-error");
    $(".error-text").remove();

    $("#txtmobile").val("");
    $("#txtdescription").val(""); 
    var modalEL = $("#inquiry");
    var modal = bootstrap.Modal.getInstance(modalEL);
    if (modal) modal.hide();
}