$("#createItemModalSaveButton").click(
    function () {
        $.ajax({
            type: "PUT",
            url: "/api/TodoItem/Create",
            contentType: "application/json",
            data: JSON.stringify({
                "TodoListId": $("#TodoListId").val(),
                "Importance": $("#Importance").val(),
                "ResponsiblePartyId": $("#ResponsiblePartyId").val(),
                "Title": $("#Title").val()
            }),
            success: function () {
                $('#createItemModalSaveButton').modal('hide');
                window.location = window.location;
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
);