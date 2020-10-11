var request = {
    post: function (url, params, method) {
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(params),
            success: function (response) {
                method(response)
            },
            error: function (XMLHttpRequest) {
                if (XMLHttpRequest.status != 200) {
                    alert("Ha ocurrido un error intentando acceder al servidor, inténtelo más tarde");
                }
            }
        })
    }
}