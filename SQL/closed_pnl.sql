CREATE TABLE closed_pnl (
  id SERIAL PRIMARY KEY,
  userid UUID NOT NULL,
  pnl NUMERIC(15, 3) 
);

