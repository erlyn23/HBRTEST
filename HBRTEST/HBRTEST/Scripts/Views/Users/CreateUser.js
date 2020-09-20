$(document).ready(function () {
    AddEventClickCreateUser();
});

AddEventClickCreateUser = function () {
    $("#CreateUser").on("click", function () {
        var json = {
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            CellPhone: $("#CellPhone").val(),
            Genre: $("#Genre").val(),
            Email: $("#Email").val(),
            UserName: $("#UserName").val(),
            Password: $("#Password").val()
        }

        if (ValidateEmptyOrNullFields(json.FirstName, json.LastName, json.CellPhone, json.Genre, json.Email, json.UserName, json.Password)) {
            $("#errorMessage").removeClass("d-none").text("No puedes dejar campos vacíos");

            setTimeout(function () {
                $("#errorMessage").addClass("d-none");
            }, 3000);
        } else {
            request.post("/Users/CreateUser", json, OnSuccessCreateUser, OnErrorCreateUser);
        }
    });
}

ValidateEmptyOrNullFields = function (FirstName, LastName, CellPhone, Genre, Email, UserName, Password) {
    if (FirstName.length <= 0 || LastName.length <= 0 || CellPhone.length <= 0 || Genre.length <= 0 || Email.length <= 0
        || UserName.length <= 0 || Password.length <= 0) {
        return true;
    }
    return false;
}

OnSuccessCreateUser = function (data) {
    alert(data);
    window.location = "/Users/Index";
}

OnErrorCreateUser = function (XMLHttpRequest, textStatus, errorMessage) {
    alert("Error: " + textStatus + ". " + errorMessage);
}