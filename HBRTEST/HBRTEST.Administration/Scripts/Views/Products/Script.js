$(document).ready(function () {
    $("#Active").change(function () {
        if ($(this).is(":checked")) {
            $("#Active").val(true);
        } else {
            $("#Active").val(false);
        }
    });

    $("#CreateProduct").on("click", function () {
        if ($("#ProductsForm").valid()) {
            ProductActions.SendProduct();
        } else {
            $("#ProductName-error").remove();
            $("#CategoryId-error").remove();
            $("#Description-error").remove();
            $("#Price-error").remove();
            $("#Existence-error").remove();
            ProductsFormValidations.SendProductValidations();
        }
    });

    $("#CancelCreateUpdateProduct").on("click", function () {
        ProductActions.CancelSendProduct();
    });
});

GetProduct = function (ProductId) {
    ProductActions.GetProduct(ProductId);
}


DeleteProduct = function (ProductId) {
    ProductActions.DeleteProduct(ProductId);
}
