CREATE OR REPLACE FUNCTION dbo.pDeleteCategoryFromId(
    CategoryID_IN VARCHAR(36)
)
RETURNS VOID AS $$
DECLARE parentId VARCHAR(36);
BEGIN
    parentId = (SELECT "ParentCategoryID" AS "parentId"
                    FROM "dbo"."Categories"
                    WHERE "CategoryID" = CategoryID_IN
                    LIMIT 1);

    -- Update articles.
    UPDATE "dbo"."Articles" 
        SET "CategoryID" = parentId
        WHERE "CategoryID" = CategoryID_IN;

    -- Update categories.
    UPDATE "dbo"."Categories"
        SET "ParentCategoryID" = parentId
        WHERE "ParentCategoryID" = CategoryID_IN;

    DELETE FROM "dbo"."Categories" WHERE "CategoryID" = CategoryID_IN;
        
END
$$ LANGUAGE plpgsql;