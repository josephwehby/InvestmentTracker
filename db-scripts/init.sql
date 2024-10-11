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

CREATE TABLE trades (
	id SERIAL PRIMARY KEY, 
  userid UUID NOT NULL,
	trade_type VARCHAR(10) NOT NULL,
	ticker VARCHAR(20) NOT NULL,
	shares NUMERIC(15,5) NOT NULL,
	price NUMERIC(7,2) NOT NULL,
	fees NUMERIC(7,3) NOT NULL,
	purchase_day TIMESTAMPTZ NOT NULL DEFAULT NOW(),
	sell_day TIMESTAMPTZ
);	

CREATE TABLE historic_pnl (
  id SERIAL PRIMARY KEY,
  userid UUID NOT NULL,
  pnl DECIMAL(15,3) NOT NULL,
  closing_pnl_date DATE NOT NULL DEFAULT NOW()
);


CREATE TABLE closed_pnl (
  id SERIAL PRIMARY KEY,
  userid UUID NOT NULL,
  pnl NUMERIC(15, 3) 
);
