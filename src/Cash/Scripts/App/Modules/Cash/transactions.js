define(["application", "stores", "urls", "logger", "moment", "lodash"],
    function (app, storeFactory, urls, log, moment, _) {
        return {
            run: function () {

                var viewModel = function () {
                    var self = this;

                    self.chartId = ko.observable($("#chartId").val());
                    self.accountId = ko.observable($("#accountid").val());


                    self.getStore = function (extra) {
                        if (!!extra)
                            return storeFactory.createApiStore(urls.transaction.extra(self.accountId()));
                        return storeFactory.createApiStore(urls.transaction.api(self.accountId()));
                    };

                    self.transactions = {
                        dataSource: self.getStore(),
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
                                dataField: 'remark',
                                caption: 'Комментарий'
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

                    self.transactionsExtra = {
                        dataSource: self.getStore('extra'),
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

                        
                        onInitialized: function (e) {
                            self.transactionsExtraList = e.component;
                        },

                        columns: [
                            {
                                dataField: 'date',
                                dataType: 'datetime',
                                caption: 'Дата операции'
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
                            },
                            {
                                dataField: 'remark',
                                caption: 'Комментарий'
                            }
                        ]
                    };

                    self.accountSelector = {
                        dataSource: storeFactory.createApiStore(urls.account.api(self.chartId())),
                        valueExpr: "id",
                        value: self.accountId,
                        displayExpr: "name",
                        onSelectionChanged: function (e) {
                            _.each([self.transactionsList, self.chart],
                                function (x) {
                                    x.option("dataSource", self.getStore());
                                });
                            self.transactionsExtraList.option("dataSource", self.getStore('extra'));
                        }
                    };

                    self.chartOptions = {
                        dataSource: self.getStore(),
                        series: {
                            type: 'line',
                            argumentField: 'date',
                            valueField: 'postBalance',
                            name: 'Баланс'
                        },
                        argumentAxis: {
                            label: {
                                customizeText: function (e) {
                                    return moment(e.value).format("DD.MM.YY HH:mm");
                                }
                            }
                        },
                        legend: {visible:false},
                        tooltip: {
                            enabled: true
                        },
                        onInitialized: function (e) {
                            self.chart = e.component;
                        }
                    };

                };

                app.bindModel(viewModel);

            }
        };
    });