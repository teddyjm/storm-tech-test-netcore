$(document).ready(function () {
    $(".updateRankButton").click(function () {

        var todoItemId = $(this).data("todo-item-id");
        var rank = $(this).data("rank");

        $.ajax({
            type: "POST",
            url: "/api/TodoItem/UpdateRank",
            contentType: "application/json",
            data: JSON.stringify({
                "TodoItemId": todoItemId,
                "Rank": rank
            }),
            success: function () {
                window.location = window.location;
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    });
});