$(function() {
    $(".set-importance-down")
        .click(function () {
            var element = $(this);
            taskSetImportance({ id: element.data("task-id"), action: "down" }).then(function (data) {
                element.parent().find(".importance").text(data);
            });
        });
    $(".set-importance-up")
        .click(function () {
            var element = $(this);
            taskSetImportance({ id: element.data("task-id"), action: "up" }).then(function (data) {
                element.parent().find(".importance").text(data);
            });
    });
});

function taskSetImportance(data) {
    return $.post("/Tasks/SetImportance", data);
}

