$(document).ready(function () {
    AddEventClickCreateCategory();
    AddEventClickCancelCreateCategory();
});

AddEventClickCreateCategory = function () {
    $("#CreateCategory").on("click", function () {
        var categoryObject = {
            CategoryId: $("#CategoryId").val(),
            CategoryName: $("#CategoryName").val(),
            Description: $("#Description").val()
        }
        if (ValidateEmptyOrNullFields(categoryObject.CategoryName)) {
            $("#validationMessageCategoryName").removeClass("d-none").text("Debes insertar el nombre de la categoría");

            setTimeout(function () {
                $("#validationMessageCategoryName").addClass("d-none");
            }, 3000)
        }
        else {
            request.post('/Categories/Index', categoryObject, OnSuccessCreateCategory, OnErrorCreateCategory);
        }
    });
}

AddEventClickCancelCreateCategory = function () {
    $("#CancelCreateUpdate").on("click", function () {
        if ($("#CategoryId").val() > 0 || $("#CategoryName").length > 0 || $("#Description").length > 0) {
            $("#CategoryId").val(0);
            $("#CategoryName").val("");
            $("#Description").val("");
        }
    });
}

ValidateEmptyOrNullFields = function (categoryName) {
    if (categoryName.length <= 0) {
        return true;
    }
    return false;
}

OnSuccessCreateCategory = function (data) {
    alert(data);
    window.location = "/Categories/Index";
}

OnErrorCreateCategory = function (XMLHttpRequest, textStatus, errorMessage) {
    alert("Error al crear categoría: " + textStatus + ". " + errorMessage);
}

DeleteCategory = function (categoryID) {
    if (categoryID <= 0) {
        alert("El id de la categoría debe ser mayor o igual a 1");
    } else {
        var deleteConfirm = confirm("¿Está seguro de eliminar este elemento? No podrá recuperar los datos");

        if (deleteConfirm) {
            var json = { CategoryID: categoryID }
            request.post("/Categories/DeleteCategory", json, OnSuccessDeleteCategory, OnErrorDeleteCategory);
        }
    }
}

OnSuccessDeleteCategory = function (data) {
    alert(data);
    window.location = "/Categories/CategoryHome";
}

OnErrorDeleteCategory = function (XMLHttpRequest, textStatus, errorMessage) {
    alert("Error: " + textStatus + ". " + errorMessage);
}

GetCategory = function (categoryID) {
    if (categoryID <= 0) {
        alert("El id de la categoría debe ser mayor o igual a 1");
    } else {
        $("#titleText").text("Modificar categoría");
        
        var json = { CategoryID: categoryID }
        request.post("/Categories/GetCategoryById", json, OnSuccessGetCategory, OnErrorGetCategory);
    }
}

OnSuccessGetCategory = function (data) {
    if (data != null) {
        $("#CategoryId").val(data.CategoryId);
        $("#CategoryName").val(data.CategoryName);
        $("#Description").val(data.Description);
    }
}

OnErrorGetCategory = function(XMLHttpRequest, textStatus, errorMessage){
    alert("Error: " + textStatus + ". " + errorMessage);
}