define([],
    function () {

        return {
            bindModel: function(model) {
                ko.applyBindings(new model());
            },
            createStore: function(url) {
                return DevExpress.data.AspNet.createStore({
                    key: "id",
                    loadUrl: url,
                    updateUrl: url,
                    insertUrl: url,
                    deleteUrl:url
                });
            }
        };
    });