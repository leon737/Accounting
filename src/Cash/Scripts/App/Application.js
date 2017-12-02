define([],
    function () {

        return {
            bindModel: function(model) {
                ko.applyBindings(new model());
            },

            getParentId: function () {
                return $("#parentid").val();
            }
        };
    });


//var instance = new appModel();
//letBinding.init();
//var ctx = new ko.bindingContext(instance);
//ctx["$model"] = instance.pageModel;
//ko.applyBindings(ctx, document.getElementById("scope"))