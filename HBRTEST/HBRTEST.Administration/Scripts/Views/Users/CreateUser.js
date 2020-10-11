$(document).ready(function () {
    $("#CreateUser").click(function () {
        if ($("#CreateUserForm").valid()) {
            userOperations.CreateUser();
        }
        else {
            $("#FirstName-error").remove();
            $("#LastName-error").remove();
            $("#CellPhone-error").remove();
            $("#Genre-error").remove();
            $("#Email-error").remove();
            $("#UserName-error").remove();
            $("#Password-error").remove();
            UsersFormValidations.CreateUserValidation();
        }
    });
});