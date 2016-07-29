CREATE OR REPLACE FUNCTION dbo.pGetPublicArticle (
  ArticleTitle_IN VARCHAR
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
		  "CategorySEOUrl" VARCHAR(255)) AS $$
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
		   "c"."SEOUrl" AS "CategorySEOUrl"
      FROM "dbo"."Articles" "ar"
        LEFT JOIN "dbo"."AspNetUsers" "u" ON "u"."Id" = "ar"."UserID"
        LEFT JOIN "dbo"."Categories" "c" ON "c"."CategoryID" = "ar"."CategoryID"
      WHERE "ar"."IsOnline" = true
	AND "ar"."SEOUrl" = ArticleTitle_IN
      LIMIT 1;

END;
$$ LANGUAGE plpgsql;    
              
              