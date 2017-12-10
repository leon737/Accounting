create table cash."Chart"
(
"Id"					uuid			primary key not null,
"Name"					text			not null,
"Description"			text			null,
"CreatedOn"				timestamp		not null,
"CreatedBy"				text			not null,
"ModifiedOn"			timestamp		null,
"ModifiedBy"			text			null
);

create table cash."Currency"
(
"Id"					uuid			primary key not null,
"Name"					text			not null,
"Code"					text			not null,
"CreatedOn"				timestamp		not null,
"CreatedBy"				text			not null,
"ModifiedOn"			timestamp		null,
"ModifiedBy"			text			null
);


create table cash."Account"
(
"Id"					uuid			primary key not null,
"ChartId"				uuid			not null references cash."Chart"("Id"),
"ParentAccountId"		uuid			null references cash."Account"("Id"),
"Name"					text			not null,
"Description"			text			null,
"Code"					int				not null,
"Type"					int				not null check("Type" in (1, 2, 3)),
"CurrencyId"			uuid			not null references cash."Currency"("Id"),
"Balance"				decimal(19, 4)	not null check(("Type" = 1 and "Balance" >= 0.0) or ("Type" = 2 and "Balance" <= 0.0) or "Type" = 3),
"Locked"				bool			not null,
"CreatedOn"				timestamp		not null,
"CreatedBy"				text			not null,
"ModifiedOn"			timestamp		null,
"ModifiedBy"			text			null,
"LastUpdatedOn"			timestamp		null,
"LastUpdatedBy"			text			null,
unique("ChartId", "ParentAccountId", "Code")
);

create table cash."Transaction"
(
"Id"					uuid			primary key not null,
"CreditAccountId"		uuid			not null references cash."Account"("Id"),
"DebitAccountId"		uuid			not null references cash."Account"("Id"),
"CreditAmount"			decimal(19,4)	not null check("CreditAmount" >= 0.0),
"DebitAmount"			decimal(19,4)	not null check("DebitAmount" >= 0.0),
"PreCreditAccountBalance"	decimal(19,4)	not null,
"PreDebitAccountBalance"	decimal(19,4)	not null,
"PostCreditAccountBalance"	decimal(19,4)	not null,
"PostDebitAccountBalance"	decimal(19,4)	not null,
"Date"					timestamp		not null,
"CurrencyRate"			decimal(19,9)	not null,
"Remark"				text			not null,
"CreatedOn"				timestamp		not null,
"CreatedBy"				text			not null
);


