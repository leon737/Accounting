define("application",
    [],
    function () {

        return {
            bindModel: function(model) {
                ko.applyBindings(new model());
            }
        };
    });