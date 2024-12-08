var dataTable;

$(document).ready(function () {
    loadDataTable();
    loadBookingConfirmedTable();
});

function loadDataTable() {
    dataTable = $('#booking-table').DataTable({
        "ajax": { url: '/manager/branch/GetAllBooking' },
        "columns": [
            { "data": "userName", "width": "15%", "className": "table-cell text-center" },
            { "data": "phoneNumber", "width": "10%", "className": "table-cell text-center"},
            { "data": "tableName", "width": "10%", "className": "table-cell text-center" },
            { "data": "guest", "width": "5%", "className": "table-cell text-center" },
            {
                "data": "bookingDate",
                "width": "15%",
                "className": "table-cell text-center",
                "render": function (data, type, row) {
                    var date = new Date(data);
                    return date.toLocaleDateString('en-CA');
                }
            },
            {
                "data": "checkinDate",
                "width": "10%",
                "className": "table-cell text-center",
                "render": function (data, type, row) {
                    var date = new Date(data);
                    return date.toLocaleDateString('en-CA');
                }
            },
            { "data": "checkinTime", "width": "6%", "className": "table-cell text-center" },
            { "data": "status", "width": "6%", "className": "table-cell text-center" }
        ]
    });

    var css = '.table-cell { max-width: 180px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }',
        head = document.head || document.getElementsByTagName('head')[0],
        style = document.createElement('style');

    head.appendChild(style);

    style.type = 'text/css';
    if (style.styleSheet) {
        style.styleSheet.cssText = css;
    } else {
        style.appendChild(document.createTextNode(css));
    }
}

function loadBookingConfirmedTable() {
    dataTable = $('#booking-confirmed').DataTable({
        "ajax": {
            url: '/manager/branch/GetBookingConfirmed',
            dataSrc: function (json) {
                console.log("API response:", json.data);
                return json.data;
            }
        },
        "columns": [
            { "data": "userName", "width": "15%", "className": "table-cell text-center" },
            { "data": "phoneNumber", "width": "10%", "className": "table-cell text-center" },
            { "data": "tableName", "width": "10%", "className": "table-cell text-center" },
            { "data": "guest", "width": "5%", "className": "table-cell text-center" },
            {
                "data": "checkinDate",
                "width": "10%",
                "className": "table-cell text-center",
                "render": function (data, type, row) {
                    var date = new Date(data);
                    return date.toLocaleDateString('en-CA');
                }
            },
            { "data": "checkinTime", "width": "6%", "className": "table-cell text-center" },
            { "data": "status", "width": "6%", "className": "table-cell text-center" }
        ]
    });
}