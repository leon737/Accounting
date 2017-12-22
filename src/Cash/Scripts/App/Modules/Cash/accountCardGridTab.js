define(["stores", "urls"], function(storeFactory, urls) {
    return { class: 
        function(model) {
            const tab = this;

            tab.title = "Карточка счета";
            tab.template = "accountCardGridTab";
            
            var getStore = function() {
                return storeFactory.createApiStore(urls.transaction.accountCard(model.accountId()))
            };

            tab.reload = function() {
                if (tab.gridInstance)
                    tab.gridInstance.option("dataSource", getStore());
            };

            tab.grid = {
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
                    tab.gridInstance = e.component;
                },
                columns: [
                    {
                        dataField: 'date',
                        dataType: 'datetime',
                        caption: 'Дата операции'
                    },
                    {
                        dataField: 'remark',
                        caption: 'Комментарий'
                    },
                    {
                        caption: "Дебет",
                        columns: [
                            {
                                caption: "Счет",
                                dataField: 'debitAccountId',
                                lookup: {
                                    dataSource: storeFactory.createApiStore(urls.account.api(model.chartId())),
                                    valueExpr: "id",
                                    displayExpr: function (e) {
                                        return `${e.name} (${e.code})`;
                                    }
                                }
                            },
                            {
                                caption: "Сумма",
                                dataField: 'debitAmount'                               
                            }
                        ]
                    },
                    {
                        caption: "Кредит",
                        columns: [
                            {
                                caption: "Счет",
                                dataField: 'creditAccountId',
                                lookup: {
                                    dataSource: storeFactory.createApiStore(urls.account.api(model.chartId())),
                                    valueExpr: "id",
                                    displayExpr: function (e) {
                                        return `${e.name} (${e.code})`;
                                    }
                                }
                            },
                            {
                                caption: "Сумма",
                                dataField: 'creditAmount'
                            }
                        ]
                    },
                    {
                        dataField: 'postBalance',
                        caption: "Текущее сальдо"
                    }
                ],
                summary: {
                    totalItems: [
                        {
                            column: 'debitAmount',
                            summaryType: 'sum',
                            customizeText: d => `Обороты за период: ${d.value}`
                        },
                        {
                            column: 'creditAmount',
                            summaryType: 'sum',
                            customizeText: d => `Обороты за период: ${d.value}`
                        },
                        {
                            name: 'balance',
                            showInColumn: "postBalance",
                            summaryType: 'custom'   ,
                            customizeText: d => `${d.value.pre} / ${d.value.post}`                    
                        }
                    ],
                    calculateCustomSummary: function(options) {
                        if (options.name == 'balance') {
                            if (options.summaryProcess == 'start')
                                options.totalValue = {post:0};
                            else if (options.summaryProcess == 'calculate') {
                                if (options.totalValue.pre == undefined)
                                    options.totalValue.pre = options.value.preBalance;
                                options.totalValue.post = options.value.postBalance;
                            }
                        }
                        
                    }
                }
            };            
        }
    };
});