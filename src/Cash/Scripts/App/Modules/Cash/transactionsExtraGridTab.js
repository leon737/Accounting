define(["stores", "urls"], function(storeFactory, urls) {
    return { class: 
        function(model) {
            const tab = this;

            tab.title = "Транзакции по счету (доп)";
            tab.template = "transactionsExtraGridTab";
            
            var getStore = function() {
                return storeFactory.createApiStore(urls.transaction.extra(model.accountId()))
            };

            tab.reload = function() {
                if (tab.transactionsExtraList)
                    tab.transactionsExtraList.option("dataSource", getStore());
            };

            tab.transactionsExtra = {
                dataSource: getStore(),
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
                    tab.transactionsExtraList = e.component;
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
                                dataSource: storeFactory.createApiStore(urls.account.api(model.chartId())),
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
                                    dataSource: storeFactory.createApiStore(urls.account.api(model.chartId())),
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
        }
    };
});