$(document).ready(function () {
    AddEventClickCreateUpdateProduct();
    AddEventClickCancelCreateProduct();
    AddEventClickSearchProduct();
});

AddEventClickCreateUpdateProduct = function () {
    $("#CreateProduct").on("click", function () {
        var json = {
            ProductId: $("#ProductId").val(),
            CategoryId: $("#CategoryId").val(),
            ProductName: $("#ProductName").val(),
            Description: $("#Description").val(),
            Existence: $("#Existence").val(),
            Price: $("#Price").val(),
            Creation_Date: $("#Creation_Date").val(),
            Expire_Date: $("#Expire_Date").val()
        }

        if (ValidateEmptyOrNullFields(json.CategoryId, json.ProductName, json.Existence, json.Description)) {
            $("#ValidationMessage").removeClass("d-none").text("Debes insertar un nombre de producto, una descripción, una categoría y una existencia");

            setTimeout(function () {
                $("#ValidationMessage").addClass("d-none");
            }, 3000)
        } else {
            request.post("/Products/Index", json, OnSuccessCreateUpdateProduct, OnErrorCreateUpdateProduct);
        }
    });
}

ValidateEmptyOrNullFields = function (categoryId, productName, existence, description) {
    if (categoryId <= 0 || productName.length <= 0 || existence <= 0 || description.length <= 0) {
        return true;
    }
    return false;
}

OnSuccessCreateUpdateProduct = function (data) {
    alert(data);
    window.location = "/Products/Index";
}

OnErrorCreateUpdateProduct = function (XMLHttpRquest, textStatus, errorMessage) {
    alert("Error: " + textStatus + ". " + errorMessage);
}

AddEventClickCancelCreateProduct = function () {
    $("#CancelCreateUpdateProduct").on("click", function () {
        var json = {
            ProductId: $("#ProductId").val(),
            CategoryId: $("#CategoryId").val(),
            ProductName: $("#ProductName").val(),
            Description: $("#Description").val(),
            Existence: $("#Existence").val(),
            Price: $("#Price").val(),
            Creation_Date: $("#Creation_Date").val(),
            Expire_Date: $("#Expire_Date").val()
        }
        if (json.ProductId > 0 || json.CategoryId > 0 || ProductName.length > 0 || Description.length > 0 || Existence.length > 0 || Price.length > 0 || Creation_Date.length > 0 || Expire_Date.length > 0) {
            $("#ProductId").val(0);
            $("#CategoryId").val("");
            $("#ProductName").val("");
            $("#Description").val("");
            $("#Existence").val(0);
            $("#Price").val(0);
            $("#Creation_Date").val("dd/mm/yyyy");
            $("#Expire_Date").val("dd/mm/yyyy");
        }
    });
}

AddEventClickSearchProduct = function () {
    $("#Search").on("keyup", function () {
        var json = { filter: $("#Filter").val(), search: $("#Search").val() }
        request.post("/Products/FilterProducts", json, OnSuccessSearchProduct, OnErrorSearchProduct);
    });
}

OnSuccessSearchProduct = function (data) {
    if (data != null) {
        $("#tableData").children("#tableContent").remove();
        $("#tableData").append("<tbody id='tableContent'></tbody>");
        for (var item in data) {
            $("#tableContent").append("<tr id='contentRow" + data[item].ProductId + "'></tr>");
            $("#contentRow" + data[item].ProductId).append("<td>" + data[item].ProductId + "</td>");

            $("#contentRow" + data[item].ProductId).append("<td>" + data[item].ProductName + "</td>");
            $("#contentRow" + data[item].ProductId).append("<td>" + data[item].CategoryName + "</td>");
            $("#contentRow" + data[item].ProductId).append("<td>" + data[item].Price + "</td>");
            $("#contentRow" + data[item].ProductId).append("<td id='actionColumn" + data[item].ProductId + "'></td>");
            $("#actionColumn" + data[item].ProductId).append("<a class='btn btn-success' href='/Products/DetailsProduct?ProductId=" + data[item].ProductId + "'><i class='fas fa-eye'></i></a>&nbsp;")
            $("#actionColumn" + data[item].ProductId).append("<button type='button' class='btn btn-primary' onclick='GetProduct(" + data[item].ProductId + ")'><i class='fas fa-edit'></i></button>&nbsp;");
            $("#actionColumn" + data[item].ProductId).append("<button type='button' class='btn btn-danger' onclick='DeleteProduct(" + data[item].ProductId + ")'><i class='fas fa-trash'></i></button>");

        }
    }
}

OnErrorSearchProduct = function (XMLHttpRequest, textStatus, errorMessage) {
    alert("Error: " + textStatus + ". " + errorMessage);
}

GetProduct = function (ProductId) {
    var json = { ProductId: ProductId }

    request.post("/Products/GetProductById", json, OnSuccessGetProduct, OnErrorGetProduct);
}

OnSuccessGetProduct = function (data) {
    $("#titleText").text("Modificar producto");

    if (data != null) {
        $("#ProductId").val(data.ProductId);
        $("#CategoryId").val(data.CategoryId);
        $("#ProductName").val(data.ProductName);
        $("#Description").val(data.Description);
        $("#Existence").val(data.Existence);
        $("#Price").val(data.Price);
        $("#Creation_Date").val(data.Creation_Date);
        $("#Expire_Date").val(data.Expire_Date);
    }
}

OnErrorGetProduct = function (XMLHttpRquest, textStatus, errorMessage) {
    alert("Error: " + textStatus + ". " + errorMessage);
}


DeleteProduct = function (ProductId) {
    var json = { ProductId: ProductId };

    var confirmDelete = confirm("¿Estás seguro de querer eliminar este elemento? No podrás recuperarlo");
    if (confirmDelete) {
        request.post("/Products/DeleteProduct", json, OnSuccessDeleteProduct, OnErrorDeleteProduct);
    }
}

OnSuccessDeleteProduct = function (data) {
    alert(data);
    window.location = "/Products/Index";
}

OnErrorDeleteProduct = function (XMLHttpRquest, textStatus, errorMessage) {
    alert("Error: " + textStatus + ". " + errorMessage);
}