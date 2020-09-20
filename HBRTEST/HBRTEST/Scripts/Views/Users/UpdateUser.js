$(document).ready(function () {
    AddEventClickUpdateUser();
});

AddEventClickUpdateUser = function () {
    $("#UpdateUser").on("click", function () {
        var json = {
            UserId: $("#UserId").val(),
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            CellPhone: $("#CellPhone").val(),
            Genre: $("#Genre").val(),
            Email: $("#Email").val(),
            Password: $("#Password").val()
        }

        if (ValidateEmptyOrNullFields(json.FirstName, json.LastName, json.CellPhone, json.Genre, json.Email, json.Password)) {
            $("#errorMessage").removeClass("d-none").text("No puedes dejar campos vacíos");

            setTimeout(function () {
                $("#errorMessage").addClass("d-none");
            }, 3000);
        } else {
            request.post("/Users/UpdateProfile", json, OnSuccessUpdateUser, OnErrorUpdateUser);
        }
    });
}

ValidateEmptyOrNullFields = function (FirstName, LastName, CellPhone, Genre, Email, Password) {
    if (FirstName.length <= 0 || LastName.length <= 0 || CellPhone.length <= 0 || Genre.length <= 0
        || Email.length <= 0 || Password.length <= 0) {
        return true;
    }
    return false;
}

OnSuccessUpdateUser = function (data) {
    alert(data);
    window.location = "/Users/UpdateProfile";
}

OnErrorUpdateUser = function (XMLHttpRequest, textStatus, errorMessage) {
    alert("Error: " + textStatus + ". " + errorMessage);
}