var dataTable;
var productStatus = ""; 
//Initalize the Table Data  of Category 
$(document).ready(function () {
    loadDataTable(null); 
    document.getElementById("btnAll").addEventListener("click", function () { productStatus = ""; dataTable.draw(); }); 
    document.getElementById("btnActive").addEventListener("click", function () { productStatus = "Active"; dataTable.draw(); }); 
    document.getElementById("btnPending").addEventListener("click", function () { productStatus = "Pending"; dataTable.draw(); }); 
    document.getElementById("btnDeleted").addEventListener("click", function () { productStatus = "Deleted"; dataTable.draw(); }); 
});


$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
          var status = data[5]  ; // use data for the age column
        console.log("LoadDataTable with Active status " + productStatus); 
        if (productStatus == "") return true;
        
        if (status == productStatus ) {
            return true;
        }
        return false;
    }
);

function loadDataTable() {
    var productstatus = ''; 
    console.log("LoadDataTable with status "); 
    dataTable = $("#tblProduct").DataTable(
        {
            "ajax": {
                "url": "/supplier/product/GetAll/${productstatus}",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "name", "width": "20%" },
                { "data": "price", "width": "5%"   },
                { "data": "unit.name", "width": "5%" },
                { "data": "category.name", "width": "12%" },
                 {
                    "data": "createDate",
                    "render": function (data) {
                        var datestr = data.toString();
                        var date = datestr.substring(0, 10); 
                        
                        return `${date}`;
                            }, 
                    "width": "10%"
                },
                {
                    "data": "status",
                    "render": function (data) {
                        var description = "";
                        if (data == "P") description = "Pending";
                        if (data == "A") description = "Active";
                        if (data == "D") description = "Deleted"; 
                        return `${description}`; 
                        },
                    "width": "8%"
                },

                {
                    "data": "id",
                    "render": function (data) {

                        return `<div class ="text-center"> 
                            <a href="/supplier/product/Upsert/${data}"  class="btn btn-success btn-sm" ><i class="fas fa-edit"></i> Update </a> 
                            <a onClick= Delete(${data}) class="btn btn-danger btn-sm"><i class="fas fa-trash-alt"></i> Delete </a> 
                            <a href="/supplier/pricing/Upsert/${data}"  class="btn btn-success btn-sm" ><i class="fas fa-edit"></i> Pricing </a> 
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

