var dataTable;
//Initalize the Table Data  of Category 
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblCategory").DataTable(
        {
            "ajax": {
                "url": "/admin/unit/GetAll",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "name", "width": "30%" },
                { "data": "category.name", "width": "40%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `<div class ="text-center"> 
                            <a href="/Admin/unit/Upsert/${data}"  class="btn btn-success" ><i class="fas fa-edit"></i> Update </a> 
                            &nbsp; 
                            <a onClick= Delete("/Admin/unit/Delete/${data}") class="btn btn-danger"><i class="fas fa-trash-alt"></i> Delete </a> 
                               </div > `
                    },
                    "width": "30%"

                }
            ],
            "language": {
                "emptyTable": "Not found any unit"
            }

        });

}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this item!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes, Delete it',
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success == true) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();

                }
                else {
                    toastr.error(datat.message);

                }


            }
        });

    }
    );
}

