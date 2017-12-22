define(["stores", "urls", "moment"], function(storeFactory, urls, moment) {
    return { class: 
        function(model) {
            const tab = this;

            tab.title = "Транзакции по счету";
            tab.template = "transactionsChartTab";

            var getStore = function() {
                return storeFactory.createApiStore(urls.transaction.api(model.accountId()));
            };

            tab.reload = function() {
                if (tab.chart)
                    tab.chart.option("dataSource", getStore());
            };

            tab.chartOptions = {
                dataSource: getStore(),
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
                    tab.chart = e.component;
                }
            };
        }
    };
});