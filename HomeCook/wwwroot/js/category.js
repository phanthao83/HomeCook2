var dataTable; 
//Initalize the Table Data  of Category 
$(document).ready(function () {
    loadDataTable(); 
});

function loadDataTable()
{
    dataTable = $("#tblCategory").DataTable(
        {
            "ajax": {
                "url": "/admin/category/GetAll", 
                "type": "GET",
                "datatype" : "json"
            }, 
            "columns": [
                {"data" : "name", "width":"50%"}, 
                { "data": "displayOrder", "width":"20%"},
                {
                    "data": "id", 
                    "render": function (data) {
                        return `<div class ="text-center"> 
                            <a href="/Admin/category/Upsert/${data}"  class="btn btn-success" ><i class="fas fa-edit"></i> Update </a> 
                            &nbsp; 
                            <a onClick= Delete("/Admin/category/Delete/${data}") class="btn btn-danger"><i class="fas fa-trash-alt"></i> Delete </a> 
                               </div > `
                    }, 
                    "width":"30%"

                }
            ], 
            "language": {
                "emptyTable": "Not found any category"
            }

        });

}

function Delete(url)
{
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this item!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes, Delete it',
        closeOnConfirm: true
    }, function ()
         {
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

