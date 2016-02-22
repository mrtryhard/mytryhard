-- Categories.sql

CREATE TABLE IF NOT EXISTS "dbo"."Categories" (
  "CategoryID" VARCHAR(36) NOT NULL, 
  "ParentCategoryID" VARCHAR(36) DEFAULT '00000000-0000-0000-0000-000000000000',
  "Title" VARCHAR(30) NOT NULL,
  "Description" VARCHAR(255),
  "SEOUrl" VARCHAR(255) NOT NULL,
  CONSTRAINT "unique_CategoryID" UNIQUE ("CategoryID"),
  CONSTRAINT "pk_CategoriesCategoryID" PRIMARY KEY ("CategoryID"),
  CONSTRAINT "fk_CategoriesCategoriesCategoryID" FOREIGN KEY ("ParentCategoryID")
    REFERENCES "dbo"."Categories"("CategoryID"),
  CONSTRAINT "unique_CategoriesTitle" UNIQUE("Title"),
  CONSTRAINT "unique_CategoriesSEOUrl" UNIQUE("SEOUrl")
); 

CREATE INDEX "CategoriesTable_Index"
  ON "dbo"."Categories" ("CategoryID", "Title");

INSERT INTO "dbo"."Categories" ("CategoryID", "Title", "Description", "SEOUrl") VALUES ('00000000-0000-0000-0000-000000000000', 'General', 'General', 'general');