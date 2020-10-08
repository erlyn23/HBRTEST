var CategoriesActions = {
    CreateCategory: function () {
        var categoryObject = {
            CategoryId: $("#CategoryId").val(),
            CategoryName: $("#CategoryName").val(),
            Description: $("#Description").val()
        }
        request.post('/Categories/Index', categoryObject, proccessResponse.OnSuccessCreateCategory);
    },
    CancelChanges: function () {
        if ($("#CategoryId").val() > 0 || $("#CategoryName").length > 0 || $("#Description").length > 0) {
            $("#CategoryId").val(0);
            $("#CategoryName").val("");
            $("#Description").val("");
            $("#titleText").text("Crear categoría");
        }
    },
    DeleteCategory: function (categoryID) {
        if (categoryID <= 0) {
            alert("El id de la categoría debe ser mayor o igual a 1");
        } else {
            var deleteConfirm = confirm("¿Está seguro de eliminar este elemento? No podrá recuperar los datos");

            if (deleteConfirm) {
                var json = { CategoryID: categoryID }
                request.post("/Categories/DeleteCategory", json, proccessResponse.OnSuccessDeleteCategory);
            }
        }
    },
    GetCategory: function (categoryID) {
        if (categoryID <= 0) {
            alert("El id de la categoría debe ser mayor o igual a 1");
        } else {
            $("#titleText").text("Modificar categoría");

            var json = { CategoryID: categoryID }
            request.post("/Categories/GetCategoryById", json, proccessResponse.OnSuccessGetCategory);
        }
    }

}

var proccessResponse = {
    OnSuccessCreateCategory: function (data) {
        alert(data);
        window.location = "/Categories/Index";
    },

    OnSuccessDeleteCategory: function (data) {
        alert(data);
        window.location = "/Categories/Index";
    },

    OnSuccessGetCategory: function (data) {
        if (data != null) {
            $("#CategoryId").val(data.CategoryId);
            $("#CategoryName").val(data.CategoryName);
            $("#Description").val(data.Description);
        }
    }
}