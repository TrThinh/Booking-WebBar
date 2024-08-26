var dataTable;

$(document).ready(function () {
    loadDataTable();

    $('#modalCenter').on('hidden.bs.modal', function () {
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    });
});

function loadDataTable() {
    dataTable = $('#dinner-table').DataTable({
        "ajax": {
            "url": "/manager/branch/GetAll",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "table_name", "width": "20%", "className": "table-cell" },
            { "data": "description", "width": "30%", "className": "table-cell" },
            { "data": "price", "width": "15%", "className": "table-cell" },
            { "data": "quantity", "width": "15%", "className": "table-cell" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <button onclick="editTable('${data}')" class="btn btn-warning btn-icon-split" data-toggle="modal" data-target="#modalCenter">
                                <span class="text">Edit</span>
                            </button>
                            <button onclick="deleteTable('${data}')" class="btn btn-danger btn-icon-split">
                                <span class="text">Delete</span>
                            </button>
                        </div>
                    `;
                },
                "width": "20%",
                "className": "table-cell"
            }
        ]
    });
}

function createTable() {
    // Reset the form
    $('#tableForm').trigger('reset');
    $('#tableFormTitle').text('Create Dinner');
    $('#modalCenter').modal('show');
    $('#tableId').val(0);
    $('#table_name').val(null);
    $('#description').val(null);
    $('#price').val(null);
    $('#quantity').val(null);
}

function editTable(id) {
    $.ajax({
        url: '/manager/branch/getbyid/' + id,
        type: 'GET',
        success: function (response) {
            var data = response.data;
            if (!data) {
                console.error('No data found for the provided id');
                return;
            }

            $('#tableId').val(data.id);
            $('#table_name').val(data.table_name);
            $('#description').val(data.description);
            $('#price').val(data.price);
            $('#quantity').val(data.quantity);

            $('#tableFormTitle').text('Edit Dinner');
            $('#modalCenter').modal('show');
        },
        error: function (xhr, status, error) {
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
            // Send DELETE request on confirm
            fetch(`/manager/branch/deletebyid/${id}`, {
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
                        // Reload the DataTable after successful delete
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