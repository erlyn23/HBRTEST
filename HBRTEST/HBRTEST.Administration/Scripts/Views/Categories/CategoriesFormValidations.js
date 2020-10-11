CategoriesFormValidations = {

    SendCategoryValidation: function () {
        $("#CategoryForm").validate({
            rules: {
                CategoryName: { required: true, maxlength: 30, minlength: 3 },
                Description: { maxlength: 150 }
            },
            messages: {
                CategoryName: {
                    required: messageManager.setErrorMessage("#CategoryNameError",
                        "<p id='CategoryNameRequiredError'>Debes escribir el nombre de la categoría</p>",
                        "#CategoryNameRequiredError"),
                    maxlength: messageManager.setErrorMessage("#CategoryNameError",
                        "<p id='CategoryNameMaxLengthError'>Debes escribir menos de 30 carácteres</p>",
                        "#CategoryNameMaxLengthError"),
                    minlength: messageManager.setErrorMessage("#CategoryNameError",
                        "<p id='CategoryNameMinLengthErrorMessage'>Debes escribir más de 3 carácteres</p>",
                        "#CategoryNameMinLengthErrorMessage")
                },
                Description: {
                    maxlength: messageManager.setErrorMessage("#DescriptionError",
                        "<p id='DescriptionMaxLengthError'>Debes escribir menos de 150 carácteres </p>",
                        "#DescriptionMaxLengthError")
                }
            }
        })
    }
}