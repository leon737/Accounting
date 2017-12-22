define(["stores", "urls"], function(storeFactory, urls) {
    return { class: 
        function(model) {
            const tab = this;

            tab.title = "Транзакции по счету";
            tab.template = "transactionsGridTab";
            
            var getStore = function() {
                return storeFactory.createApiStore(urls.transaction.api(model.accountId()))
            };

            tab.reload = function() {
                if (tab.transactionsList)
                    tab.transactionsList.option("dataSource", getStore());
            };

            tab.transactions = {
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
                    tab.transactionsList = e.component;
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
                            dataSource: storeFactory.createApiStore(urls.account.api(model.chartId())),
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
        }
    };
});