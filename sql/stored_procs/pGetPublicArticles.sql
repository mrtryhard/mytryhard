drop function dbo.pGetPublicArticles(integer, integer);

CREATE OR REPLACE FUNCTION dbo.pGetPublicArticles (
  NbStart_IN INTEGER,
  Count_IN INTEGER
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
           "ar"."SEOUrl" AS "SEOUrl"
      FROM "dbo"."Articles" "ar"
        LEFT JOIN "dbo"."AspNetUsers" "u" ON "u"."Id" = "ar"."UserID"
        LEFT JOIN "dbo"."Categories" "c" ON "c"."CategoryID" = "ar"."CategoryID"
      WHERE "ar"."IsOnline" = true
      ORDER BY "ar"."PublishedDate" DESC
      LIMIT Count_IN OFFSET NbStart_IN;

END;
$$ LANGUAGE plpgsql;    
              
              