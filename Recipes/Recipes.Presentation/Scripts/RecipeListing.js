function LoadRecipeListings () {
    var recipeIds = $.map($("#selectedIngredients").children(), function (n, i) {
        return n.id;
    });

    $.ajax({
        url: "http://localhost:53399/Recipes/GetRecipes",
        type: "POST",
        data: {
            ingredients: recipeIds.join(", "),
            pageNumber: $("#page").val(),
            includeExtra: true
        },
        success: function (data) {

            if (parseInt($("#page").val()) === 0) {
                $('#recipes').html('');
                $('#recipes').append(data);
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