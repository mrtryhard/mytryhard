-- pAddSetArticle

CREATE OR REPLACE FUNCTION dbo.pAddSetArticle (
  UserID_IN VARCHAR(36),
  Title_IN VARCHAR(255),
  ArticleID_IN VARCHAR(36),
  LastEditionDate_IN timestamp without time zone,
  PublishedDate_IN timestamp without time zone,
  SEOUrl_IN VARCHAR(255),
  Content_IN TEXT,
  CategoryID_IN VARCHAR(36) DEFAULT NULL,
  IsCommentAllowed_IN BOOLEAN DEFAULT true, 
  IsOnline_IN BOOLEAN DEFAULT true
)
RETURNS VOID AS $$
BEGIN
  
    UPDATE "dbo"."Articles"
    SET 
      "CategoryID" = CategoryID_IN, 
      "Title" = Title_IN, 
      "Content" = Content_IN, 
      "IsCommentAllowed" = IsCommentAllowed_IN,
      "IsOnline" = IsOnline_IN,
      "SEOUrl" = SEOUrl_IN,
      "PublishedDate" = PublishedDate_IN,
      "LastEditionDate" = LastEditionDate_IN
    WHERE "ArticleID" = ArticleID_IN; 
    
    -- Check if we found.
    IF NOT FOUND THEN 
        INSERT INTO "dbo"."Articles" ("ArticleID",
	    "UserID",
            "CategoryID", 
            "Title", 
            "Content", 
            "IsCommentAllowed", 
            "IsOnline",
            "SEOUrl",
            "PublishedDate",
            "LastEditionDate")
          VALUES (ArticleID_IN, 
            UserID_IN,
            CategoryID_IN, 
            Title_IN, 
            Content_IN, 
            IsCommentAllowed_IN, 
            IsOnline_IN,
            SEOUrl_IN,
            PublishedDate_IN,
            LastEditionDate_IN
          );
    END IF;

END;
$$ LANGUAGE plpgsql;