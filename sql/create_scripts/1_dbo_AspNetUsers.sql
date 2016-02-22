-- Table: dbo."AspNetUsers"
-- Changes compared to the SQL Server version:
--   o Each BIT fields were changed to boolean for compatibility.
--   o Column ShownName was added
--   o NVARCHAR converted to VARCHAR
--   o Unique constraint added on ShownName and Email

CREATE TABLE dbo."AspNetUsers"
(
  "Id" character varying(450) NOT NULL,
  "AccessFailedCount" integer NOT NULL,
  "ConcurrencyStamp" character varying,
  "Email" character varying(256),
  "LockoutEnd" timestamp with time zone,
  "NormalizedEmail" character varying(256),
  "NormalizedUserName" character varying(256),
  "PasswordHash" character varying,
  "PhoneNumber" character varying,
  "SecurityStamp" character varying,
  "UserName" character varying(30),
  "FirstName" character varying(30),
  "LastName" character varying(30),
  "Signature" character varying(255),
  "EmailConfirmed" boolean NOT NULL,
  "LockoutEnabled" boolean NOT NULL,
  "TwoFactorEnabled" boolean NOT NULL,
  "PhoneNumberConfirmed" boolean NOT NULL,
  "ShownName" character varying(30) NOT NULL, -- Real shown name
  CONSTRAINT "PK_ApplicationUser" PRIMARY KEY ("Id"),
  CONSTRAINT "UN_ShownName" UNIQUE ("ShownName"),
  CONSTRAINT "UN_Email" UNIQUE ("Email")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."AspNetUsers"
  OWNER TO kotsuki;
COMMENT ON COLUMN dbo."AspNetUsers"."ShownName" IS 'Real shown name';


-- Index: dbo."EmailIndex"

-- DROP INDEX dbo."EmailIndex";

CREATE INDEX "EmailIndex"
  ON dbo."AspNetUsers"
  USING btree
  ("NormalizedEmail" COLLATE pg_catalog."default");

-- Index: dbo."UserNameIndex"

-- DROP INDEX dbo."UserNameIndex";

CREATE INDEX "UserNameIndex"
  ON dbo."AspNetUsers"
  USING btree
  ("NormalizedUserName" COLLATE pg_catalog."default");

