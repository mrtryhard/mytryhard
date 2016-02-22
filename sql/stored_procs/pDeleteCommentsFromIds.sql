-- pDeleteCommentsFromCommentsIds.sql

CREATE OR REPLACE FUNCTION pDeleteCommentsFromIds(
  CommentsIDs_IN INTEGER[]
)
RETURNS BOOLEAN AS $$
BEGIN
  DELETE FROM "Comments" WHERE "CommentID" = ANY(CommentsIDs_IN);

  RETURN FOUND;
END
$$ LANGUAGE plpgsql;