--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.1
-- Dumped by pg_dump version 9.6.1

-- Started on 2017-02-26 20:27:03 MSK

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 16497)
-- Name: dbo; Type: SCHEMA; Schema: -; Owner: leon
--

CREATE SCHEMA dbo;


ALTER SCHEMA dbo OWNER TO leon;

--
-- TOC entry 1 (class 3079 OID 12393)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2233 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = dbo, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 195 (class 1259 OID 16522)
-- Name: AspNetRoles; Type: TABLE; Schema: dbo; Owner: leon
--

CREATE TABLE "AspNetRoles" (
    "Id" character varying NOT NULL,
    "Name" character varying NOT NULL
);


ALTER TABLE "AspNetRoles" OWNER TO leon;

--
-- TOC entry 197 (class 1259 OID 16534)
-- Name: AspNetUserClaims; Type: TABLE; Schema: dbo; Owner: leon
--

CREATE TABLE "AspNetUserClaims" (
    "Id" integer NOT NULL,
    "UserId" character varying NOT NULL,
    "ClaimType" character varying,
    "ClaimValue" character varying
);


ALTER TABLE "AspNetUserClaims" OWNER TO leon;

--
-- TOC entry 196 (class 1259 OID 16532)
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: leon
--

CREATE SEQUENCE "AspNetUserClaims_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "AspNetUserClaims_Id_seq" OWNER TO leon;

--
-- TOC entry 2236 (class 0 OID 0)
-- Dependencies: 196
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: leon
--

ALTER SEQUENCE "AspNetUserClaims_Id_seq" OWNED BY "AspNetUserClaims"."Id";


--
-- TOC entry 194 (class 1259 OID 16508)
-- Name: AspNetUserLogins; Type: TABLE; Schema: dbo; Owner: leon
--

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" character varying NOT NULL,
    "ProviderKey" character varying NOT NULL,
    "UserId" character varying NOT NULL
);


ALTER TABLE "AspNetUserLogins" OWNER TO leon;

--
-- TOC entry 198 (class 1259 OID 16549)
-- Name: AspNetUserRoles; Type: TABLE; Schema: dbo; Owner: leon
--

CREATE TABLE "AspNetUserRoles" (
    "UserId" character varying NOT NULL,
    "RoleId" character varying NOT NULL
);


ALTER TABLE "AspNetUserRoles" OWNER TO leon;

--
-- TOC entry 193 (class 1259 OID 16498)
-- Name: AspNetUsers; Type: TABLE; Schema: dbo; Owner: leon
--

CREATE TABLE "AspNetUsers" (
    "Id" character varying NOT NULL,
    "Email" character varying,
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" character varying,
    "SecurityStamp" character varying,
    "PhoneNumber" character varying,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEndDateUtc" timestamp without time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    "UserName" character varying NOT NULL
);


ALTER TABLE "AspNetUsers" OWNER TO leon;

SET search_path = public, pg_catalog;

--
-- TOC entry 189 (class 1259 OID 16447)
-- Name: Resource; Type: TABLE; Schema: public; Owner: leon
--

CREATE TABLE "Resource" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    "MeasureUnit" integer NOT NULL,
    "Created" timestamp without time zone NOT NULL,
    "Modified" timestamp without time zone,
    "AdditionalInfo" text,
    "CreatedBy" character varying NOT NULL,
    "ModifiedBy" character varying
);


ALTER TABLE "Resource" OWNER TO leon;

--
-- TOC entry 190 (class 1259 OID 16456)
-- Name: ResourceMeasureUnit; Type: TABLE; Schema: public; Owner: leon
--

CREATE TABLE "ResourceMeasureUnit" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL
);


ALTER TABLE "ResourceMeasureUnit" OWNER TO leon;

--
-- TOC entry 188 (class 1259 OID 16445)
-- Name: Resource_Id_seq; Type: SEQUENCE; Schema: public; Owner: leon
--

CREATE SEQUENCE "Resource_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Resource_Id_seq" OWNER TO leon;

--
-- TOC entry 2242 (class 0 OID 0)
-- Dependencies: 188
-- Name: Resource_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: leon
--

ALTER SEQUENCE "Resource_Id_seq" OWNED BY "Resource"."Id";


--
-- TOC entry 187 (class 1259 OID 16436)
-- Name: Task; Type: TABLE; Schema: public; Owner: leon
--

CREATE TABLE "Task" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    "Created" timestamp without time zone NOT NULL,
    "Modified" timestamp without time zone,
    "Workload" numeric(4,1),
    "StartDate" date,
    "AdditionalInfo" text,
    "ParentId" integer,
    "Importance" integer NOT NULL,
    "CreatedBy" character varying NOT NULL,
    "ModifiedBy" character varying,
    "Active" boolean DEFAULT false NOT NULL,
    "WorkloadAutoCalc" boolean DEFAULT false NOT NULL
);


ALTER TABLE "Task" OWNER TO leon;

--
-- TOC entry 192 (class 1259 OID 16466)
-- Name: TaskResource; Type: TABLE; Schema: public; Owner: leon
--

CREATE TABLE "TaskResource" (
    "Id" integer NOT NULL,
    "TaskId" integer NOT NULL,
    "ResourceId" integer NOT NULL,
    "Quantity" numeric(6,2) NOT NULL,
    "Created" timestamp without time zone NOT NULL,
    "Modified" timestamp without time zone,
    "CreatedBy" character varying NOT NULL,
    "ModifiedBy" character varying
);


ALTER TABLE "TaskResource" OWNER TO leon;

--
-- TOC entry 191 (class 1259 OID 16464)
-- Name: TaskResource_Id_seq; Type: SEQUENCE; Schema: public; Owner: leon
--

CREATE SEQUENCE "TaskResource_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "TaskResource_Id_seq" OWNER TO leon;

--
-- TOC entry 2246 (class 0 OID 0)
-- Dependencies: 191
-- Name: TaskResource_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: leon
--

ALTER SEQUENCE "TaskResource_Id_seq" OWNED BY "TaskResource"."Id";


--
-- TOC entry 186 (class 1259 OID 16434)
-- Name: Task_Id_seq; Type: SEQUENCE; Schema: public; Owner: leon
--

CREATE SEQUENCE "Task_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Task_Id_seq" OWNER TO leon;

--
-- TOC entry 2248 (class 0 OID 0)
-- Dependencies: 186
-- Name: Task_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: leon
--

ALTER SEQUENCE "Task_Id_seq" OWNED BY "Task"."Id";


SET search_path = dbo, pg_catalog;

--
-- TOC entry 2058 (class 2604 OID 16537)
-- Name: AspNetUserClaims Id; Type: DEFAULT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUserClaims" ALTER COLUMN "Id" SET DEFAULT nextval('"AspNetUserClaims_Id_seq"'::regclass);


SET search_path = public, pg_catalog;

--
-- TOC entry 2056 (class 2604 OID 16450)
-- Name: Resource Id; Type: DEFAULT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Resource" ALTER COLUMN "Id" SET DEFAULT nextval('"Resource_Id_seq"'::regclass);


--
-- TOC entry 2053 (class 2604 OID 16439)
-- Name: Task Id; Type: DEFAULT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Task" ALTER COLUMN "Id" SET DEFAULT nextval('"Task_Id_seq"'::regclass);


--
-- TOC entry 2057 (class 2604 OID 16469)
-- Name: TaskResource Id; Type: DEFAULT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "TaskResource" ALTER COLUMN "Id" SET DEFAULT nextval('"TaskResource_Id_seq"'::regclass);


SET search_path = dbo, pg_catalog;

--
-- TOC entry 2085 (class 2606 OID 16529)
-- Name: AspNetRoles pk_aspnetroles; Type: CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetRoles"
    ADD CONSTRAINT pk_aspnetroles PRIMARY KEY ("Id");


--
-- TOC entry 2090 (class 2606 OID 16542)
-- Name: AspNetUserClaims pk_aspnetuserclaims; Type: CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUserClaims"
    ADD CONSTRAINT pk_aspnetuserclaims PRIMARY KEY ("Id");


--
-- TOC entry 2083 (class 2606 OID 16515)
-- Name: AspNetUserLogins pk_aspnetuserlogins; Type: CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUserLogins"
    ADD CONSTRAINT pk_aspnetuserlogins PRIMARY KEY ("LoginProvider", "ProviderKey", "UserId");


--
-- TOC entry 2094 (class 2606 OID 16556)
-- Name: AspNetUserRoles pk_aspnetuserroles; Type: CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT pk_aspnetuserroles PRIMARY KEY ("UserId", "RoleId");


--
-- TOC entry 2078 (class 2606 OID 16505)
-- Name: AspNetUsers pk_aspnetusers; Type: CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUsers"
    ADD CONSTRAINT pk_aspnetusers PRIMARY KEY ("Id");


--
-- TOC entry 2080 (class 2606 OID 16507)
-- Name: AspNetUsers uq_aspnetusers; Type: CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUsers"
    ADD CONSTRAINT uq_aspnetusers UNIQUE ("UserName");


--
-- TOC entry 2087 (class 2606 OID 16531)
-- Name: AspNetRoles uq_rolename; Type: CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetRoles"
    ADD CONSTRAINT uq_rolename UNIQUE ("Name");


SET search_path = public, pg_catalog;

--
-- TOC entry 2070 (class 2606 OID 16463)
-- Name: ResourceMeasureUnit ResourceMeasureUnit_pkey; Type: CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "ResourceMeasureUnit"
    ADD CONSTRAINT "ResourceMeasureUnit_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2065 (class 2606 OID 16455)
-- Name: Resource Resource_pkey; Type: CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Resource"
    ADD CONSTRAINT "Resource_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2072 (class 2606 OID 16471)
-- Name: TaskResource TaskResource_pkey; Type: CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "TaskResource"
    ADD CONSTRAINT "TaskResource_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2060 (class 2606 OID 16444)
-- Name: Task Task_pkey; Type: CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Task"
    ADD CONSTRAINT "Task_pkey" PRIMARY KEY ("Id");


SET search_path = dbo, pg_catalog;

--
-- TOC entry 2088 (class 1259 OID 16548)
-- Name: ix_aspnetuserclaims_userid; Type: INDEX; Schema: dbo; Owner: leon
--

CREATE INDEX ix_aspnetuserclaims_userid ON "AspNetUserClaims" USING btree ("UserId");


--
-- TOC entry 2081 (class 1259 OID 16521)
-- Name: ix_aspnetuserlogins_userid; Type: INDEX; Schema: dbo; Owner: leon
--

CREATE INDEX ix_aspnetuserlogins_userid ON "AspNetUserLogins" USING btree ("UserId");


--
-- TOC entry 2091 (class 1259 OID 16568)
-- Name: ix_aspnetuserroles_roleid; Type: INDEX; Schema: dbo; Owner: leon
--

CREATE INDEX ix_aspnetuserroles_roleid ON "AspNetUserRoles" USING btree ("RoleId");


--
-- TOC entry 2092 (class 1259 OID 16567)
-- Name: ix_aspnetuserroles_userid; Type: INDEX; Schema: dbo; Owner: leon
--

CREATE INDEX ix_aspnetuserroles_userid ON "AspNetUserRoles" USING btree ("UserId");


SET search_path = public, pg_catalog;

--
-- TOC entry 2066 (class 1259 OID 16586)
-- Name: fki_FK_Resource_CreatedBy_User; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_Resource_CreatedBy_User" ON "Resource" USING btree ("CreatedBy");


--
-- TOC entry 2067 (class 1259 OID 16592)
-- Name: fki_FK_Resource_ModifiedBy_User; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_Resource_ModifiedBy_User" ON "Resource" USING btree ("ModifiedBy");


--
-- TOC entry 2068 (class 1259 OID 16489)
-- Name: fki_FK_Resource_ResourceMeasureUnit; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_Resource_ResourceMeasureUnit" ON "Resource" USING btree ("MeasureUnit");


--
-- TOC entry 2073 (class 1259 OID 16601)
-- Name: fki_FK_TaskResource_CreatedBy_User; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_TaskResource_CreatedBy_User" ON "TaskResource" USING btree ("CreatedBy");


--
-- TOC entry 2074 (class 1259 OID 16608)
-- Name: fki_FK_TaskResource_ModifiedBy_User; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_TaskResource_ModifiedBy_User" ON "TaskResource" USING btree ("ModifiedBy");


--
-- TOC entry 2075 (class 1259 OID 16483)
-- Name: fki_FK_TaskResource_Resource; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_TaskResource_Resource" ON "TaskResource" USING btree ("ResourceId");


--
-- TOC entry 2076 (class 1259 OID 16477)
-- Name: fki_FK_TaskResource_Task; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_TaskResource_Task" ON "TaskResource" USING btree ("TaskId");


--
-- TOC entry 2061 (class 1259 OID 16574)
-- Name: fki_FK_Task_CreateBy_User; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_Task_CreateBy_User" ON "Task" USING btree ("CreatedBy");


--
-- TOC entry 2062 (class 1259 OID 16580)
-- Name: fki_FK_Task_ModifiedBy_User; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_Task_ModifiedBy_User" ON "Task" USING btree ("ModifiedBy");


--
-- TOC entry 2063 (class 1259 OID 16495)
-- Name: fki_FK_Task_ParentTask; Type: INDEX; Schema: public; Owner: leon
--

CREATE INDEX "fki_FK_Task_ParentTask" ON "Task" USING btree ("ParentId");


SET search_path = dbo, pg_catalog;

--
-- TOC entry 2106 (class 2606 OID 16543)
-- Name: AspNetUserClaims AspNetUserClaims_UserId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUserClaims"
    ADD CONSTRAINT "AspNetUserClaims_UserId_fkey" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 2105 (class 2606 OID 16516)
-- Name: AspNetUserLogins AspNetUserLogins_UserId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUserLogins"
    ADD CONSTRAINT "AspNetUserLogins_UserId_fkey" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 2107 (class 2606 OID 16557)
-- Name: AspNetUserRoles AspNetUserRoles_RoleId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT "AspNetUserRoles_RoleId_fkey" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 2108 (class 2606 OID 16562)
-- Name: AspNetUserRoles AspNetUserRoles_UserId_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: leon
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT "AspNetUserRoles_UserId_fkey" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


SET search_path = public, pg_catalog;

--
-- TOC entry 2099 (class 2606 OID 16581)
-- Name: Resource FK_Resource_CreatedBy_User; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Resource"
    ADD CONSTRAINT "FK_Resource_CreatedBy_User" FOREIGN KEY ("CreatedBy") REFERENCES dbo."AspNetUsers"("Id");


--
-- TOC entry 2100 (class 2606 OID 16587)
-- Name: Resource FK_Resource_ModifiedBy_User; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Resource"
    ADD CONSTRAINT "FK_Resource_ModifiedBy_User" FOREIGN KEY ("ModifiedBy") REFERENCES dbo."AspNetUsers"("Id");


--
-- TOC entry 2098 (class 2606 OID 16484)
-- Name: Resource FK_Resource_ResourceMeasureUnit; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Resource"
    ADD CONSTRAINT "FK_Resource_ResourceMeasureUnit" FOREIGN KEY ("MeasureUnit") REFERENCES "ResourceMeasureUnit"("Id");


--
-- TOC entry 2103 (class 2606 OID 16596)
-- Name: TaskResource FK_TaskResource_CreatedBy_User; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "TaskResource"
    ADD CONSTRAINT "FK_TaskResource_CreatedBy_User" FOREIGN KEY ("CreatedBy") REFERENCES dbo."AspNetUsers"("Id");


--
-- TOC entry 2104 (class 2606 OID 16603)
-- Name: TaskResource FK_TaskResource_ModifiedBy_User; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "TaskResource"
    ADD CONSTRAINT "FK_TaskResource_ModifiedBy_User" FOREIGN KEY ("ModifiedBy") REFERENCES dbo."AspNetUsers"("Id");


--
-- TOC entry 2102 (class 2606 OID 16478)
-- Name: TaskResource FK_TaskResource_Resource; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "TaskResource"
    ADD CONSTRAINT "FK_TaskResource_Resource" FOREIGN KEY ("ResourceId") REFERENCES "Resource"("Id");


--
-- TOC entry 2101 (class 2606 OID 16472)
-- Name: TaskResource FK_TaskResource_Task; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "TaskResource"
    ADD CONSTRAINT "FK_TaskResource_Task" FOREIGN KEY ("TaskId") REFERENCES "Task"("Id");


--
-- TOC entry 2096 (class 2606 OID 16569)
-- Name: Task FK_Task_CreateBy_User; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Task"
    ADD CONSTRAINT "FK_Task_CreateBy_User" FOREIGN KEY ("CreatedBy") REFERENCES dbo."AspNetUsers"("Id");


--
-- TOC entry 2097 (class 2606 OID 16575)
-- Name: Task FK_Task_ModifiedBy_User; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Task"
    ADD CONSTRAINT "FK_Task_ModifiedBy_User" FOREIGN KEY ("ModifiedBy") REFERENCES dbo."AspNetUsers"("Id");


--
-- TOC entry 2095 (class 2606 OID 16490)
-- Name: Task FK_Task_ParentTask; Type: FK CONSTRAINT; Schema: public; Owner: leon
--

ALTER TABLE ONLY "Task"
    ADD CONSTRAINT "FK_Task_ParentTask" FOREIGN KEY ("ParentId") REFERENCES "Task"("Id");


--
-- TOC entry 2231 (class 0 OID 0)
-- Dependencies: 4
-- Name: dbo; Type: ACL; Schema: -; Owner: leon
--

GRANT USAGE ON SCHEMA dbo TO tasksuser;


SET search_path = dbo, pg_catalog;

--
-- TOC entry 2234 (class 0 OID 0)
-- Dependencies: 195
-- Name: AspNetRoles; Type: ACL; Schema: dbo; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "AspNetRoles" TO tasksuser;


--
-- TOC entry 2235 (class 0 OID 0)
-- Dependencies: 197
-- Name: AspNetUserClaims; Type: ACL; Schema: dbo; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "AspNetUserClaims" TO tasksuser;


--
-- TOC entry 2237 (class 0 OID 0)
-- Dependencies: 194
-- Name: AspNetUserLogins; Type: ACL; Schema: dbo; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "AspNetUserLogins" TO tasksuser;


--
-- TOC entry 2238 (class 0 OID 0)
-- Dependencies: 198
-- Name: AspNetUserRoles; Type: ACL; Schema: dbo; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "AspNetUserRoles" TO tasksuser;


--
-- TOC entry 2239 (class 0 OID 0)
-- Dependencies: 193
-- Name: AspNetUsers; Type: ACL; Schema: dbo; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "AspNetUsers" TO tasksuser;


SET search_path = public, pg_catalog;

--
-- TOC entry 2240 (class 0 OID 0)
-- Dependencies: 189
-- Name: Resource; Type: ACL; Schema: public; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "Resource" TO tasksuser;


--
-- TOC entry 2241 (class 0 OID 0)
-- Dependencies: 190
-- Name: ResourceMeasureUnit; Type: ACL; Schema: public; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "ResourceMeasureUnit" TO tasksuser;


--
-- TOC entry 2243 (class 0 OID 0)
-- Dependencies: 188
-- Name: Resource_Id_seq; Type: ACL; Schema: public; Owner: leon
--

GRANT SELECT,USAGE ON SEQUENCE "Resource_Id_seq" TO tasksuser;


--
-- TOC entry 2244 (class 0 OID 0)
-- Dependencies: 187
-- Name: Task; Type: ACL; Schema: public; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "Task" TO tasksuser;


--
-- TOC entry 2245 (class 0 OID 0)
-- Dependencies: 192
-- Name: TaskResource; Type: ACL; Schema: public; Owner: leon
--

GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE "TaskResource" TO tasksuser;


--
-- TOC entry 2247 (class 0 OID 0)
-- Dependencies: 191
-- Name: TaskResource_Id_seq; Type: ACL; Schema: public; Owner: leon
--

GRANT SELECT,USAGE ON SEQUENCE "TaskResource_Id_seq" TO tasksuser;


--
-- TOC entry 2249 (class 0 OID 0)
-- Dependencies: 186
-- Name: Task_Id_seq; Type: ACL; Schema: public; Owner: leon
--

GRANT SELECT,USAGE ON SEQUENCE "Task_Id_seq" TO tasksuser;


-- Completed on 2017-02-26 20:27:03 MSK

--
-- PostgreSQL database dump complete
--
