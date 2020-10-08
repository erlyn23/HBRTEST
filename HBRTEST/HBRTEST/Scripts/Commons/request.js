var request = {
    post: function (url, params, successMethod){
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify(params),
            contentType: "application/json; charset=UTF-8",
            dataType: "json",
            success: function (data){
                 successMethod(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.error(XMLHttpRequest, textStatus, errorThrown);
            }
        });
    }
}