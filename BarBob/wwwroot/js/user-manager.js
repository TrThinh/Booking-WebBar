var dataTable;

$(document).ready(function () {
    loadDataTable();
    closeForm();
});

function loadDataTable() {
    dataTable = $('#user-table').DataTable({
        "ajax": { url: '/manager/manageuser/getall' },
        "columns": [
            { "data": "firstName", "width": "15%", "className": "table-cell" },
            { "data": "lastName", "width": "15%", "className": "table-cell" },
            { "data": "email", "width": "10%", "className": "table-cell" },
            { "data": "phoneNumber", "width": "12%", "className": "table-cell" },
            { "data": "birthday", "width": "12%", "className": "table-cell" },
            { "data": "role", "width": "7%", "className": "table-cell" },
            {
                data: { id: "id" },
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <button onclick=deleteUser('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:80px;">
                                <i class="bi bi-trash-fill"></i> Delete
                            </button>
                        </div>
                    `;
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

function closeForm() {
    $('#close-form').click(function () {
        Swal.fire({
            title: 'Are you sure you want to close?',
            text: 'You will be redirected to the Manage User page.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, close'
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = "/Manager/ManageUser";
            }
        });
    });
}

function deleteUser(userId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(`/manager/manageuser/deleteUser/${userId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        Swal.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        );
                        dataTable.ajax.reload();
                    } else {
                        Swal.fire(
                            'Error!',
                            data.message,
                            'error'
                        );
                    }
                })
                .catch(error => {
                    console.error("Error:", error);
                    Swal.fire(
                        'Error!',
                        "An error occurred during deletion",
                        'error'
                    );
                });
        }
    });
}