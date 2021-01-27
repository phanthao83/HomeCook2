var dataTable;
//Initalize the Table Data  of Category 
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblPricingHistory").DataTable(
        {
            "ajax": {
                "url": "/supplier/pricing/GetByProduct/${data}",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "name", "width": "50%" },
                { "data": "displayOrder", "width": "20%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `<div class ="text-center"> 
                            <a href="/Admin/category/Upsert/${data}"  class="btn btn-success" ><i class="fas fa-edit"></i> Update </a> 
                            &nbsp; 
                            <a onClick= Delete("/Admin/category/Delete/${data}") class="btn btn-danger"><i class="fas fa-trash-alt"></i> Delete </a> 
                               </div > `
                    },
                    "width": "30%"

                }
            ],
            "language": {
                "emptyTable": "Not found any category"
            }

        });

}