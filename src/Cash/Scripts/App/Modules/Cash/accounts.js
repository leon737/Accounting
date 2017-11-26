﻿define(["application"],
    function (app) {
        return {
            run: function () {

                var viewModel = function () {
                    var self = this;

                    self.chartId = ko.observable($("#chartid").val());

                    self.accounts = {
                        dataSource: app.createStore("/api/account/" + self.chartId()),
                        parentIdExpr: "parentAccountId",
                        //hasItemsExpr: "Has_Items"
                        editing: {
                            allowAdding: true,
                            allowUpdating: true,
                            allowDeleting: true,
                            mode: 'popup'
                        },
                        filterRow: {
                            visible: true,
                            applyFilter: "auto"
                        },
                        columnAutoWidth: true,
                        columns: [
                            {
                                dataField: 'name',
                                validationRules: [
                                    { type: 'required' }
                                ]
                            },
                            {
                                dataField: 'description',
                                visible: false
                            },
                            {
                                dataField: 'code',
                                dataType: 'numeric',
                                validationRules: [
                                    { type: 'required' },
                                    { type: 'range', min: 1, max: 999 }
                                ]
                            },
                            {
                                dataField: 'type',
                                dataType: 'number',
                                lookup: {
                                    dataSource: {
                                        store: {
                                            type: 'array',
                                            data: [
                                                { id: 1, name: 'Активный' },
                                                { id: 2, name: 'Пассивный' },
                                                { id: 3, name: 'Активно/пассивный' }
                                            ],
                                            key: "id"
                                        }
                                    },
                                    valueExpr: 'id',
                                    displayExpr: 'name'
                                },
                                validationRules: [
                                    { type: 'required' }
                                ]
                            },
                            {
                                dataField: 'currencyId',
                                caption: 'Currency',
                                lookup: {
                                    dataSource: DevExpress.data.AspNet.createStore({
                                        key: "id",
                                        loadUrl: "/api/currency"
                                    }),
                                    valueExpr: "id",
                                    displayExpr: "name"
                                },
                                validationRules: [
                                    { type: 'required' }
                                ]
                            },
                            { dataField: 'balance', allowEditing: false },
                            {
                                dataField: 'locked', trueText: 'Заблокирован', falseText: 'Разблокирован'
                          
                            },
                            { dataField: 'createdOn', dataType: 'datetime', allowEditing: false },
                            { dataField: 'createdBy', allowEditing: false },
                            { dataField: 'modifiedOn', dataType: 'datetime', allowEditing: false },
                            { dataField: 'modifiedBy', allowEditing: false },
                            { dataField: 'lastUpdatedOn', dataType: 'datetime', allowEditing: false },
                            { dataField: 'lastUpdatedBy', allowEditing: false }
                        ],
                        onEditorPreparing: function (e) {
                            if (e.dataField == 'description') e.editorName = 'dxTextArea';
                            if (e.dataField == 'code') {
                                e.editorName = 'dxNumberBox';
                                e.editorOptions.min = 1;
                                e.editorOptions.max = 999;
                                e.editorOptions.format = '##0';
                            }
                            if (e.dataField == 'type' || e.dataField == 'currencyId') {
                                if (!!e.row) {
                                    if (!!e.row.data.hasTransactions || (!!e.row.balance && e.row.data.balance != 0)) {
                                        e.editorOptions.disabled = true;
                                    }
                                }
                            }
                        },
                        onToolbarPreparing: function (e) {
                            e.toolbarOptions.items.unshift(
                            {
                                location: 'before',
                                template: 'chartSelectorTemplate'
                            });
                        },
                        onInitialized: function (e) {
                            self.accountsList = e.component;
                        }
                    };

                    self.chartSelector = {
                        dataSource: DevExpress.data.AspNet.createStore({
                            key: "id",
                            loadUrl: "/api/chart"
                        }),
                        valueExpr: "id",
                        value: self.chartId,
                        displayExpr: "name",
                        onSelectionChanged: function (e) {
                            self.accountsList.option("dataSource", app.createStore("/api/account/" + self.chartId()));
                            self.accountsList.refresh();
                        }
                    };
                };

                app.bindModel(viewModel);

            }
        };
    });