define(["application"],
    function (app) {
        return {
            run: function() {

                var viewModel = function() {
                    var self = this;

                    self.currencies = {
                        dataSource: app.createStore("/api/currency"),
                        editing: {
                            allowAdding: true,
                            allowUpdating: true,
                            allowDeleting: true,
                            mode: 'form'
                        },
                        filterRow: {
                            visible: true,
                            applyFilter: "auto"
                        },
                        columns: [
                            {
                                dataField: 'name'
                            }, 
                            {
                                dataField: 'code',
                            },
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