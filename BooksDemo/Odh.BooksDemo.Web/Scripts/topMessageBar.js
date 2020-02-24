var MSG = (function () {
    /**********************************************************************************
    * Used for displaying messages to users.
    * Messages are displayed at the top of the screen and are color * coded based on 
    * messageType. Messages will display for a set amount of time then be removed.
    *
    * - message -> the message to display to users
    * - messageType -> the type of message being displayed. Determines notification styling.
    *       Valid message types are:
    *       - infoMsg - blue background. Used to convey general information. This is the default if no messageType is specified.
    *       - successMsg - green background. Used to convey a successful action i.e. Saved Successfully.
    *       - warningMsg - yellow background. Used to convey potential issues, though not ones that will prevent furthre action.
    *       - errorMsg - red background. Used to convey error conditions that must be resolved before proceeding.
    * - duration -> length of time, in milliseconds, to the message. Defaults to 2000 if no duration is provided.
    ***********************************************************************************/
    function displayMessage(message, messageType, duration) {

        // set a default duration of 2 seconds if no duration is provided
        if (duration === undefined) {
            duration = 2000;
        }

        // Set a default message type/style if none is provided.
        if (messageType === undefined) {
            messageType = 'infoMsg';
        }

        $('#FlashNotification-Message').html(message);

        $('#FlashNotifications')
            .removeClass()
            .addClass(messageType)
            .slideDown(500)
            .delay(duration)
            .slideUp(500);
    }

    function info(message, duration) {
        displayMessage(message, 'isa_info', duration);
    }

    function success(message, duration) {
         DialogSuccess("Success", message, null, duration);
    }

    function warning(message, duration) {
        displayMessage(message, 'isa_warning', duration);
    }

    function error(message, duration) {
        displayMessage(message, 'isa_error', duration);
    }

    return {
        info: info,
        success: success,
        warning: warning,
        error: error
    }
})();
//</script>