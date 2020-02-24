function setFormDefaultValues(frm) {
    var obj = null;

    if (typeof frm === "undefined" || frm === null) {
        obj = $("#form0")[0];
    } else {
        obj = $("#" + frm)[0];
    }

    //var obj = $("#form0")[0];
    if (obj == null) {
        return;
    }

    for (var i = 0; i < obj.elements.length; i++) {
        var control = obj.elements[i];
        if ((control.type === "text" || control.type === "textarea") && control.className !== "k-formatted-value k-input") {
            control.defaultValue = control.value;
        }

        if (control.type == "checkbox" || control.type == "radio") {
            control.defaultChecked = control.checked;
        }
        else if (control.type == "select-one" || control.type == "select-multiple") {
            for (var j = 1; j < control.options.length; j++) {
                control.options[j].defaultSelected = control.options[j].selected;
            }
        }
    }
}

function hasTheFormChanged(frm) {
    //debugger;
    var isDirty = false;
    var obj = null;

    if (typeof frm === "undefined" || frm === null) {
        obj = $("#form0")[0];
    } else {
        obj = $("#" + frm)[0];
    }

    //var obj = $("#form0")[0];
    if (obj == null) { return isDirty; }

    for (var i = 0; i < obj.elements.length; i++) {
        var control = obj.elements[i];
        if ((control.type == "text" || control.type == "textarea") && control.className != "k-formatted-value k-input") {
            if (control.value.trim() != control.defaultValue.trim()) {
                isDirty = true;
                var d = new Date(control.defaultValue);
                if (!isNaN(d.valueOf())) {
                    var nd = new Date(control.value);
                    if (!isNaN(nd.valueOf())) {
                        if (d.valueOf() == nd.valueOf()) { isDirty = false; }
                    }
                }
            }
        }

        if (control.type == "checkbox" || control.type == "radio") {
            if (control.checked != control.defaultChecked) {
                isDirty = true;
            }
        }
        else if (control.type == "select-one" || control.type == "select-multiple") {
            for (var j = 1; j < control.options.length; j++) {
                if (control.options[j].selected != control.options[j].defaultSelected) {
                    isDirty = true;
                }
            }
        }
        if (isDirty) break;
    }
    return isDirty;
}


function confirmFormDirtyOk() {
    if (confirm("Your changes are not saved. Do you wish to proceed with out saving your changes?")) {
        return true;
    } else {
        return false;
    }

}

function checkDirtyFlag(frm) {
    //debugger;
    if (hasTheFormChanged(frm)) {
        return confirmFormDirtyOk();
    }
    return true;
}