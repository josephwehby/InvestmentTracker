CREATE TABLE historic_pnl (
  id SERIAL PRIMARY KEY,
  userid UUID NOT NULL,
  pnl DECIMAL(15,3) NOT NULL,
  closing_pnl_date DATE NOT NULL DEFAULT NOW()
);