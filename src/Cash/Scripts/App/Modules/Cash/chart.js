define(["application"],
    function (app) {
        return {
            run: function() {

                var viewModel = function() {
                    var self = this;

                    self.chartGrid = {
                        dataSource: app.createStore("/api/chart"),
                        editing: {
                            allowAdding: true,
                            allowUpdating: true,
                            allowDeleting: true
                        },
                        filterRow: {
                            visible: true,
                            applyFilter: "auto"
                        },
                        columns: [
                            'name', 'description', 
                            { dataField: 'createdOn', dataType: 'datetime', allowEditing: false },
                            { dataField: 'createdBy', allowEditing: false},
                            { dataField: 'modifiedOn', dataType: 'datetime', allowEditing: false },
                            { dataField: 'modifiedBy', allowEditing: false }
                        ]
                    };
                };

                app.bindModel(viewModel);

            }
        };
    });