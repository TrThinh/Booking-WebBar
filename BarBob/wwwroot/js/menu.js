var dataTable;

$(document).ready(function () {
    loadDataTable();

    $('#modalCenter').on('hidden.bs.modal', function () {
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    });
});

function loadDataTable() {
    dataTable = $('#menu-data').DataTable({
        "ajax": {
            "url": "/manager/branch/GetAllMenu",
            "dataSrc": "data"
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + 1;
                },
                "width": "5%",
                "className": "table-cell"
            },
            { "data": "type", "width": "8%", "className": "table-cell" },
            { "data": "name", "width": "20%", "className": "table-cell" },
            { "data": "description", "width": "30%", "className": "table-cell" },
            { "data": "price", "width": "15%", "className": "table-cell" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <button onclick="editMenu('${data}')" class="btn btn-warning btn-icon-split" data-toggle="modal" data-target="#modalCenter">
                                <span class="text">Edit</span>
                            </button>
                            <button onclick="deleteTable('${data}')" class="btn btn-danger btn-icon-split">
                                <span class="text">Delete</span>
                            </button>
                        </div>
                    `;
                },
                "width": "23%",
                "className": "table-cell"
            }
        ]
        ,
        "rowCallback": function (row, data, index) {
            var pageInfo = dataTable.page.info();
            $('td:eq(0)', row).html(pageInfo.start + index + 1);
        }
    });
}

function createMenu() {
    // Reset the form
    $('#tableForm').trigger('reset');
    $('#tableFormTitle').text('Add Food & Drink');
    $('#modalCenter').modal('show');
    $('#menuId').val(0);
    $('#menu_type').val(null);
    $('#menu_name').val(null);
    $('#menu_description').val(null);
    $('#menu_price').val(null);
}

function editMenu(id) {
    $.ajax({
        url: '/manager/branch/getitembyid/' + id,
        type: 'GET',
        success: function (response) {
            var data = response.data;
            if (!data) {
                console.error('No data found for the provided id');
                return;
            }

            $('#menuId').val(data.id);
            $('#menu_type').val(data.type);
            $('#menu_name').val(data.name);
            $('#menu_description').val(data.description);
            $('#menu_price').val(data.price);

            $('#tableFormTitle').text('Edit item');
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
        text: "You won't be able to revert this item!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            // Send DELETE request on confirm
            fetch(`/manager/branch/deleteitembyid/${id}`, {
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