$(document).ready(function () {
    $("#SendCategory").click(function () {
        if ($("#CategoryForm").valid()) {
            CategoriesActions.CreateCategory();
        } else {
            $("#CategoryName-error").remove();
            $("#Description-error").remove();
            CategoriesFormValidations.SendCategoryValidation();
        }
    })

    $("#CancelSendCategory").click(function () {
        CategoriesActions.CancelChanges();
    })
}); 

GetCategory = function (categoryId) {
    CategoriesActions.GetCategory(categoryId);
}

DeleteCategory = function (categoryId) {
    CategoriesActions.DeleteCategory(categoryId);
}

