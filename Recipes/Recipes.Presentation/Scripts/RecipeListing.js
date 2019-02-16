function LoadRecipeListings () {
    var recipeIds = $.map($("#selectedIngredients").children(), function (n, i) {
        return n.id;
    });

    $.ajax({
        url: "http://localhost:53399/Recipes/GetRecipes",
        type: "POST",
        data: {
            ingredients: recipeIds.join(", "),
            includeExtra: true
        },
        success: function (data) {

            $('#recipes').html('');
            $('#recipes').append(data);
        }
    });
}