function task_statuses_model(taskId, data) {
    var self = this;

    self.taskId = taskId;

    self.statuses = ko.observableArray(data);
    
    self.selectStatus = function(id)
    {
        $.post("/Tasks/SetStatus", {id: self.taskId, targetStatusId: id}).then(function(data) {
            self.statuses(data.statuses);
            $("#modified_tag").text(data.modified);
            $("#modifiedBy_tag").text(data.modifiedBy);
            $("#status_tag").text(data.status);
        });
    }
}
