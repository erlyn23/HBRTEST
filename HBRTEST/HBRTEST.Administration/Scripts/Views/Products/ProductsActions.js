var ProductActions = {
    SendProduct: function () {
        var json = {
            ProductId: $("#ProductId").val(),
            CategoryId: $("#CategoryId").val(),
            ProductName: $("#ProductName").val(),
            ProductImage: null,
            Description: $("#Description").val(),
            Existence: $("#Existence").val(),
            Price: $("#Price").val(),
            Active: $("#Active").val(),
        }


        var imageData = new FormData();
        var image = $("#ProductImage")[0].files[0];
        imageData.append($("#ProductImage").val(), image);


        $.ajax({
            url: "/Products/UploadImage",
            type: "POST",
            dataType: 'json',
            data: imageData,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data != null) {
                    json.ProductImage = data;
                }
                request.post("/Products/Index", json, proccessResponse.OnSuccessSendProduct);
            },
                error: function (XMLHttpRequest) {
                if (XMLHttpRequest.status != 200) {
                    alert("Ha ocurrido un error");
                }
            }
        })
    },

    CancelSendProduct: function () {
        var json = {
            ProductId: $("#ProductId").val(),
            CategoryId: $("#CategoryId").val(),
            ProductName: $("#ProductName").val(),
            Description: $("#Description").val(),
            Existence: $("#Existence").val(),
            Price: $("#Price").val()
        }
        if (json.ProductId > 0 || json.CategoryId > 0 || json.ProductName.length > 0 || json.Description.length > 0
            || json.Existence.length > 0 || json.Price.length > 0) {
            $("#titleText").text("Registrar producto");
            $("#ProductId").val(0);
            $("#CategoryId").val("");
            $("#ProductName").val("");
            $("#ProductImageContainer").removeClass("d-none");
            $("#Description").val("");
            $("#Existence").val(0);
            $("#Price").val(0);
            $("#Active").attr("checked", false).val(false);
        }
    },

    GetProduct: function (ProductId) {
        var json = { ProductId: ProductId }

        request.post("/Products/GetProductById", json, proccessResponse.OnSuccessGetProduct);
    },

    DeleteProduct: function (ProductId) {
        var json = { ProductId: ProductId };

        var confirmDelete = confirm("¿Estás seguro de querer eliminar este elemento? No podrás recuperarlo");
        if (confirmDelete) {
            request.post("/Products/DeleteProduct", json, proccessResponse.OnSuccessDeleteProduct);
        }
    }
}

var proccessResponse = {
    OnSuccessSendProduct: function (data) {
        alert(data);
        window.location = "/Products/Index";
    },

    OnSuccessGetProduct: function (data) {
        $("#titleText").text("Modificar producto");

        if (data != null) {
            $("#ProductId").val(data.ProductId);
            $("#CategoryId").val(data.CategoryId);
            $("#ProductName").val(data.ProductName);
            $("#Description").val(data.Description);
            $("#Existence").val(data.Existence);
            $("#ProductImageContainer").addClass("d-none");
            $("#Price").val(data.Price);
            $("#Active").attr("checked", data.Active);
        }
    },

    OnSuccessDeleteProduct: function (data) {
        alert(data);
        window.location = "/Products/Index";
    }

}