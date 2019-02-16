$(document).ready(function () {
    $(function () {
        $('#search').autocomplete({
            minLength: 3,
            source: function (request, response) {
                $.ajax({
                    url: "http://localhost:53399/IngredientsSuggest/SuggestIngredients",
                    dataType: "json",
                    type: "POST",
                    data: {
                        name: request.term
                    },
                    success: function (data) {
                        response($.map(data, function (value, key) {
                            return {
                                label: value.Name,
                                value: value.Id
                            }
                        }));
                    },
                });
            },
            focus: function (event, ui) {
                $('#search').val(ui.item.Name);
                return false;
            },
            select: function (event, ui) {
                $('#selectedIngredients').append('<div id="' + ui.item.value + '">' + ui.item.label + '</div>');
                LoadRecipeListings();
                return false;
            }
        });
    });
});