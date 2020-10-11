var ProductActions = {
    filterProducts: function () {
        var json = { categoryId: $("#CategoryID").val(), productName: $("#ProductName").val() }

        request.post("/Products/FilterProducts", json, proccessResponse.OnSucccessFilterProducts);
    }
}

var proccessResponse = {
    OnSucccessFilterProducts: function (response) {
        if (response.length > 0) {
            $("#productsRow").remove();
            $("#productsContainer").append("<div class='row mt-3' id='productsRow'></div>");
            for (let i in response) {
                var productImage = "https://localhost:44375" + response[i].ProductImage;
                $("#productsRow").append("<div class='col-md-4 mt-3' id='productsCol" + response[i].ProductId + "'></div>");
                $("#productsCol" + response[i].ProductId).append("<div class='card' style='width: 18rem;' id='productCard" + response[i].ProductId + "'></div >");
                $("#productCard" + response[i].ProductId).append("<img src=" + productImage + " class='card-img-top' alt='imagen del producto' style='width: 17.9rem; height: 15rem;'>");
                $("#productCard" + response[i].ProductId).append("<div class='card-body' id='productCardBody" + response[i].ProductId + "'></div >");
                $("#productCardBody" + response[i].ProductId).append("<h5 class='card-title'>"+response[i].ProductName+"</h5>"+
                "<p class='card-text'>"+response[i].Description+"</p>"+
                "<p>Precio: <b class='text-success'>RD$ "+response[i].Price+"</b></p>"+
                "<a href='/Products/ProductsDetail?productId="+response[i].ProductId+"' class='btn btn-primary'>Ver detalles</a>");
            }
        }
    }
}