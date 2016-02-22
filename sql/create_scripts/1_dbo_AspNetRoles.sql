-- Not a lot of differences compared to the SQL Server version
-- NVARCHAR changed to VARCHAR.
CREATE TABLE "dbo"."AspNetRoles" (
    "Id"               VARCHAR (450) NOT NULL,
    "ConcurrencyStamp" VARCHAR NULL,
    "Name"             VARCHAR (256) NULL,
    "NormalizedName"   VARCHAR (256) NULL,
    CONSTRAINT "PK_IdentityRole" PRIMARY KEY ("Id")
);



CREATE INDEX "RoleNameIndex"
    ON "dbo"."AspNetRoles"("NormalizedName" ASC);

