function DeletePRoduct(data) {
    console.log("Delete " + data);
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
            url: "/Supplier/product/Delete/" + data,
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
function ActivateProduct(data) {
    console.log("Activate product " + data);
    $.ajax({
        type: 'POST',
        url: '/Supplier/product/Activate/' + data,
        success: function (data) {
            if (data.success == true) {
                toastr.success(data.message);
                dataTable.ajax.reload();

            }
            else {
                toastr.error(data.message);

            }


        }
    });
}


