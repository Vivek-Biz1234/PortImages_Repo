function getQueryString(name) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(name);
}

function removeQueryString() {
    const cleanUrl = window.location.origin + window.location.pathname;
    window.history.replaceState({}, document.title, cleanUrl);
}

function setButtonState(button, isProcessing = true, processingText = "Processing...") {

    button = $(button);

    if (isProcessing) { 
        if (!button.data("original-html")) {
            button.data("original-html", button.html());
        }

        //button
        //    .prop("disabled", true)
        //    .html(`<i class="ti ti-loader ti-spin"></i> ${processingText}`);
        button
            .prop("disabled", true)
            .html(`<span class="spinner-border spinner-border-sm"></span> ${processingText}`);
    }
    else {

        let originalHtml = button.data("original-html");

        button
            .prop("disabled", false)
            .html(originalHtml);
    }
}

function formatDate(dateString) {

    if (!dateString)
        return "-";

    let date = new Date(dateString);

    return date.toISOString().split('T')[0];
}