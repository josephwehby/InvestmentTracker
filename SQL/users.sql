CREATE TABLE users (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  username VARCHAR(32) NOT NULL,
  password_hash VARCHAR(64) NOT NULL,
  salt VARCHAR(24) NOT NULL,
  refresh_token VARCHAR(128),
  token_created TIMESTAMPTZ,
  token_expires TIMESTAMPTZ
);

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";