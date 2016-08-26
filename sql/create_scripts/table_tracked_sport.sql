CREATE TABLE IF NOT EXISTS "dbo"."TrackedSports" (
  "TrackedSportID" SERIAL NOT NULL,
  "Title" VARCHAR(32) NOT NULL,
  "Description" VARCHAR(255),
  CONSTRAINT "unique_TrackedSportID" UNIQUE ("TrackedSportID"), 
  CONSTRAINT "pk_TrackedSportID" PRIMARY KEY ("TrackedSportID"),
  CONSTRAINT "unique_TrackedSportTitle" UNIQUE ("Title")
);

CREATE INDEX "TrackedSports_Index" ON "dbo"."TrackedSports" ("TrackedSportID");
INSERT INTO "dbo"."TrackedSports" ("Title", "Description") 
  VALUES ('Jogging', 'Jogging r�gulier.'); 
INSERT INTO "dbo"."TrackedSports" ("Title", "Description") 
  VALUES ('Natation', 'Woush woush.'); 
INSERT INTO "dbo"."TrackedSports" ("Title", "Description") 
  VALUES ('Marche', 'Pour les relax.');
INSERT INTO "dbo"."TrackedSports" ("Title", "Description") 
  VALUES ('Course', 'Pour les initi�s.');
INSERT INTO "dbo"."TrackedSports" ("Title", "Description") 
  VALUES ('Kayak', 'Paisible, ou peut-�tre pas.');
INSERT INTO "dbo"."TrackedSports" ("Title", "Description") 
  VALUES ('Cano�', 'Allez, allez!');   
INSERT INTO "dbo"."TrackedSports" ("Title", "Description") 
  VALUES ('Aviron', 'Un rapide!');  
INSERT INTO "dbo"."TrackedSports" ("Title", "Description") 
  VALUES ('Squatch', 'Pour les intense'); 