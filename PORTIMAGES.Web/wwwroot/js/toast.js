function ShowToast(type, title, message) {
    // Remove existing toast if any
    $("#dynamic-toast").remove();

    // Define icons based on type
    let iconClass = {
        success: "ti ti-check",
        error: "ti ti-exclamation-circle",
        warning: "ti ti-alert-triangle",
        info: "ti ti-info-circle"
    }[type] || "ti ti-info-circle";

    // Build Toast HTML Dynamically
    let toastHtml = `
        <div id="dynamic-toast" class="toast-box ${type}">
            <div class="toast-icon"><i class="${iconClass}"></i></div>
            <div class="toast-content">
                <h4>${title}</h4>
                <p>${message}</p>
            </div>
            <button class="toast-close"><i class="ti ti-x"></i></button>
            <div class="toast-timer"></div>
        </div>
    `;

    // Append HTML to body
    $("body").append(toastHtml);

    let toast = $("#dynamic-toast");
    let timer = toast.find(".toast-timer");

    // Play animation
    setTimeout(() => {
        toast.addClass("show");
        timer.addClass("animate");
    }, 50);

    // Auto close
    let autoClose = setTimeout(() => {
        toast.removeClass("show");
        setTimeout(() => toast.remove(), 300);
    }, 4000);

    // Manual close
    toast.find(".toast-close").click(function () {
        clearTimeout(autoClose);
        toast.removeClass("show");
        setTimeout(() => toast.remove(), 300);
    });

}

function ShowConfirmToast(message, onConfirm, title = "Are you sure?") {

    // Remove existing confirmation toast if any
    $("#confirm-toast").remove();

    let confirmHtml = `
        <div id="confirm-toast" class="toast-box confirm">
            <div class="toast-icon"><i class="ti ti-help-octagon"></i></div>
            <div class="toast-content">
                <h4>${title}</h4>
                <p>${message}</p>

                <div class="confirm-buttons">
                    <button id="confirm-yes" class="btn btn-danger btn-sm">Yes</button>
                    <button id="confirm-no" class="btn btn-secondary btn-sm">No</button>
                </div>
            </div>
            <button class="toast-close"><i class="ti ti-x"></i></button>
        </div>
    `;

    $("body").append(confirmHtml);

    let box = $("#confirm-toast");

    // show animation
    setTimeout(() => box.addClass("show"), 50);

    // Handle YES
    $("#confirm-yes").click(function () {
        box.removeClass("show");
        setTimeout(() => box.remove(), 300);
        if (onConfirm) onConfirm();
    });

    // Handle NO
    $("#confirm-no").click(function () {
        box.removeClass("show");
        setTimeout(() => box.remove(), 300);
    });

    // Close button (X)
    box.find(".toast-close").click(function () {
        box.removeClass("show");
        setTimeout(() => box.remove(), 300);
    });
}
