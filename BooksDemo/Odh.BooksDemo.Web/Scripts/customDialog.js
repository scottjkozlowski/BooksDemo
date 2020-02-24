//<script type="text/javascript">
var alertResult = "Cancel";
var alertWindows;
var alertCallback;

$(function () {
    alertWindows = $("#div_alert_window").kendoWindow({
        actions: ["Close"],
        draggable: true,
        modal: true,
        resizable: false,
        visible: false,
        title: "Action"
    }).data("kendoWindow");
});

function alertClose(btnResult, param) {
    alertResult = btnResult;
    if (alertCallback != null) alertCallback(btnResult, param);
    alertWindows.close();
};
function alertClosewith2(btnResult, param1, param2) {
    alertResult = btnResult;
    if (alertCallback != null) alertCallback(btnResult, param1, param2);
    alertWindows.close();
};

function alertClosewith3(btnResult, param1, param2, param3) {
    alertResult = btnResult;
    if (alertCallback != null) alertCallback(btnResult, param1, param2, param3);
    alertWindows.close();
};

function alertClosewith5(btnResult, param1, param2, param3, param4, param5) {
    alertResult = btnResult;
    if (alertCallback != null) alertCallback(btnResult, param1, param2, param3, param4, param5);
    alertWindows.close();
};

function alertCloseCallBack(e) {
    alertWindows.unbind("close", alertCloseCallBack);
}

function openDialogBox(title, message, type, buttons, theFunction, param) {

    var runTimeData = '<table cellpadding="0" cellspacing="0"><tr><td><div        class="alert_' + type + '"></div></td>' +
         '<td><div class="alert-text">' + message + '</div></td></tr></table><div style="float:right;padding:10px">';

    for (var i in buttons) {
        var s = buttons[i];
        runTimeData += '<input class="k-button" type="button"     onclick="alertClose(\'' + s + '\' , \'' + param + '\' )" value="' + s + '">&nbsp;';
    }

    runTimeData += '</div>';
    alertResult = "Cancel";

    if (theFunction !== undefined) {
        alertCallback = theFunction;
    }
    else {
        alertCallback = null;
    }

    alertWindows.bind("close", alertCloseCallBack);
    alertWindows.title(title);
    alertWindows.center();
    alertWindows.content(runTimeData);
    alertWindows.open();
}

function openDialogBoxWith2(title, message, type, buttons, theFunction, param1, param2) {

    var runTimeData = '<table cellpadding="0" cellspacing="0"><tr><td><div        class="alert_' + type + '"></div></td>' +
         '<td><div class="alert-text">' + message + '</div></td></tr></table><div style="float:right;padding:10px">';

    for (var i in buttons) {
        var s = buttons[i];
        runTimeData += '<input class="k-button" type="button"     onclick="alertClosewith2(\'' + s + '\' , \'' + param1 + '\' , \'' + param2 + '\' )" value="' + s + '">&nbsp;';

    }

    runTimeData += '</div>';
    alertResult = "Cancel";

    if (theFunction !== undefined) {
        alertCallback = theFunction;
    }
    else {
        alertCallback = null;
    }

    alertWindows.bind("close", alertCloseCallBack);
    alertWindows.title(title);
    alertWindows.center();
    alertWindows.content(runTimeData);
    alertWindows.open();
}

function openDialogBoxWith3(title, message, type, buttons, theFunction, param1, param2, param3) {

    var runTimeData = '<table cellpadding="0" cellspacing="0"><tr><td><div        class="alert_' + type + '"></div></td>' +
         '<td><div class="alert-text">' + message + '</div></td></tr></table><div style="float:right;padding:10px">';

    for (var i in buttons) {
        var s = buttons[i];
        runTimeData += '<input class="k-button" type="button"     onclick="alertClosewith3(\'' + s + '\' , \'' + param1 + '\' , \'' + param2 + '\' , \'' + param3 + '\' )" value="' + s + '">&nbsp;';

    }

    runTimeData += '</div>';
    alertResult = "Cancel";

    if (theFunction !== undefined) {
        alertCallback = theFunction;
    }
    else {
        alertCallback = null;
    }

    alertWindows.bind("close", alertCloseCallBack);
    alertWindows.title(title);
    alertWindows.center();
    alertWindows.content(runTimeData);
    alertWindows.open();
}
function openDialogBoxWith5(title, message, type, buttons, theFunction, param1, param2, param3, param4, param5) {

    var runTimeData = '<table cellpadding="0" cellspacing="0"><tr><td><div        class="alert_' + type + '"></div></td>' +
         '<td><div class="alert-text">' + message + '</div></td></tr></table><div style="float:right;padding:10px">';

    for (var i in buttons) {
        var s = buttons[i];
        runTimeData += '<input class="k-button" type="button"     onclick="alertClosewith5(\'' + s + '\' , \'' + param1 + '\' , \'' + param2 + '\' , \'' + param3 + '\' , \'' + param4 + '\' , \'' + param5 + '\'  )" value="' + s + '">&nbsp;';

    }

    runTimeData += '</div>';
    alertResult = "Cancel";

    if (theFunction !== undefined) {
        alertCallback = theFunction;
    }
    else {
        alertCallback = null;
    }

    alertWindows.bind("close", alertCloseCallBack);
    alertWindows.title(title);
    alertWindows.center();
    alertWindows.content(runTimeData);
    alertWindows.open();
}


function DialogSuccess(response, successMessage, selector, duration) {
    
    
    if (selector == null) selector = "#divResult";
    if (duration == null) duration = 2000;
    var resultDiv = $(selector);
    resultDiv.removeAttr('style');
    resultDiv.removeClass();
    if (response !== "Success") {
        resultDiv.addClass("isa_error");
        resultDiv.html(Encoder.htmlDecode(response));
    } else {
        resultDiv.addClass("isa_success");
        resultDiv.css({ "position": "fixed", "left": "50%", "top": "50%", "margin-left": "-200px", "z-index": "100" });
        resultDiv.html(Encoder.htmlDecode(successMessage));
        resultDiv.slideDown(500);
        resultDiv.delay(duration);
        resultDiv.slideUp(500);
    }
    resultDiv.show();
    resultDiv.attr("tabindex", -1).focus();
}
function DialogPopUpSuccess(response, successMessage, selector, duration) {


    if (selector == null) selector = "#divPopupResult";
    if (duration == null) duration = 2000;
    var resultDiv = $(selector);
    resultDiv.removeAttr('style');
    resultDiv.removeClass();
    if (response !== "Success") {
        resultDiv.addClass("isa_error");
        resultDiv.html(Encoder.htmlDecode(response));
    } else {
        resultDiv.addClass("isa_success");
        resultDiv.css({ "position": "fixed", "left": "50%", "top": "50%", "margin-left": "-200px", "z-index": "100" });
        resultDiv.html(Encoder.htmlDecode(successMessage));
        resultDiv.slideDown(500);
        resultDiv.delay(duration);
        resultDiv.slideUp(500);
    }
    resultDiv.show();
    resultDiv.attr("tabindex", -1).focus();
}

