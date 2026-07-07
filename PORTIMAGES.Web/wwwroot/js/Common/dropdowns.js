function bindDropdown(key, ddlSelector, parentId = null, selectedValue = null, atZeroIndex = null, selectedText = null) {
    let url = `/Admin/Common/GetDropdown?key=${key}`;

    if (parentId !== null && parentId !== undefined) {
        url += `&parentId=${parentId}`;
    }

    $.get(url, function (res) {
        let html;
        if (res.status !== 1) {
            console.warn(res.message);
            ShowToast("warning", "Something went wrong", res.message);
            return;
        }
        let defaultValue = (atZeroIndex === 0) ? 0 : -1;
        let defaultText = selectedText ?? "Select";

        if (atZeroIndex === 0) {
            html = `<option value="${defaultValue}">${defaultText}</option>`;
        } else {
            html = `<option value="${defaultValue}">${defaultText}</option>`;
        }

        $.each(res.data, function (i, item) {
            let selected = selectedValue == item.id ? "selected" : "";
            html += `<option value="${item.id}" ${selected}>${item.name}</option>`;
        });

        $(ddlSelector).html(html);
        //$(ddlSelector).chosen({ no_results_text: "Oops, nothing found!" });

        if ($(ddlSelector).data('chosen')) {
            $(ddlSelector).trigger("chosen:updated");
        } else {
            $(ddlSelector).chosen({
                no_results_text: "Oops, nothing found!",
                width: "100%"
            });
        }

    });
}
