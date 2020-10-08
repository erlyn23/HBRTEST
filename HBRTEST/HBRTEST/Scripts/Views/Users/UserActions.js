var userOperations = {
    CreateUser: function ()
    {
        var json = {
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            CellPhone: $("#CellPhone").val(),
            Genre: $("#Genre").val(),
            Email: $("#Email").val(),
            UserName: $("#UserName").val(),
            Password: $("#Password").val()
        }

        request.post("/Users/CreateUser", json, proccessResponse.OnSuccessCreateUser);
    },
    UpdateUser: function () {
        var json = {
            UserId: $("#UserId").val(),
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            CellPhone: $("#CellPhone").val(),
            Genre: $("#Genre").val(),
            Email: $("#Email").val(),
            Password: $("#Password").val(),
            Status: $("#Status").val()
        }
        request.post("/Users/UpdateProfile", json, proccessResponse.OnSuccessUpdateUser);
    },
    SignIn: function () {
        var json = {
            UserName: $("#UserName").val(),
            Password: $("#Password").val()
        }
        request.post("/Users/Index", json, proccessResponse.OnSuccessSignIn);
    }
}

var proccessResponse = {
    OnSuccessCreateUser: function (data) {
        alert(data);
        window.location = "/Users/Index";
    },

    OnSuccessUpdateUser: function (data) {
        alert(data);
        window.location = "/Users/UpdateProfile";
    },

    OnSuccessSignIn: function (data) {
        if (data[0] === "/") {
            window.location = data;
        }
        else {
            $("#errorMessage").removeClass("d-none").text(data);

            setTimeout(function () {
                $("#errorMessage").addClass("d-none");
            }, 3000);
        }
    }
}
