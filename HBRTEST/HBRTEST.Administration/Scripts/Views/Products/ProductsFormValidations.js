var ProductsFormValidations = {
    SendProductValidations: function () {
        $("#ProductsForm").validate({
            rules:
            {
                ProductName: { required: true },
                CategoryId: { required: true },
                Description: { required: true },
                Price: { required: true },
                Existence: { required: true }
            },
            messages:
            {
                ProductName: {
                    required: messageManager.setErrorMessage("#ProductNameError",
                        "<p id='ProductNameRequiredError'>Debes escribir el nombre del producto</p>",
                        "#ProductNameRequiredError"),
                },
                CategoryId: {
                    required: messageManager.setErrorMessage("#CategoryIdError",
                        "<p id='CategoryIdRequiredError'>Debes escribir el nombre del producto</p>",
                        "#CategoryIdRequiredError"),
                },
                Description: {
                    required: messageManager.setErrorMessage("#DescriptionError",
                        "<p id='DescriptionRequiredError'>Debes escribir la descripción del producto</p>",
                    "#DescriptionRequiredError")
                },
                Price: {
                    required: messageManager.setErrorMessage("#PriceError",
                        "<p id='PriceRequiredError'>Debes escribir el precio del producto</p>",
                        "#PriceRequiredError")
                },
                Existence: {
                    required: messageManager.setErrorMessage("#Existence",
                        "<p id='ExistenceRequiredError'>Debes escribir la cantidad de existencia</p>",
                        "#ExistenceRequiredError")
                },
            }
        })
    }
}