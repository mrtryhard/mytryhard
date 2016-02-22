-- Comments.sql (not used yet)

CREATE TABLE IF NOT EXISTS "dbo"."Comments" (
  "CommentID" VARCHAR(36) NOT NULL,
  "ArticleID" VARCHAR(36) NOT NULL,
  "UserID" VARCHAR(36),
  "Email" VARCHAR(257),
  "ShownName" VARCHAR(20),
  "Content" TEXT NOT NULL,
  CONSTRAINT "pk_CommentsCommentID" PRIMARY KEY ("CommentID"),
  CONSTRAINT "fk_CommentsArticlesArticleID" FOREIGN KEY ("ArticleID")
    REFERENCES "dbo"."Articles"("ArticleID"),
  CONSTRAINT "fk_CommentsUsersUserID" FOREIGN KEY ("UserID")
    REFERENCES "dbo"."AspNetUsers"("Id"),
  CONSTRAINT "chk_UserID_or_Email" CHECK (
      "UserID" IS NOT NULL OR "Email" IS NOT NULL
    )
);

CREATE INDEX "CommentsTable_Index"
  ON "dbo"."Comments" ("ArticleID", "UserID", "Email");