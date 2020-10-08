$(document).ready(function () {


    $("#Status").change(function () {
        var confirmCheck = confirm("ADVERTENCIA: Si desmarcas esta opción, no podrás entrar al sistema.");
        if (confirmCheck) {
            if (!$(this).is(":checked")) {
                $(this).attr("checked", false);
                $(this).val("Inactivo");
            } else {
                $(this).attr("checked", true);
                $(this).val("Activo");
            }
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