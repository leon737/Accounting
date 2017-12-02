define(["application", "stores", "urls"],
    function (app, storeFactory, urls) {
        return {
            run: function() {

                var viewModel = function() {
                    var self = this;

                    self.currencies = {
                        dataSource: storeFactory.createApiStore(urls.currency.api),
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
                                dataField: 'code'
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