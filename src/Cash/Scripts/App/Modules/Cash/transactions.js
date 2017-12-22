define(["application", "stores", "urls", "logger", "lodash",
    "Modules/Cash/transactionsChartTab", "Modules/Cash/transactionsGridTab", "Modules/Cash/transactionsExtraGridTab"],
    function (app, storeFactory, urls, log, lodash, transactionsChartTab, transactionsGridTab, transactionsExtraGridTab) {
        return {
            run: function () {

                var viewModel = function () {
                    const model = this;

                    model.chartId = ko.observable($("#chartId").val());
                    model.accountId = ko.observable($("#accountid").val());

                    model.chartsPanel = {
                        items: [
                            new transactionsChartTab.class(model)
                        ]
                    };

                    model.gridsPanel = {
                        items: [
                            new transactionsGridTab.class(model),
                            new transactionsExtraGridTab.class(model)
                        ]
                    };

                    var tabs = _.concat(model.chartsPanel.items, model.gridsPanel.items);

                    model.accountSelector = {
                        dataSource: storeFactory.createApiStore(urls.account.api(model.chartId())),
                        valueExpr: "id",
                        value: model.accountId,
                        displayExpr: "name",
                        onSelectionChanged: function (e) {                            
                            _.each(tabs, function(tab){ 
                                tab.reload();
                            });
                        }
                    };                   

                };

                app.bindModel(viewModel);

            }
        };
    });