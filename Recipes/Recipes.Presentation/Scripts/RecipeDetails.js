function LoadRecipe(id) {
    $.ajax({
        url: "http://192.168.1.7:81/Recipes/GetRecipe",
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
            url: "http://192.168.1.7:81/VendorProducts/GetVendorProductsExactMatch",
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