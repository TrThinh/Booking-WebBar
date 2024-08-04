var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#details-table').DataTable({
        "ajax": { url: '/admin/managebranch/GetAllTable' },
        "columns": [
            { "data": "description", "width": "15%", "className": "table-cell" },
            { "data": "tableType.table_name", "width": "15%", "className": "table-cell" },
            {
                data: { id: "id" },
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <button onclick="editTable('${data.id}')" class="btn btn-warning btn-icon-split" data-toggle="modal" data-target="#modalCenter">
                                <span class="text">Edit</span>
                            </button>
                            <button onclick="deleteTable('${data.id}')" class="btn btn-danger btn-icon-split">
                                <span class="text">Delete</span>
                            </button>
                        </div>
                    `;
                },
                "width": "15%",
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


//function createDetailsTable(tableTypeId) {
//    $('#tableDetailsForm').trigger('reset');
//    $('#tableDetailsFormTitle').text('Create Table');
//    $('#modalCenter').modal('show');
//    $('#tableDetailsId').val(null);
//    $('#description').val(null);
//    $('#tableTypeId').val(tableTypeId);
//}

//function deleteDetailsTableById(id) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this table!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            fetch(`/admin/managebranch/deletedetailstablebyid/${id}`, {
//                method: 'DELETE',
//                headers: {
//                    'Content-Type': 'application/json'
//                }
//            })
//                .then(response => response.json())
//                .then(data => {
//                    if (data.success) {
//                        Swal.fire(
//                            'Deleted!',
//                            data.message,
//                            'success'
//                        );
//                        dataTable.ajax.reload();
//                    } else {
//                        Swal.fire(
//                            'Error!',
//                            data.message,
//                            'error'
//                        );
//                    }
//                })
//                .catch(error => {
//                    console.error("Error:", error);
//                    Swal.fire(
//                        'Error!',
//                        "An error occurred during deletion",
//                        'error'
//                    );
//                });
//        }
//    });
//}
