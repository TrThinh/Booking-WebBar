var dataTable;

$(document).ready(function () {
    loadDataTable();

    $('#modalCenter').on('hidden.bs.modal', function () {
        window.location.href = '/admin/managebranch/index';
    });
});

function loadDataTable() {
    dataTable = $('#dinner-table').DataTable({
        "ajax": { url: '/admin/managebranch/getall' },
        "columns": [
            { "data": "time", "width": "15%", "className": "table-cell" },
            { "data": "type", "width": "15%", "className": "table-cell" },
            {
                data: { id: "id" },
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a onclick=editTable('${data.id}') class="btn btn-warning btn-icon-split" data-toggle="modal" data-target="#modalCenter">
                                <span class="text">Edit</span>
                            </a> 
                            <a onclick=deleteTable('${data.id}') class="btn btn-danger btn-icon-split">
                                <span class="text">Delete</span>
                            </a> 
                        </div>
                    `
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


function createTable() {
    // Reset the form
    $('#tableForm').trigger('reset');
    $('#tableFormTitle').text('Create Dinner');
    $('#modalCenter').modal('show');
    $('#tableId').val(0);
    $('#tableTime').val(null);
    $('#tableType').val(null);
}

function editTable(id) {
    // Get the table data by id using an AJAX request
    $.ajax({
        url: '/admin/managebranch/getbyid/' + id,
        type: 'GET',
        success: function (response) {
            var data = response.data;

            $('#tableId').val(data.id);
            $('#tableTime').val(data.time);
            $('#tableType').val(data.type);

            // Show the edit modal
            $('#tableFormTitle').text('Edit Dinner');
            $('#modalCenter').modal('show');
        },
        error: function (xhr, status, error) {
            // Handle the error
            console.log(error);
        }
    });
}

function deleteTable(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this table!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            // Send DELETE request on confirmation
            fetch(`/admin/managebranch/deletebyid/${id}`, {
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
                        // Reload the DataTable after successful deletion
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