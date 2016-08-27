CREATE OR REPLACE FUNCTION dbo.pGetTrackerEntriesForUser (
  UserID_IN VARCHAR(36),
  SportID_IN INT = NULL
)
RETURNS TABLE("EntryId" INT, 
  "UserId" VARCHAR(36), 
  "SportId" INT, 
  "Distance" INT,
  "DateTimeStart" TIMESTAMP,
  "DateTimeEnd" TIMESTAMP,
  "SportName" VARCHAR(32)) AS $$
BEGIN
   RETURN QUERY 
     SELECT "te"."TrackerEntryID" AS "EntryId",
	   "te"."UserID" AS "UserId", 
	   "te"."TrackedSportID" AS "SportId",
	   "te"."Distance" AS "Distance",
	   "te"."DateTimeStart" AS "DateTimeStart",
	   "te"."DateTimeEnd" AS "DateTimeEnd",
	   "ts"."Title" AS "SportName"
	 FROM "dbo"."TrackerEntry" "te"
	   LEFT JOIN "dbo"."TrackedSports" "ts" ON "ts"."TrackedSportID" = "te"."TrackedSportID"
	 WHERE "te"."UserID" = UserID_IN 
		AND 1 = CASE SportID_IN 
		  WHEN -1 THEN 1 
		  ELSE 
		    CASE 
			  WHEN "te"."TrackedSportID" = SportID_IN THEN 1 
		      ELSE 0 
			END
		  END
	 ORDER BY "ts"."Title", "te"."DateTimeStart";
END;
$$ LANGUAGE plpgsql;