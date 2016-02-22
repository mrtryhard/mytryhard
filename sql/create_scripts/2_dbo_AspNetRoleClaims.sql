-- The only change is the Id converted from IDENTITY int to SERIAL
CREATE TABLE "dbo"."AspNetRoleClaims" (
    "Id"         SERIAL NOT NULL,
    "ClaimType"  VARCHAR NULL,
    "ClaimValue" VARCHAR NULL,
    "RoleId"     VARCHAR (450) NULL,
    CONSTRAINT "PK_IdentityRoleClaim_string" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_IdentityRoleClaim_string_IdentityRole_RoleId" FOREIGN KEY ("RoleId") REFERENCES "dbo"."AspNetRoles" ("Id")
);

