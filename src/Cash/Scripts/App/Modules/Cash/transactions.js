define(["application", "stores", "urls", "logger"],
    function (app, storeFactory, urls, log) {
        return {
            run: function () {

                var viewModel = function () {
                    var self = this;

                    self.chartId = ko.observable($("#chartId").val());
                    self.accountId = ko.observable($("#accountid").val());

                    self.transactions = {
                        dataSource: storeFactory.createApiStore(urls.transaction.api(self.accountId())),
                        editing: {
                            allowAdding: false,
                            allowUpdating: false,
                            allowDeleting: false,
                        },
                        filterRow: {
                            visible: true,
                            applyFilter: "auto"
                        },
                        columnAutoWidth: true,

                        onToolbarPreparing: function (e) {
                            e.toolbarOptions.items.unshift(
                            {
                                location: 'before',
                                template: 'accountSelectorTemplate'
                            });
                        },
                        onInitialized: function (e) {
                            self.transactionsList = e.component;
                        },

                        columns: [
                            {
                                dataField: 'date',
                                dataType: 'datetime',
                                caption: 'Дата операции'
                            },
                            {
                                dataField: 'relationFrom',
                                caption: 'Тип операции',
                                lookup: {
                                    dataSource: storeFactory.createArrayStore([
                                        { id: 0, name: 'Дебет' },
                                        { id: 1, name: 'Кредит' }
                                    ]),
                                    valueExpr: 'id',
                                    displayExpr: 'name'
                                }
                            },
                            {
                              dataField: 'amount',
                              caption: 'Сумма'
                            },
                            {
                                dataField: 'correspondingAccountId',
                                caption: 'Корреспондирующий счет',
                                lookup: {
                                    dataSource: storeFactory.createApiStore(urls.account.api(self.chartId())),
                                    valueExpr: "id",
                                    displayExpr: function (e) {
                                        return `${e.name} (${e.code})`;
                                    }
                                }
                            },
                            {
                                dataField: 'postBalance',
                                caption: "Баланс счета"
                            },
                             { dataField: 'createdOn', dataType: 'datetime', allowEditing: false },
                            { dataField: 'createdBy', allowEditing: false }
                        ]
                    };

                    self.accountSelector = {
                        dataSource: storeFactory.createApiStore(urls.account.api(self.chartId())),
                        valueExpr: "id",
                        value: self.accountId,
                        displayExpr: "name",
                        onSelectionChanged: function (e) {
                            self.transactionsList.option("dataSource", storeFactory.createApiStore(urls.transaction.api(self.accountId())));
                            self.transactionsList.refresh();
                        }
                    };
                };

                app.bindModel(viewModel);

            }
        };
    });