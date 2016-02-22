// ContentLoader
var PartialLoader = {
    internalLoadingTimer: null,

    // Loads the panel.
    loadPanel: function (panelData) {
        PartialLoader.internalTimerStop();
        var panelDom = document.getElementById("admin-container");
        panelDom.innerHTML = panelData;
    },

    timerStart: function () {
        // Insert loading.
        PartialLoader.loadPanel(WidgetUtils.showLoader());
        PartialLoader.internalLoadingTimer = setTimeout(function () {
            // Error on loading timeout.
            PartialLoader.loadPanel(WidgetUtils.showMessage("error", "Délai de requête dépassé."));
        }, 1000 * 10);
    },

    internalTimerStop: function () {
        clearTimeout(PartialLoader.internalLoadingTimer);
        PartialLoader.internalLoadingTimer = null;
    }
}; // ContentLoader

// SideMenu Widget
var SideMenu = {
    // Gets the data and redistribute to PartialLoader.
    onLinkClick: function (event) {
        var target = event.target;
        if (target) {
            SideMenu.internalLoadPanel(target);
        }

        // disable redirecting if we're javascript; 
        return false;
    },

    // Input for search
    onSearchKeyUp: function (event) {
        var target = event.target;
        if (target) {
            if (event.keyCode === 13) { // Enter.
                SideMenu.internalLoadPanel(target);
            }
        }
    },

    // Dispatch
    internalLoadPanel: function (domTarget) {
        var panel = domTarget.getAttribute("data-panel");
        var param = domTarget.getAttribute("data-param");

        var objPanel = { panel: panel, param: param };

        // Insert loader.
        PartialLoader.timerStart();

        if (panel) {
            Utils.getRawData(objPanel, PartialLoader.loadPanel);
        } else {
            PartialLoader.loadPanel(WidgetUtils.showMessage("error", "Le lien est inexistant"));
        }
    },

    // Bind listeners
    bindListeners: function () {
        // Links events
        var nodesList = document.querySelectorAll("section.admin-side a");
        for (var i = 0; i < nodesList.length; ++i) {
            nodesList[i].addEventListener("click", SideMenu.onLinkClick);
        }

        // Search events
        var searchBtn = document.getElementById("btnSubmitSearch");
        if (searchBtn) {
            var searchQuery = document.getElementById("searchquery");
            searchBtn.addEventListener("click", SideMenu.onSearchSubmit);
            searchQuery.addEventListener("keyup", SideMenu.onSearchKeyUp);
        }
    }
};

var AdminUtils = {
    fillSEOFieldEvent: function (target, origin) {
        target = document.getElementById(target);
        origin = document.getElementById(origin);

        var parsed = StringUtils.generateSEOUrl(origin.value);
        target.value = parsed;
    }
};

// Admin things.
var Admin = {
    documentReady: function (event) {
        SideMenu.bindListeners();

        var adminContainer = document.getElementById("admin-container");

        if (adminContainer) {
            // Load default panel if any.
            var defaultPanel = adminContainer.getAttribute("data-def-panel");
            var defaultParam = adminContainer.getAttribute("data-def-param");

            if (defaultPanel) {
                var objPanel = { panel: defaultPanel, param: defaultParam };
                Utils.getRawData(objPanel, PartialLoader.loadPanel);
            }
        }
    }
};

// On page loaded load stuff.
document.addEventListener("DOMContentLoaded", Admin.documentReady, false);