-- The only change is the id field from Identity int to SERIAL
CREATE TABLE "dbo"."AspNetUserClaims" (
    "Id"         SERIAL NOT NULL,
    "ClaimType"  VARCHAR NULL,
    "ClaimValue" VARCHAR NULL,
    "UserId"     VARCHAR (450) NULL,
    CONSTRAINT "PK_IdentityUserClaim_string" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_IdentityUserClaim_string_ApplicationUser_UserId" FOREIGN KEY ("UserId") REFERENCES "dbo"."AspNetUsers" ("Id")
);

