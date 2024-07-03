CREATE TABLE historic_pnl (
  id SERIAL PRIMARY KEY,
  pnl DECIMAL(15,3) NOT NULL,
  closing_pnl_date DATE NOT NULL DEFAULT NOW()
);