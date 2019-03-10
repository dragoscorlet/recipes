function LoadRecipeListings () {
    var recipeIds = $.map($("#selectedIngredients").children(), function (n, i) {
        return n.id;
    });

    $.ajax({
        url: "http://192.168.1.7:81/Recipes/GetRecipes",
        type: "POST",
        data: {
            ingredients: recipeIds.join(", "),
            pageNumber: $("#page").val(),
            includeExtra: true
        },
        success: function (data) {

            if (parseInt($("#page").val()) === 0) {
                $('#recipes').html('');
                $('#recipe').html('');
                $('#recipes').append(data);
                $('#recipes').show();
            }
            else {
                $('#recipes').append(data);
            }
        }
    });
}

$(window).scroll(function () {
    if ($(window).scrollTop() == $(document).height() - $(window).height() && $("#recipes").is(":visible")) {
        LoadRecipeListings();
        $("#page").val(parseInt($("#page").val()) + 1)
    }
});