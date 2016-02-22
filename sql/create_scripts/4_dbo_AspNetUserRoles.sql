-- No changes from SQL Server
CREATE TABLE "dbo"."AspNetUserRoles" (
    "UserId" VARCHAR (450) NOT NULL,
    "RoleId" VARCHAR (450) NOT NULL,
    CONSTRAINT "PK_IdentityUserRole_string" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_IdentityUserRole_string_IdentityRole_RoleId" FOREIGN KEY ("RoleId") REFERENCES "dbo"."AspNetRoles" ("Id"),
    CONSTRAINT "FK_IdentityUserRole_string_ApplicationUser_UserId" FOREIGN KEY ("UserId") REFERENCES "dbo"."AspNetUsers" ("Id")
);

