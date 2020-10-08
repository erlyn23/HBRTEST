$(function () {
    $("#SignIn").click(function () {
        if ($("#LoginForm").valid()) {
            userOperations.SignIn();
        } else {
            $("#UserName-error").remove();
            $("#Password-error").remove();
            UsersFormValidations.SignInValidation();
        }
    });
});