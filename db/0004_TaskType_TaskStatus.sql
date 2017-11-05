CREATE TABLE "TaskType"
(
"Id" int not null primary key,
"Name" text not null
);

INSERT INTO "TaskType"
VALUES 
(1, '������'),
(2, '�����������');

CREATE TABLE "TaskStatus"
(
"Id" int not null primary key,
"Name" text not null
);

INSERT INTO "TaskStatus"
VALUES
(1, '�����'),
(2, '� ������'),
(3, '���������'),
(4, '���������');

CREATE SEQUENCE public."TaskStatusTransition_Id_seq"
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;


CREATE TABLE "TaskStatusTransition"
(
"Id" integer not null DEFAULT nextval('"TaskStatusTransition_Id_seq"'::regclass) primary key,
"SourceStatusId" int not null,
"TargetStatusId" int not null,
"Name" text not null
);

INSERT INTO "TaskStatusTransition"
("SourceStatusId", "TargetStatusId", "Name")
VALUES 
(1, 2, '������ ������'), -- new -> in progress
(2, 1, '�������� ������'), -- in progress -> new
(2, 3, '���������'), -- in progress -> complete
(3, 2, '����������� ������'), -- complete -> in progress
(1, 4, '���������'), -- new -> rejected
(4, 1, '��������� � �������'); -- rejected -> new

ALTER TABLE "TaskStatusTransition" ADD CONSTRAINT "FK_TaskStatusTransition_TaskStatus_Source" FOREIGN KEY("SourceStatusId") REFERENCES "TaskStatus"("Id");

ALTER TABLE "TaskStatusTransition" ADD CONSTRAINT "FK_TaskStatusTransition_TaskStatus_Target" FOREIGN KEY("TargetStatusId") REFERENCES "TaskStatus"("Id");

ALTER TABLE "Task"
ADD COLUMN "TaskTypeId" int null;

ALTER TABLE "Task"
ADD COLUMN "TaskStatusId" int null;

UPDATE "Task" SET "TaskTypeId" = 1 WHERE "TaskTypeId" IS NULL;

UPDATE "Task" SET "TaskStatusId" = 1 WHERE "TaskStatusId" IS NULL;

ALTER TABLE "Task"
ALTER COLUMN "TaskTypeId" SET NOT NULL;

ALTER TABLE "Task"
ALTER COLUMN "TaskStatusId" SET NOT NULL;

ALTER TABLE "Task" ADD CONSTRAINT "FK_Task_TaskType" FOREIGN KEY("TaskTypeId") REFERENCES "TaskType"("Id");

ALTER TABLE "Task" ADD CONSTRAINT "FK_Task_TaskStatus" FOREIGN KEY("TaskStatusId") REFERENCES "TaskStatus"("Id");

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "TaskType" TO tasksuser;

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "TaskStatus" TO tasksuser;

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "TaskStatusTransition" TO tasksuser;

GRANT SELECT,USAGE ON SEQUENCE "TaskStatusTransition_Id_seq" TO tasksuser;





