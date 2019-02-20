function LoadRecipe(id) {
    $.ajax({
        url: "http://localhost:53399/Recipes/GetRecipe",
        type: "POST",
        data: {
            idRecipe: id
        },
        success: function (data) {
            $('#recipes').hide();
            $('#recipe').append(data);
        }
    });
}


function LoadVendorProducts(data) {
    $(document).ready(function () {
        $.ajax({
            url: "http://localhost:53399/VendorProducts/GetVendorProductsExactMatch",
            type: "POST",
            data: {
                ingredients: data
            },
            success: function (data) {
                $('#vendorProductsDrop').append(data)
            }
        });
    });

    return true;
}