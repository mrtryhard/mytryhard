-- No changes from SQL Server version, really
CREATE TABLE "dbo"."AspNetUserLogins" (
    "LoginProvider"       VARCHAR (450) NOT NULL,
    "ProviderKey"         VARCHAR (450) NOT NULL,
    "ProviderDisplayName" VARCHAR NULL,
    "UserId"              VARCHAR (450) NULL,
    CONSTRAINT "PK_IdentityUserLogin_string" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_IdentityUserLogin_string_ApplicationUser_UserId" FOREIGN KEY ("UserId") REFERENCES "dbo"."AspNetUsers" ("Id")
);

