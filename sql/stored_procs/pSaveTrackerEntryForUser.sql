CREATE OR REPLACE FUNCTION dbo.pSaveTrackerEntryForUser (
  UserID_IN VARCHAR(36),
  SportID_IN INT,
  Distance_IN INT,
  DateTimeStart_IN TIMESTAMP,
  DateTimeEnd_IN TIMESTAMP
)
RETURNS VOID AS $$
BEGIN
  INSERT INTO "dbo"."TrackerEntry" ("TrackedSportID", "UserID", "Distance", "DateTimeStart", "DateTimeEnd")
    VALUES (SportID_IN, UserID_IN, Distance_IN, DateTimeStart_IN, DateTimeEnd_IN);
END;
$$ LANGUAGE plpgsql;