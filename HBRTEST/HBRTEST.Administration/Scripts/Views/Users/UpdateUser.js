$(document).ready(function () {


    $("#Active").change(function () {
        if (!$(this).is(":checked")) {
            var confirmCheck = confirm("ADVERTENCIA: Si desmarcas esta opción, no podrás entrar al sistema.");
            if (confirmCheck) {
                $(this).val(false);
            }
        } else {
                $(this).val(true);
        }
    });

    $("#UpdateUser").click(function () {
        if ($("#UpdateProfileForm").valid()) {
            userOperations.UpdateUser();
        } else {
            $("#FirstName-error").remove();
            $("#LastName-error").remove();
            $("#CellPhone-error").remove();
            $("#Genre-error").remove();
            $("#Email-error").remove();
            $("#Password-error").remove();
            UsersFormValidations.UpdateUserValidation();
        }
    })
});