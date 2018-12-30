var appUtils = new function () {

    var self = this;

    self.dateFormat = "MM/DD/YYYY";

    self.isNullOrEmpty = function (strVal) { return (strVal === typeof ("undefined") || strVal === "" || strVal == null); };

    self.getUrl = function (action, controller) {
        var area = $("#hidArea").val();
        if (self.isNullOrEmpty(area))
            return baseUrl + controller + "/" + action;
        //returning with area
        return baseUrl + area + "/" + controller + "/" + action;
    };

    self.constructResponse = function () {
        var response = $("#hidResponse").val();
        //validating response
        if (self.isNullOrEmpty(response)) return;
        //clearing response from field
        $("#hidResponse").val("");
        //checking for notifier
        if (response.indexOf("|") === -1) {
            alertify.alert(response);
            return;
        }
        var strArray = response.split("|");
        if (strArray.length === 0) return;
        switch (strArray[0].toLowerCase()) {
            case "success":
                alertify.success(strArray[1]);
                break;
            case "error":
                alertify.error(strArray[1]);
                break;
            default:
                alertify.message(strArray[1]);
                break;
        }
    };

    self.getErrorString = function (errors) {
        if (errors == null || errors.length === 0) return null;
        var tempStr = "";
        $.each(errors, function (index, item) {
            tempStr += item + "<br />";
        });
        return tempStr;
    };

    self.processErrorResponse = function (res) {
        if (res.status === 400) {
            var errMsg = $.isArray(res.responseJSON)
                ? self.getErrorString(res.responseJSON)
                : res.responseText;
            if (!self.isNullOrEmpty(errMsg))
                alertify.error(errMsg);
        } else {
            alertify.error("Unable to process your request.");
        }
    };

    self.showLoader = function (element) {
        $(element).block({ message: "<img src='/images/ajax-loader-m.gif' />" });
    };

    self.hideLoader = function (element) {
        $(element).unblock();
    };

    self.showInputLoader = function (element) {
        $(element).css("background", "url(/images/ajax-loader-s.gif) no-repeat 99% 5px");
    };

    self.hideInputLoader = function (element) {
        $(element).css("background", "none");
    };

    self.getDummyImageUrl = function (dimension) {
        return "https://dummyimage.com/" + dimension + "/cccccc/777777&text=No%20Image";
    };

    self.init = function () {
        "use strict";

        $("input").iCheck({
            checkboxClass: "icheckbox_square-blue",
            radioClass: "iradio_square-blue",
            increaseArea: "20%" // optional
        });
    };
};

$(document).ready(function () {

    alertify.defaults.glossary.title = "Auto Pay";
    alertify.defaults.theme.ok = "btn btn-primary btn-flat btn-sm";
    alertify.defaults.theme.cancel = "btn btn-default btn-flat btn-sm";
    alertify.defaults.transition = "fade";

    alertify.set("notifier", "position", "top-right");

    if ($("body").attr("class").indexOf("public-layout") === -1) {
        $('[data-toggle="control-sidebar"]').controlSidebar();
        $('[data-toggle="push-menu"]').pushMenu();
        $('[data-toggle="tooltip"]').tooltip();
    }

    appUtils.init();
    appUtils.constructResponse();
});