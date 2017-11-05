-- Sequence: public."Project_Id_seq"

-- DROP SEQUENCE public."Project_Id_seq";

CREATE SEQUENCE public."Project_Id_seq"
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
ALTER TABLE public."Project_Id_seq"
  OWNER TO leon;



-- Table: public."Project"

-- DROP TABLE public."Project";

CREATE TABLE public."Project"
(
  "Id" integer NOT NULL DEFAULT nextval('"Project_Id_seq"'::regclass),
  "Name" text NOT NULL,
  "Code" text NOT NULL,
  "Description" text,
  "Created" timestamp without time zone NOT NULL,
  "Modified" timestamp without time zone,
  "CreatedBy" text NOT NULL,
  "ModifiedBy" text,
  CONSTRAINT project_pkey PRIMARY KEY ("Id"),
  CONSTRAINT project_code_unique UNIQUE ("Code")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."Project"
  OWNER TO leon;
  
INSERT INTO "Project"
("Id", "Name", "Code", "Created", "CreatedBy")
SELECT 1, 'Sample Project', 'sample_project', current_timestamp, "Id"
FROM "dbo"."AspNetUsers" limit 1;

ALTER TABLE "Task"
ADD COLUMN "ProjectId" int null;

UPDATE "Task" SET "ProjectId" = 1;

ALTER TABLE "Task"
ALTER COLUMN "ProjectId" SET not null;

ALTER TABLE "Resource"
ADD COLUMN "ProjectId" int null;

UPDATE "Resource" SET "ProjectId" = 1;

ALTER TABLE "Resource"
ALTER COLUMN "ProjectId" SET not null;

ALTER TABLE "Task" ADD CONSTRAINT "FK_Task_Project" FOREIGN KEY ("ProjectId") REFERENCES "Project"("Id");

ALTER TABLE "Resource" ADD CONSTRAINT "FK_Resource_Project" FOREIGN KEY ("ProjectId") REFERENCES "Project"("Id");

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "Project" TO tasksuser;

GRANT SELECT,USAGE ON SEQUENCE "Project_Id_seq" TO tasksuser;





