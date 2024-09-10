var dataTable;

$(document).ready(function () {
    loadDataTable();
    closeForm();
});

function loadDataTable() {
    dataTable = $('#booking-table').DataTable({
        "ajax": { url: '/manager/branch/GetAllBooking' },
        "columns": [
            { "data": "userName", "width": "15%", "className": "table-cell" },
            { "data": "tableName", "width": "10%", "className": "table-cell" },
            { "data": "guest", "width": "5%", "className": "table-cell" },
            { "data": "bookingDate", "width": "15%", "className": "table-cell" },
            { "data": "checkinDate", "width": "10%", "className": "table-cell" },
            { "data": "checkinTime", "width": "6%", "className": "table-cell" }
        ]
    });

    //CSS to shorten data when it's too long
    var css = '.table-cell { max-width: 180px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }',
        head = document.head || document.getElementsByTagName('head')[0],
        style = document.createElement('style');

    head.appendChild(style);

    style.type = 'text/css';
    if (style.styleSheet) {
        // This is required for IE8 and below.
        style.styleSheet.cssText = css;
    } else {
        style.appendChild(document.createTextNode(css));
    }
}