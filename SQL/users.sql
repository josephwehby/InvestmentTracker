CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  username VARCHAR(32) NOT NULL,
  password_hash VARCHAR(64) NOT NULL,
  salt VARCHAR(24) NOT NULL,
  refresh_token VARCHAR(128),
  token_created TIMESTAMPTZ,
  token_expires TIMESTAMPTZ
);