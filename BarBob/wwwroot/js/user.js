var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#user-table').DataTable({
        "ajax": { url: '/admin/manageuser/getall' },
        "columns": [
            { "data": "firstName", "width": "15%", "className": "table-cell" },
            { "data": "lastName", "width": "15%", "className": "table-cell" },
            { "data": "userName", "width": "15%", "className": "table-cell" },
            { "data": "email", "width": "10%", "className": "table-cell" },
            { "data": "phoneNumber", "width": "12%", "className": "table-cell" },
            { "data": "branch.nameBranch", "width": "12%", "className": "table-cell" },
            { "data": "role", "width": "7%", "className": "table-cell" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                        <div class="text-center">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:80px;">
                                <i class="bi bi-lock-fill"></i>  Unlock
                            </a> 
                            <button onclick=deleteUser('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:80px;">
                                <i class="bi bi-trash-fill"></i> Delete
                            </button>
                        </div>
                    `;
                    } else {
                        return `
                        <div class="text-center">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:80px;">
                                <i class="bi bi-unlock-fill"></i>  Lock
                            </a>
                            <button onclick=deleteUser('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:80px;">
                                <i class="bi bi-trash-fill"></i> Delete
                            </button>
                        </div>
                    `;
                    }
                },
                "width": "10%",
                "className": "table-cell"
            }
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