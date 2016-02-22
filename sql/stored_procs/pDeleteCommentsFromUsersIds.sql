-- pDeleteCommentsFromUsersIds.sql

CREATE OR REPLACE FUNCTION pDeleteCommentsFromUsersIds(
  UsersIDs_IN INTEGER[]
)
RETURNS BOOLEAN AS $$
BEGIN
  DELETE FROM "Comments" WHERE "UserID" = ANY(UsersIDs_IN);

  RETURN FOUND;
END
$$ LANGUAGE plpgsql;