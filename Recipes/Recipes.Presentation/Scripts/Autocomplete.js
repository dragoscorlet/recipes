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
                        $("#page").val("0");
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
                $('#selectedIngredients').append('<div class="selectingIds" id="' + ui.item.value + '">' +
                    '<div class="ing">' + ui.item.label +
                    '</div><div class="removeing" onclick="RemoveSelectedIng(' + ui.item.value
                    + ')"><img src="https://cdn.iconscout.com/icon/free/png-256/delete-844-902124.png" height="20" width="20"/></div></div>');
                LoadRecipeListings();
                return false;
            }
        });
    });
});

function RemoveSelectedIng(id) {
    $('#' + id).remove();
    $('#recipes').html('');
    LoadRecipeListings();
}