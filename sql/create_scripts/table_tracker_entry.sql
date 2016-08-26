-- Distance is stored in meters. 
CREATE TABLE IF NOT EXISTS "dbo"."TrackerEntry" (
  "TrackerEntryID" SERIAL NOT NULL, 
  "TrackedSportID" INT NOT NULL,
  "UserID" VARCHAR(36) NOT NULL, 
  "Distance" INT NOT NULL, 
  "DateTimeStart" TIMESTAMP NOT NULL,
  "DateTimeEnd" TIMESTAMP NOT NULL,
  CONSTRAINT "pk_TrackerEntryID" PRIMARY KEY ("TrackerEntryID"),
  CONSTRAINT "fk_TrackerEntryTrackedSportID" FOREIGN KEY ("TrackedSportID")
    REFERENCES "dbo"."TrackedSports" ("TrackedSportID"),
  CONSTRAINT "fk_TrackerEntryUserID" FOREIGN KEY ("UserID") 
    REFERENCES "dbo"."AspNetUsers" ("Id")
);

CREATE INDEX "TrackerEntryIndex" ON "dbo"."TrackerEntry" ("TrackedSportID", "UserID");
