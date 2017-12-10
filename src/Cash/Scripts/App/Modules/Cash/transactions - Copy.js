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
                                dataType: 'datetime'
                            },
                            {
                                caption: 'Кредит',
                                columns: [
                                {
                                    dataField: 'creditAccountId',
                                    caption: 'Cчет',
                                    lookup: {
                                        dataSource: storeFactory.createApiStore(urls.account.api(self.chartId())),
                                        valueExpr: "id",
                                        displayExpr: function (e) {
                                            return `${e.name} (${e.code})`;
                                        }
                                    }
                                },
                                {
                                    dataField: 'preCreditAccountBalance',
                                    caption: "До"
                                },
                            {
                                dataField: 'postCreditAccountBalance',
                                caption: "После"
                            }
                                ]
                            },
                            {
                                caption: 'Дебет',
                                columns: [
                                    {
                                        dataField: 'debitAccountId',
                                        caption: 'Cчет',
                                        lookup: {
                                            dataSource: storeFactory.createApiStore(urls.account.api(self.chartId())),
                                            valueExpr: "id",
                                            displayExpr: function (e) {
                                                return `${e.name} (${e.code})`;
                                            }
                                        }
                                    },
                                    {
                                        dataField: 'preDebitAccountBalance',
                                        caption: "До"
                                    },
                                    {
                                        dataField: 'postDebitAccountBalance',
                                        caption: "После"
                                    }
                                ]
                            }
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