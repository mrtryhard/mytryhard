var Utils = {
    // Gets the Json Object.
    getJsonData: function (path, callback) {
        var jsonCallback = function (jsonResponse) {
            var data = JSON.parse(jsonResponse);

            if (callback) {
                callback(data);
            }
        };

        Utils.internalGetRawResponse(path, jsonCallback);
    }, // getJsonData

    // Gets the raw data (simple text)
    getRawData: function (objPanel, callback) {
        var path = objPanel.panel;

        if (objPanel.param) {
            path = path + "/?param=" + objPanel.param;
        }

        Utils.internalGetRawResponse(path, callback);
    }, // getRawData

    // Receive raw response - To use internal to Utils 'class' only.
    internalGetRawResponse: function (path, responseCallback) {
        var request = new XMLHttpRequest();

        request.onreadystatechange = function () {
            if (request.readyState === XMLHttpRequest.DONE) {
                if (request.status === 200 || request.status === 0) {
                    if (responseCallback) {
                        if (request.responseText == "") {
                            responseCallback(WidgetUtils.showMessage("error", "Délai d'attente dépassé."));
                        } else {
                            responseCallback(request.responseText);
                        }
                    }
                }
            }
        };

        request.open("GET", path);
        request.send();
    } // internalRawResponseCallback
}; // Utils

var StringUtils = {
    generateSEOUrl: function (strData) {

        var parsed = strData.toLowerCase();
        // Invalid chars
        parsed = parsed.replace(/[^a-z0-9\s-]/g, "");

        // Convert multiple space to one space.
        parsed = parsed.replace(/\s+/g, " ").trim();

        // Cut and trim 
        parsed = parsed.substring(0, parsed.length <= 45 ? parsed.length : 45);

        // Hyphens
        parsed = parsed.replace(/\s/g, "-");

        return parsed;
    }
}; // StringUtils

var WidgetUtils = {
    showLoader: function () {
        return '<div id="loader"></div>';
    },
    // Shows a message
    showMessage: function (type, message) {
        return '<p class="message ' + type + '">' + message + '</p>';
    }
}; // WidgetUtils