--grant select, insert, delete, update on all tables in schema cash to cashuser;

select * from dbo."AspNetUsers";

insert into cash."Currency"
("Id", "Name", "Code", "CreatedOn", "CreatedBy")
values('93333f0a-27b6-4d41-8874-7de08fca91d3', 'Рубль', 'RUR', now(), 'cf7ca134-5274-42f7-a011-20326296b78c')

insert into cash."Account"
("Id", "ChartId", "Name",  "Code", "Type", "CurrencyId", "Balance", "Locked", "CreatedOn", "CreatedBy" )
values('51d831ba-8e88-4ada-9cca-d46333d45d29', 'c6064197-b150-4da4-8708-25ce89e0802d', 'Тестовый счет 1', 10, 1, '93333f0a-27b6-4d41-8874-7de08fca91d3', 100.0, false,
now(), 'cf7ca134-5274-42f7-a011-20326296b78c')

insert into cash."Account"
("Id", "ChartId", "Name",  "Code", "Type", "CurrencyId", "Balance", "Locked", "CreatedOn", "CreatedBy" )
values('403f9c10-a2aa-4870-be57-07f44e423af3', 'c6064197-b150-4da4-8708-25ce89e0802d', 'Тестовый счет 1', 20, 2, '93333f0a-27b6-4d41-8874-7de08fca91d3', -50.0, false,
now(), 'cf7ca134-5274-42f7-a011-20326296b78c')


insert into cash."Account"
("Id", "ParentAccountId", "ChartId", "Name",  "Code", "Type", "CurrencyId", "Balance", "Locked", "CreatedOn", "CreatedBy" )
values('f4519936-3e50-4bd8-a441-b58eb2b79c9b', '51d831ba-8e88-4ada-9cca-d46333d45d29', 'c6064197-b150-4da4-8708-25ce89e0802d', 'Тестовый счет 1', 1, 1, '93333f0a-27b6-4d41-8874-7de08fca91d3', 30.0, false,
now(), 'cf7ca134-5274-42f7-a011-20326296b78c')

insert into cash."Transaction"
("Id", "CreditAccountId", "DebitAccountId", "CreditAmount", "DebitAmount", "CreditAccountBalance", "DebitAccountBalance", "CurrencyRate", "Remark", "CreatedOn", "CreatedBy")
values ('362912c6-c7da-4d20-b744-3cd3790fe6a4', '403f9c10-a2aa-4870-be57-07f44e423af3', 'f4519936-3e50-4bd8-a441-b58eb2b79c9b', 10, 10, 100, 50, 1, 'remark', now(), 'cf7ca134-5274-42f7-a011-20326296b78c')