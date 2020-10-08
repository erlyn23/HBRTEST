var UsersFormValidations = {
    SignInValidation: function () {
        $("#LoginForm").validate({
            rules: {
                UserName: { required: true },
                Password: { required: true }
            },
            messages: {
                UserName: {
                    required: messageManager.setErrorMessage("#errorMessageUserName",
                        "<p id='validMessageUserName'>Debes escribir tu nombre de usuario.</p>",
                        "#validMessageUserName")
                },
                Password: {
                    required: messageManager.setErrorMessage("#errorMessagePassword",
                        "<p id='validMessagePass'>Debes escribir tu contraseña.</p>",
                        "#validMessagePass")
                }
            }
        });
    },

    CreateUserValidation: function () {
        $("#CreateUserForm").validate({
            rules: {
                FirstName: { required: true, minlength: 3, maxlength: 30 },
                LastName: { required: true, minlength: 3, maxlength: 30 },
                CellPhone: { required: true, minlength: 10, maxlength: 10 },
                Genre: { required: true },
                Email: { required: true, email: true },
                UserName: { required: true, minlength: 3, maxlength: 30 },
                Password: { required: true }
            },
            messages: {
                FirstName: {
                    required: messageManager.setErrorMessage("#errorMessageFirstName",
                        "<p id='FirstNameRequiredError'>Debes insertar tu nombre</p>",
                        "#FirstNameRequiredError"),
                    minlength: messageManager.setErrorMessage("#errorMessageFirstName",
                        "<p id='FirstNameMinLengthError'>Debes insertar al menos 3 letras</p>",
                        "#FirstNameMinLengthError"),
                    maxlength: messageManager.setErrorMessage("#errorMessageFirstName",
                        "<p id='FirstNameMaxLengthError'>Debes insertar solo 30 letras</p>",
                        "#FirstNameMaxLengthError"),
                },
                LastName: {
                    required: messageManager.setErrorMessage("#errorMessageLastName",
                        "<p id='LastNameRequiredError'>Debes insertar tu apellido</p>",
                        "#LastNameRequiredError"),
                    minlength: messageManager.setErrorMessage("#errorMessageLastName",
                        "<p id='LastNameMinLengthError'>Debes insertar al menos 3 letras</p>",
                        "#LastNameMinLengthError"),
                    maxlength: messageManager.setErrorMessage("#errorMessageLastName",
                        "<p id='LastNameMaxLengthError'>Debes insertar solo 30 letras</p>",
                        "#LastNameMaxLengthError"),
                },
                CellPhone: {
                    required: messageManager.setErrorMessage("#errorMessageCellPhone",
                        "<p id='CellPhoneRequiredError'>Debes insertar tu teléfono</p>",
                        "#CellPhoneRequiredError"),
                    minlength: messageManager.setErrorMessage("#errorMessageCellPhone",
                        "<p id='CellPhoneMinLengthError'>Debes insertar solo 10 números</p>",
                        "#CellPhoneMinLengthError"),
                    maxlength: messageManager.setErrorMessage("#errorMessageCellPhone",
                        "<p id='CellPhoneMaxLengthError'>Debes insertar solo 10 números</p>",
                        "#CellPhoneMaxLengthError"),
                },
                Genre: {
                    required: messageManager.setErrorMessage("#errorMessageGenre",
                        "<p id='GenreRequiredError'>Debes escoger un género</p>",
                        "#GenreRequiredError"),
                },
                Email: {
                    required: messageManager.setErrorMessage("#errorMessageEmail",
                        "<p id='EmailRequiredError'>Debes insertar tu email</p>",
                        "#EmailRequiredError"),
                    email: messageManager.setErrorMessage("#errorMessageEmail",
                        "<p id='EmailPatternError'>Debes insertar un email válido</p>",
                        "#EmailPatternError"),
                },
                UserName: {
                    required: messageManager.setErrorMessage("#errorMessageUserName",
                        "<p id='UserNameRequiredError'>Debes insertar tu nombre de usuario</p>",
                        "#UserNameRequiredError"),
                    minlength: messageManager.setErrorMessage("#errorMessageUserName",
                        "<p id='UserNameMinLengthError'>Debes insertar al menos 3 letras</p>",
                        "#UserNameMinLengthError"),
                    maxlength: messageManager.setErrorMessage("#errorMessageUserName",
                        "<p id='UserNameMaxLengthError'>Debes insertar solo 30 letras</p>",
                        "#UserNameMaxLengthError"),
                },
                Password: {
                    required: messageManager.setErrorMessage("#errorMessagePassword",
                        "<p id='PasswordRequiredError'>Debes insertar tu contraseña</p>",
                        "#PasswordRequiredError"),
                }
            }
        });
    },

    UpdateUserValidation: function () {
        $("#CreateUserForm").validate({
            rules: {
                FirstName: { required: true, minlength: 3, maxlength: 30 },
                LastName: { required: true, minlength: 3, maxlength: 30 },
                CellPhone: { required: true, minlength: 10, maxlength: 10 },
                Genre: { required: true },
                Email: { required: true, email: true }
            },
            messages: {
                FirstName: {
                    required: messageManager.setErrorMessage("#errorMessageFirstName",
                        "<p id='FirstNameRequiredError'>Debes insertar tu nombre</p>",
                        "#FirstNameRequiredError"),
                    minlength: messageManager.setErrorMessage("#errorMessageFirstName",
                        "<p id='FirstNameMinLengthError'>Debes insertar al menos 3 letras</p>",
                        "#FirstNameMinLengthError"),
                    maxlength: messageManager.setErrorMessage("#errorMessageFirstName",
                        "<p id='FirstNameMaxLengthError'>Debes insertar solo 30 letras</p>",
                        "#FirstNameMaxLengthError"),
                },
                LastName: {
                    required: messageManager.setErrorMessage("#errorMessageLastName",
                        "<p id='LastNameRequiredError'>Debes insertar tu apellido</p>",
                        "#LastNameRequiredError"),
                    minlength: messageManager.setErrorMessage("#errorMessageLastName",
                        "<p id='LastNameMinLengthError'>Debes insertar al menos 3 letras</p>",
                        "#LastNameMinLengthError"),
                    maxlength: messageManager.setErrorMessage("#errorMessageLastName",
                        "<p id='LastNameMaxLengthError'>Debes insertar solo 30 letras</p>",
                        "#LastNameMaxLengthError"),
                },
                CellPhone: {
                    required: messageManager.setErrorMessage("#errorMessageCellPhone",
                        "<p id='CellPhoneRequiredError'>Debes insertar tu teléfono</p>",
                        "#CellPhoneRequiredError"),
                    minlength: messageManager.setErrorMessage("#errorMessageCellPhone",
                        "<p id='CellPhoneMinLengthError'>Debes insertar solo 10 números</p>",
                        "#CellPhoneMinLengthError"),
                    maxlength: messageManager.setErrorMessage("#errorMessageCellPhone",
                        "<p id='CellPhoneMaxLengthError'>Debes insertar solo 10 números</p>",
                        "#CellPhoneMaxLengthError"),
                },
                Genre: {
                    required: messageManager.setErrorMessage("#errorMessageGenre",
                        "<p id='GenreRequiredError'>Debes escoger un género</p>",
                        "#GenreRequiredError"),
                },
                Email: {
                    required: messageManager.setErrorMessage("#errorMessageEmail",
                        "<p id='EmailRequiredError'>Debes insertar tu email</p>",
                        "#EmailRequiredError"),
                    email: messageManager.setErrorMessage("#errorMessageEmail",
                        "<p id='EmailPatternError'>Debes insertar un email válido</p>",
                        "#EmailPatternError"),
                }
            }
        });
    }
}