-- pDeleteArticles.sql

CREATE OR REPLACE FUNCTION dbo.pDeleteArticlesFromIds(
  ArticlesIDs_IN VARCHAR(36)[]
)
RETURNS VOID AS $$
BEGIN
  DELETE FROM "dbo"."Comments" WHERE "ArticleID" = ANY(ArticlesIDs_IN);
  DELETE FROM "dbo"."Articles" WHERE "ArticleID" = ANY(ArticlesIDs_IN);
END
$$ LANGUAGE plpgsql;