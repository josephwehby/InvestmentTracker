CREATE TABLE trades (
	id SERIAL PRIMARY KEY, 
	trade_type VARCHAR(10) NOT NULL,
	ticker VARCHAR(20) NOT NULL,
	shares NUMERIC(15,5) NOT NULL,
	buy_price NUMERIC(7,2) NOT NULL,
	sell_price NUMERIC(7,2),
	fees NUMERIC(7,3) NOT NULL,
	purchase_day TIMESTAMPTZ NOT NULL DEFAULT NOW(),
	sell_day TIMESTAMPTZ
);	