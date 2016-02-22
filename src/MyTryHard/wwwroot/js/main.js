// Menu things.
var Menu = {
    // Fires when menus clicked
    onClick: function (event) {
        var target = event.target;
        if (target) {
            var href = target.getAttribute("data-href");
            window.location = href;
        }
    },

    // Adds the listeners
    bindListeners: function () {
        var nodesList = document.querySelectorAll("header nav ul li");
        for (var i = 0; i < nodesList.length; ++i) {
            nodesList[i].addEventListener("click", Menu.onClick);
        }
    },
};

// Common things.
var Common = {
    documentReady: function (event) {
        Menu.bindListeners();
    }
};

// When page is loaded.
document.addEventListener("DOMContentLoaded", Common.documentReady, false);