define(["application"],
    function (app) {
        return {
            run: function() {

                var viewModel = function() {
                    var self = this;

                    self.dataGridOptions = {
                        dataSource: DevExpress.data.AspNet.createStore({
                            key: "id",
                            loadUrl: "/api/chart",
                            updateUrl: "/api/chart",
                            insertUrl: "/api/chart",
                            deleteUrl: "/api/chart"
                        }),
                        editing: {
                            allowAdding: true,
                            allowUpdating: true,
                            allowDeleting: true
                        },
                        columns: [
                            'name', 'description'
                        ]
                    };
                };

                app.bindModel(viewModel);

            }
        };
    });