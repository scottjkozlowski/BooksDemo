function showLoadingIndicator() {
    //ConsoleLog("showLoadingIndicator");
    //  Get height of body of this page
    var bodyHeight = getBodyHeight();
    //  Get width of body of this page
    var bodyWidth = getBodyWidth();
    //  Set a 'style' property to a value, for all matched 
    //  elements(id == BackGroundLoadingIndicator).
    $("#BackGroundLoadingIndicator").attr(
                    "style",
                    "display:block;height:" + bodyHeight + "px;width:" + bodyWidth + "px");
    //  Set a 'style' property to a value, for all matched 
    //  elements(id == LoadingIndicatorPanel).
    //  (Set matched element with id == LoadingIndicatorPanel VISIBLE)
    $("#LoadingIndicatorPanel").attr("style", "display:block;");
    //  Set a 'style' property to a value, for all matched 
    //  elements(id == imgLoading).
    //  (Set matched element with id == imgLoading VISIBLE)
    $("#imgLoading").attr("style", "display:block;");
}
/// <summary>
/// This function hides Loading Indicator on the page
/// </summary>
function hideLoadingIndicator() {
    //ConsoleLog("hideLoadingIndicator");
    //  Set a 'style' property to a value, for all matched 
    //  elements(id == LoadingIndicatorPanel).    
    $("#LoadingIndicatorPanel").attr("style", "display:none;");
    //  Set a 'style' property to a value, for all matched 
    //  elements(id == BackGroundLoadingIndicator).
    $("#BackGroundLoadingIndicator").attr("style", "display:none;");
    //  Set a 'style' property to a value, for all matched 
    //  elements(id == imgLoading).
    $("#imgLoading").attr("style", "display:none;");
}
/// <summary>
/// This function returns the calculated width of body 
/// of this page
/// </summary>
function getBodyWidth() {
    //  Initialize width to zero
    var width = 0;
    //  Initialize scrollWidth to zero
    var scrollWidth = 0;
    //  Initialize offsetWidth to zero
    var offsetWidth = 0;
    //  Initialize maxWidth to zero
    var maxWidth = 0;
    //  If document.width has some value then ...
    if (document.width) {
        //  Set width to document.width
        width = document.width;
    } //  If document.body has some value then ...
    else if (document.body) {
        //  If document.body.scrollWidth has some value then ...
        if (document.body.scrollWidth) {
            //  Set width & scrollWidth to document.body.scrollWidth
            width = scrollWidth = document.body.scrollWidth;
        }
        //  If document.body.offsetWidth has some value then ...
        if (document.body.offsetWidth) {
            //  Set width & offsetWidth to document.body.offsetWidth
            width = offsetWidth = document.body.offsetWidth;
        }
        //  If scrollWidth & offsetWidth 
        //  both have some value then ...
        if (scrollWidth && offsetWidth) {
            //  Set width & maxWidth to maximum value 
            //  among scrollWidth & offsetWidth
            width = maxWidth = Math.max(scrollWidth, offsetWidth);
            // If scrollWidth & offsetWidth are different then ...
            if (scrollWidth != offsetWidth) {
                // Manupulate width to cover entire body with mask
                width = width * (1003 / 936);
            }
            // If screen.width has some value then ...
            if (screen.width) {
                //  Set width to maximum value
                //  among width & screen.width
                width = maxWidth = Math.max(width, screen.width);
            }
        }
    }
    //  If browser is Microsoft Internet Explorer then ...
    if (navigator.appName == "Microsoft Internet Explorer" || navigator.appName == "Opera") {
        return width;
    } //  If browser is Netscape then ...
    else if (navigator.appName == "Netscape") {
        //  Set width to maximum value
        //  among screen.width & window.innerWidth
        return Math.max(screen.width, window.innerWidth);
    }
    else {
        return width;
    }
}
/// <summary>
/// This function returns the calculated height of body 
/// of this page
/// </summary>
function getBodyHeight() {
    //  Initialize height to zero
    var height = 0;
    //  Initialize scrollHeight to zero
    var scrollHeight = 0;
    //  Initialize offsetHeight to zero
    var offsetHeight = 0;
    //  Initialize maxHeight to zero
    var maxHeight;
    //  If document.height has some value then ...
    if (document.height) {
        //  Set height to document.height
        height = document.height;
    } //  If document.body has some value then ...
    else if (document.body) {
        //  If document.body.scrollHeight has some value then ...
        if (document.body.scrollHeight) {
            //  Set height & scrollHeight to document.body.scrollHeight
            height = scrollHeight = document.body.scrollHeight;
        }
        //  If document.body.offsetHeight has some value then ...
        if (document.body.offsetHeight) {
            //  Set height & offsetHeight to document.body.offsetHeight
            height = offsetHeight = document.body.offsetHeight;
        }
        //  If scrollHeight & offsetHeight 
        //  both have some value then ...
        if (scrollHeight && offsetHeight) {
            //  Set height & maxHeight to maximum value
            //  among scrollHeight & offsetHeight
            height = maxHeight = Math.max(scrollHeight, offsetHeight);
        }
    }
    //  If browser is Microsoft Internet Explorer then ...
    if (navigator.appName == "Microsoft Internet Explorer") {
        return height;
    } //  If browser is NOT Microsoft Internet Explorer then ...
    else {
        //  width to be returned is the available height
        //  multiplied by
        //  ratio of device-logical YDPIs
        return (window.screen.availHeight) * (screen.deviceYDPI / screen.logicalYDPI);
    }
}