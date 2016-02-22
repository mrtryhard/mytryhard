CREATE OR REPLACE FUNCTION dbo.pGetGeneralStats()
    RETURNS TABLE(
        "LastArticleTitle" VARCHAR(255),
        "PopularArticleTitle" VARCHAR(255),
        "PublishedArticleCount" INTEGER,
        "DraftArticleCount" INTEGER,
        "LockedArticleCount" INTEGER,
        "LastUserNameRegistered" VARCHAR(20),
        "RegisteredUserCount" INTEGER,
        "BannedUserCount" INTEGER,
        "LastCommentContent" VARCHAR(255),
        "TopCommentingUserName" VARCHAR(20),
        "ApprovedCommentCount" INTEGER,
        "PendingCommentCount" INTEGER,
        "PopularPlanetName" VARCHAR(20),
        "TopClickerUserName" VARCHAR(20),
        "TotalPlanetClickCount" INTEGER
    ) AS $$
DECLARE lat VARCHAR(255); 
DECLARE pat VARCHAR(255);
DECLARE pac INTEGER;
DECLARE dac INTEGER;
DECLARE lac INTEGER;
DECLARE lunr VARCHAR(20);
DECLARE ruc INTEGER;
DECLARE buc INTEGER;
DECLARE lcc VARCHAR(255);
DECLARE tcun VARCHAR(20);
DECLARE acc INTEGER;
DECLARE pcc INTEGER;
DECLARE ppn VARCHAR(20);
DECLARE tcunn VARCHAR(20);
DECLARE tpcc INTEGER;
BEGIN
-- LastArticleTitle
lat = (SELECT "Title" 
           FROM "dbo"."Articles" 
           WHERE "IsOnline" = TRUE 
           ORDER BY "PublishedDate" DESC
           LIMIT 1);

-- PopularArticleTitle           
pat = (SELECT "hola"."Title" FROM (SELECT "Title", CAST(COUNT(*) AS INTEGER) AS "Count"
	                               FROM "dbo"."Articles" "sar"
	                               JOIN "dbo"."Comments" "c" ON "c"."ArticleID" = "sar"."ArticleID"
			               GROUP BY "Title"
	                               ORDER BY "Count" DESC 
	                               LIMIT 1) "hola"
       LIMIT 1);
	    
-- PublishedArticleCount	  
pac = (SELECT CAST(COUNT(*) AS INTEGER) AS "PublishedArticleCount" 
           FROM "dbo"."Articles" 
	   WHERE "IsOnline" = TRUE);

-- DraftArticleCount                   
dac = (SELECT CAST(COUNT(*) AS INTEGER) AS "DraftArticleCount" 
           FROM "dbo"."Articles"
	   WHERE "IsOnline" = FALSE);

-- LockedArticleCount
lac = (SELECT CAST(COUNT(*) AS INTEGER) AS "LockedArticleCount"
	   FROM "dbo"."Articles"
	   WHERE "IsOnline" = TRUE 
	       AND "IsCommentAllowed" = FALSE);
	       
-- LastUserNameRegistered 
lunr = (SELECT "UserName"
	   FROM "dbo"."AspNetUsers" 
	   ORDER BY "LockoutEnd" DESC
	   LIMIT 1);
	   
-- RegisteredUserCount 
ruc = (SELECT CAST(COUNT(*) AS INTEGER) "UserCount"
           FROM "dbo"."AspNetUsers"
	   WHERE "EmailConfirmed" = TRUE);
	       
    RETURN QUERY
	(SELECT 
	    lat AS "LastArticleTitle",
	    pat AS "PopularArticleTitle",
	    pac AS "PublishedArticleCount",
	    dac AS "DraftArticleCount",
	    lac AS "LockedArticleCount",
	    lunr AS "LastUserNameRegistered",
	    ruc AS "RegisteredUserCount",
	    0 AS "BannedUserCount",
	    CAST('' AS VARCHAR) AS "LastCommentContent",
	    CAST('' AS VARCHAR) AS "TopCommentingUserName",
	    0 AS "ApprovedCommentCount",
	    0 AS "PendingCommentCount",
	    CAST('' AS VARCHAR) AS "PopularPlanetName",
	    CAST('' AS VARCHAR) AS "TopClickerUserName",
	    0 AS "TotalPlanetClickCount");
END;
$$ LANGUAGE plpgsql;  