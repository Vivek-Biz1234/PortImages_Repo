let stream = null;

// ================= GLOBAL STATE =================
const allowedTypes = ["image/jpeg", "image/png", "image/jpg", "image/webp"];
const maxSize = 1 * 1024 * 1024; // 1MB

let validFiles = [];     // files to upload
let encIdGlobal = "";
const shutterSound = new Audio('/sounds/camera-click.mp3');

// ================= INIT =================
$(document).ready(function () {

    const encId = getQueryString("_pid");

    if (encId) {
        encIdGlobal = encId;
        $("#hdnProductEncId_UI").val(encId);
        bindDropdowns();         
        loadSavedImages(encId);        
    }
});

// ================= DROPDOWN =================
function bindDropdowns() {
    bindDropdown("WEIGHTUNIT", "#ddlWeightUnit");
    setTimeout(() => {
        if (selectedWeightUnit) {
            $("#ddlWeightUnit").val(selectedWeightUnit).trigger("chosen:updated");
        }
    }, 300);
}
 

// ================= FILE INPUT =================
$("#ImageFile").on("change", function () {

    const files = Array.from(this.files);

    files.forEach(file => {

        // validation
        if (!allowedTypes.includes(file.type)) {
            ShowToast("warning", "Invalid File", file.name + " not allowed");
            return;
        }

        if (file.size > maxSize) {
            ShowToast("warning", "File Too Large", file.name + " > 1MB");
            return;
        }

        validFiles.push(file);

        const reader = new FileReader();
        reader.onload = function (e) {
            addPreview(e.target.result, "local");
        };
        reader.readAsDataURL(file);
    });

    this.value = "";
});

// ================= CAMERA OPEN =================
$(document).on("click", "#btnOpenCamera", async function () {

    try {
        stream = await navigator.mediaDevices.getUserMedia({
            video: true
        });

        $("#cameraStream")[0].srcObject = stream;
        $("#cameraModal").removeClass("d-none");

    } catch (err) {
        ShowToast("error", "Camera Error", err.message);
    }
});

// ================= CAPTURE =================
$(document).on("click", "#btnCapture", function () {

    const video = document.getElementById("cameraStream");

    let canvas = document.createElement("canvas");
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;

    let ctx = canvas.getContext("2d");
    ctx.drawImage(video, 0, 0);

    //Play sound
    shutterSound.currentTime = 0;
    shutterSound.play().catch(() => { });
    canvas.toBlob(function (blob) {

        let file = new File([blob], "camera_" + Date.now() + ".jpg", {
            type: "image/jpeg"
        });

        validFiles.push(file);

        addPreview(URL.createObjectURL(blob), "local");

    }, "image/jpeg");
});

// ================= CLOSE CAMERA =================
$(document).on("click", "#btnCloseCamera", function () {

    if (stream) {
        stream.getTracks().forEach(track => track.stop());
    }

    $("#cameraModal").addClass("d-none");
});

// ================= PREVIEW ADD =================
function addPreview(src, type, imageId = null) {

    $("#imagePreview").removeClass("d-none");

    let html = `
        <div class="col-md-2 col-6 mb-3 image-item ${type}" ${imageId ? `data-id="${imageId}"` : ""}>
            <div class="file-card">               
                <img src="${src}" class="file-preview"/> 
              ${imageId ? `
                    <div class="file-status">
                        <div class="status-icon status-success">✓</div>
                    </div>
                ` : ""}
                <div class="file-actions">
                    <button class="file-action delete ${type}" type="button" ${imageId ? `data-id="${imageId}"` : ""}>
                     ${imageId ? `
                     <i class="ti ti-trash text-danger"></i>
                ` : ` <i class="ti ti-x text-danger"></i>`}                       
                    </button>
                </div>
            </div>
        </div>
    `;

    //$("#imagePreview").append(html);
    if (type === "local") {
        $("#imagePreview").prepend(html); // NEW images on TOP
    } else {
        $("#imagePreview").append(html);  // SAVED images at bottom
    }
}

// ================= DELETE LOCAL =================
$(document).on("click", ".delete.local", function () {

    let index = $(this).closest(".image-item").index(".image-item.local");

    validFiles.splice(index, 1);
    $(this).closest(".image-item").remove();
});

// ================= LOAD SAVED =================
function loadSavedImages(encId) {

    $("#imagePreview").html("");

    $.get("/Product/GetProductImages", { pid: encId }, function (res) {

        if (res.status !== 1) return;

        res.data.forEach(img => {
            addPreview(img.imagePath, "saved", img.imageId);
        });
    });
}

// ================= DELETE SAVED =================
$(document).on("click", ".delete.saved", function () {

    let id = $(this).data("id");

    ShowConfirmToast("Delete this image?", () => {

        $.post("/Product/DeleteProductImage", { Pid: id }, function (res) {

            let type = res.status === 1 ? "success" :
                res.status === -1 ? "warning" : "error";

            if (res.status === 1) {
                loadSavedImages(encIdGlobal);
            }

            ShowToast(type, "Status", res.message);
        });
    });
});
 

// ================= SUBMIT =================
$(document).on("click", "#btnAdd", function () {

    if (validFiles.length === 0 && $("#txtComponentName").val().trim() === "") {
        ShowToast("warning", "Validation", "Add image or component name");
        return;
    }

    let formData = new FormData();

    formData.append("encId", encIdGlobal);
    formData.append("ComponentName", $("#txtComponentName").val());
    formData.append("ComponentWeight", $("#txtComponentWeight").val());
    formData.append("WeightUnit", $("#ddlWeightUnit").val());

    validFiles.forEach(f => {
        formData.append("Pimages", f);
    });

    $.ajax({
        url: "/Product/AddProductImage",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (res) {

            let type = res.status === 1 ? "success" :
                res.status === 2 ? "warning" : "error";

            if (res.status === 1) {
                validFiles = [];
                loadSavedImages(encIdGlobal);
            }

            ShowToast(type, "Status", res.message);
        }
    });
});

$(document).on("click", ".file-preview", function () {
    let src = $(this).attr("src"); 
    $("#previewFullImage").attr("src", src); 
    let modal = new bootstrap.Modal(document.getElementById('imagePreviewModal'));
    modal.show();
}); 