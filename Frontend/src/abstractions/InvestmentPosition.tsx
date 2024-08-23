export interface InvestmentPosition {
  ticker: string;
  current_price: number;
  avg_cost: number;
  quantity: number;
  cost_basis: number;
  market_value: number;
  fees: number;
  pnl: number;
  percent_gain: number;
  price_day_difference: number;
}
