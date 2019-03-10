$(document).ready(function () {
    $(function () {
        $('#search').autocomplete({
            minLength: 3,
            source: function (request, response) {
                $.ajax({
                    url: "http://192.168.1.7:81/IngredientsSuggest/SuggestIngredients",
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
                $('#selectedIngredients').append('<li class="list-group-item d-flex justify-content-between align-items-center py-1 selectingIds" id="' + ui.item.value + '">' +
                    '<div class="ing">' + ui.item.label +
                    '</div><div class="removeing" onclick="RemoveSelectedIng(' + ui.item.value
                    + ')"><img src="https://cdn.iconscout.com/icon/free/png-256/delete-844-902124.png" height="20" width="20"/></div></li>');
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