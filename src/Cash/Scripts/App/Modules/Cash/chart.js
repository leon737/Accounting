define(["application", "lodash", "stores", "urls"],
    function (app, _, storeFactory, urls) {
        return {
            run: function() {

                var viewModel = function() {
                    var self = this;

                    self.charts = {
                        dataSource: storeFactory.createApiStore(urls.chart.api),
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
                                dataField: 'name',
                                cellTemplate: 'nameCellTemplate'
                            }, 

                            {
                                dataField: 'description',
                                visible: false
                            },
                            { dataField: 'createdOn', dataType: 'datetime', allowEditing: false },
                            { dataField: 'createdBy', allowEditing: false},
                            { dataField: 'modifiedOn', dataType: 'datetime', allowEditing: false },
                            { dataField: 'modifiedBy', allowEditing: false }
                        ],
                        onEditorPreparing: function(e) {
                            if (e.dataField == 'description') e.editorName = 'dxTextArea';
                        }
                    };
                };

                app.bindModel(viewModel);

            }
        };
    });