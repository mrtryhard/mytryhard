--  
CREATE TABLE IF NOT EXISTS "dbo"."Articles" (
  "ArticleID" VARCHAR(36) NOT NULL,
  "CategoryID" VARCHAR(36),
  "UserID" VARCHAR(36) NOT NULL,
  "PublishedDate" TIMESTAMP,
  "LastEditionDate" TIMESTAMP DEFAULT current_timestamp,
  "Title" VARCHAR(255) NOT NULL,
  "Content" TEXT NOT NULL,
  "IsCommentAllowed" BOOLEAN DEFAULT true,
  "IsOnline" BOOLEAN DEFAULT true,
  "SEOUrl" VARCHAR(255) NOT NULL,
  CONSTRAINT "pk_ArticlesArticleID" PRIMARY KEY ("ArticleID"),
  CONSTRAINT "fk_ArticlesCategoriesCategoryID" FOREIGN KEY ("CategoryID")
      REFERENCES "dbo"."Categories"("CategoryID"),
  CONSTRAINT "fk_ArticlesUsersUserID" FOREIGN KEY ("UserID")
      REFERENCES "dbo"."AspNetUsers"("Id"),
  CONSTRAINT "unique_ArticlesSEOUrl" UNIQUE("SEOUrl")
);

-- We'll often search by title / Published date
CREATE INDEX "ArticlesTable_Index" 
  ON "dbo"."Articles" ("PublishedDate", "SEOUrl");