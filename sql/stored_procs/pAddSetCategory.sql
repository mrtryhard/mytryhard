CREATE OR REPLACE FUNCTION dbo.pAddSetCategory(
    CategoryID_IN VARCHAR(36),
    ParentCategoryID_IN VARCHAR(36),
    Title_IN VARCHAR(30),
    Description_IN VARCHAR(255),
    SEOUrl_IN VARCHAR(255)
)
RETURNS VOID AS $$
BEGIN
    UPDATE "dbo"."Categories"
        SET "ParentCategoryID" = ParentCategoryID_IN,
            "Title" = Title_IN,
            "Description" = Description_IN,
            "SEOUrl" = SEOUrl_IN
        WHERE "CategoryID" = CategoryID_IN;

    -- If not found, then it's new.
    IF NOT FOUND THEN 
        INSERT INTO "dbo"."Categories" (
            "CategoryID",
            "ParentCategoryID",
            "Title",
            "Description",
            "SEOUrl")
            VALUES (
                CategoryID_IN,
                ParentCategoryID_IN,
                Title_IN,
                Description_IN,
                SEOUrl_IN
                );
     END IF; 
END;
$$ LANGUAGE plpgsql;
