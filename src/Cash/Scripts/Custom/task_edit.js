$(function () {
        $("#new_resource")
            .autocomplete(
            {
                minLength: 2,
                source: "/" + $("#project").val() + "/Resources/FindResource",
                select: function(event, ui) {
                    window.resource_to_add(ui.item);
                }
            }
        );

    $("#parent_task_title")
        .autocomplete(
            {
                minLength: 2,
                source: "/" + $("#project").val() + "/Tasks/FindTask",
                select: function (event, ui) {
                    $("#ParentId").val(ui.item.id);
                }
            }
        );

    $("#move_to_root")
        .click(function () {
            $("#parent_task_title").val("");
            $("#ParentId").val("");
        });
});


function task_resources_model(data) {
    var self = this;

    self.resources = ko.observableArray(data);

    self.resource_to_add = ko.observable();

    window.resource_to_add = self.resource_to_add;

    self.add_quantity = ko.observable();

    self.add_resource = function() {
        self.resources.push({
            id: self.resource_to_add().id,
            name: self.resource_to_add().value,
            unit_name: self.resource_to_add().unit_name,
            quantity: self.add_quantity()
        });
        self.resource_to_add(undefined);
        self.add_quantity(undefined);
        $("#new_resource").val("");
    };

    self.remove_resource = function(resource) {
        self.resources.remove(resource);
    };

}
