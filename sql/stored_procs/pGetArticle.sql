CREATE OR REPLACE FUNCTION dbo.pGetArticle (
  ArticleID_IN VARCHAR
)
RETURNS TABLE("AuthorId" VARCHAR(36), 
	      "ArticleId" VARCHAR(36),
	      "CategoryId" VARCHAR(36),
	      "AuthorName" VARCHAR(20),
	      "CategoryTitle" VARCHAR(255),
	      "PublishedDate" timestamp without time zone,
	      "LastEditDate" timestamp without time zone,
              "Title" VARCHAR(255),
              "Content" TEXT,
              "IsCommentAllowed" boolean,
              "IsOnline" boolean,
              "SEOUrl" VARCHAR(255)) AS $$
BEGIN

  RETURN QUERY
    SELECT "ar"."UserID" AS "AuthorId",
           "ar"."ArticleID" AS "ArticleId",
           "ar"."CategoryID" AS "CategoryId",
           "u"."ShownName" AS "AuthorName",
           "c"."Title" AS "CategoryTitle",
           "ar"."PublishedDate" AS "PublishedDate",
           "ar"."LastEditionDate" AS "LastEditDate",
           "ar"."Title" AS "Title",
           "ar"."Content" AS "Content",
           "ar"."IsCommentAllowed" AS "IsCommentAllowed",
           "ar"."IsOnline" AS "IsOnline",
           "ar"."SEOUrl" AS "SEOUrl"
      FROM "dbo"."Articles" "ar"
        LEFT JOIN "dbo"."AspNetUsers" "u" ON "u"."Id" = "ar"."UserID"
        LEFT JOIN "dbo"."Categories" "c" ON "c"."CategoryID" = "ar"."CategoryID"
      WHERE "ar"."ArticleID" = ArticleID_IN
      LIMIT 1;

END;
$$ LANGUAGE plpgsql;    
              
              