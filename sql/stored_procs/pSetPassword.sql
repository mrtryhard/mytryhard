-- pAddSetPassword
-- Sets the password for the given user with the given hashes. 
-- Does NOT hash password.

CREATE OR REPLACE FUNCTION pSetPassword(
  UserID_IN INTEGER,
  Password_IN VARCHAR(255),
  Super_IN VARCHAR(32)  
) 
RETURNS BOOLEAN AS $$
BEGIN
  -- Update password
  UPDATE "Passwords" 
    SET "Passwords"."Content" = Password_IN,
        "Passwords"."Super" = Super_IN
    WHERE "Passwords"."UserID" = UserID_IN;

  RETURN FOUND; -- Special PGSQL value.
END
$$ LANGUAGE plpgsql;