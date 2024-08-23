$(document).ready(function () {
    var currentUrl = window.location.href;
    $('.nav-item-sidebar a').each(function () {
        var linkUrl = this.href;
        if (currentUrl == linkUrl) {
            $(this).closest('.nav-item-sidebar').addClass('active');
        }
    })
});