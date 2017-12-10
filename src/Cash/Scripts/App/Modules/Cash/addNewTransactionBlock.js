define(["stores", "urls", "logger", "moment"], function(storeFactory, urls, log, moment) {
    return {
        model: function (chartId, parentModel) {
            var model = this;

            model.data = {
                creditAccount: ko.observable(),
                debitAccount: ko.observable(),
                amount: ko.observable(0),
                date: ko.observable(),
                remark: ko.observable("")
            };

            model.resetErrors = function() {
                model.errors.creditAccountConstraintViolation(false);
                model.errors.creditAccountTimelineViolation(false);
                model.errors.debitAccountConstraintViolation(false);
                model.errors.debitAccountTimelineViolation(false);
                model.errors.creditAndDebitAccountsAreSame(false);
            }

            model.errors = {
                creditAccountConstraintViolation: ko.observable(false),
                creditAccountTimelineViolation: ko.observable(false),
                debitAccountConstraintViolation: ko.observable(false),
                debitAccountTimelineViolation: ko.observable(false),
                creditAndDebitAccountsAreSame: ko.observable(false)
            };

            model.validationGroupOptions = {
                onInitialized: function(e) { model.validationGroup = e.component; }
            };

            model.date = {
                value: model.data.date,
                max: new Date(),
                calendarOptions: {
                    firstDayOfWeek: 1
                },
                showClearButton: true,
                type: 'datetime'
            };

            model.submitData = function () {
                if (!model.validationGroup.validate().isValid) return;
                model.sendData();
            };

            model.ok = {
                text: "Внести данные",
                type: "success",
                onClick: model.submitData
            };
            
            model.amount = {
                value: model.data.amount,
                min: 0.01,
                max: 10000000.00,
                format: '##,###,##0.00'
            };

            model.amountValidator = {
                validationRules: [
                    { type: 'required', message: 'Не заполнено поле сумма'},
                    { type: 'range', min: 0.01, message: 'Значение поля сумма вне допустимого диапазона'}
                ]
            };

            model.remark = {
                height: "70px",
                value: model.data.remark
            };

            model.remarkValidator = {
                validationRules: [
                    { type: 'required', message: 'Не заполнено поле комментария' }
                ]
            };
           

            var treeView;

            var syncTreeViewSelection = function (treeView, value) {
                if (!value) {
                    treeView.unselectAll();
                } else {
                    treeView.selectItem(value);
                }
            };

            var createAccountOptions = function(value) {
                return {
                    value: value,
                    valueExpr: 'id',
                    displayExpr: function(e) {
                        if (!!e) {
                            return `${e.name} (${e.code})`;
                        }
                        return "";
                    },
                    dataSource: storeFactory.createApiStore(urls.account.active(chartId())),
                    contentTemplate: function(e) {
                        var value = e.component.option("value"),
                            $treeView = $("<div>")
                                .dxTreeView({
                                    dataSource: e.component.option("dataSource"),
                                    dataStructure: "plain",
                                    keyExpr: "id",
                                    parentIdExpr: "parentAccountId",
                                    selectionMode: "single",
                                    height: '300px',
                                    displayExpr: "name",
                                    selectByClick: true,
                                    onContentReady: function(args) {
                                        syncTreeViewSelection(args.component, value);
                                    },
                                    selectNodesRecursive: false,
                                    onItemSelectionChanged: function(args) {
                                        var value = args.component.getSelectedNodesKeys();

                                        e.component.option("value", value);

                                        model.resetErrors();
                                        
                                    }
                                });

                        treeView = $treeView.dxTreeView("instance");

                        e.component.on("valueChanged",
                            function(args) {
                                syncTreeViewSelection(treeView, args.value);
                            });

                        return $treeView;
                    }
                };
            };

            var createAccountValidatorOptions = function (constraintViolation, timelineViolation, message) {
                return {
                    validationRules: [
                        { type: 'required', message: `${message}: не выбран счет`},
                        { type: 'custom', message: message + ": нарушение ограничения", reevaluate: true, validationCallback: function() { return !constraintViolation(); }},
                        { type: 'custom', message: message + ": нарушение последовательности транзакций", reevaluate: true, validationCallback: function () { return !timelineViolation(); }},
                        { type: 'custom', message: "Выбран один и тот же счет в качестве кредитного и дебитового счетов", reevaluate: true, validationCallback: function () { return !model.errors.creditAndDebitAccountsAreSame(); } }
                    ]
                };
            };


            model.creditAccount = createAccountOptions(model.data.creditAccount);
            model.debitAccount = createAccountOptions(model.data.debitAccount);
            model.creditAccountValidator = createAccountValidatorOptions(model.errors.creditAccountConstraintViolation, model.errors.creditAccountTimelineViolation, "Кредитный счет");
            model.debitAccountValidator = createAccountValidatorOptions(model.errors.debitAccountConstraintViolation, model.errors.debitAccountTimelineViolation, "Дебетовый счет");


            model.sendData = function () {
                var data = {
                    creditAccount: model.data.creditAccount()[0],
                    debitAccount: model.data.debitAccount()[0],
                    amount: model.data.amount(),
                    date: model.data.date() && moment(model.data.date()).valueOf(),
                    remark: model.data.remark()
                };

                log.out(model.data);
                log.out(data);


                $.post(urls.transaction.create, data)
                    .done(function(result) {
                        model.resetErrors();
                        if (result.status == 0) {
                            DevExpress.ui.notify("Транзакция успешно размещена", "success", 2000);
                            parentModel.accountsList.refresh();

                        } else {
                            DevExpress.ui.notify("Ошибка при размещении транзакции", "error", 2000);
                            switch(result.error) {
                                case 1: // credit and debit accounts are same
                                    model.errors.creditAndDebitAccountsAreSame(true);
                                    break;
                                case 2: // amount is zero
                                    break;
                                case 3: // credit account constraint violation
                                    model.errors.creditAccountConstraintViolation(true);
                                    break;
                                case 4: // debit account constraint violation
                                    model.errors.debitAccountConstraintViolation(true);
                                    break;
                                case 5: // credit account timeline violation
                                    model.errors.creditAccountTimelineViolation(true);
                                    break;
                                case 6: // debit account timeline violation
                                    model.errors.debitAccountTimelineViolation(true);
                                    break;
                            }
                            model.validationGroup.validate();
                        }
                    });
            };

        }


    }
});