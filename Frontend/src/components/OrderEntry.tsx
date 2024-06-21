import "../stylesheets/OrderEntry.css";
import { useState, ChangeEvent } from "react";

function OrderEntry() {
  const [ticker, setTicker] = useState<string>("");
  const [shares, setShares] = useState<number>(0);
  const [price, setPrice] = useState<number>(0);
  const [ordertype, setOrdertype] = useState<string>("buy");
  const [fees, setFees] = useState<number>(0);

  function checkNumericalInput(e: ChangeEvent<HTMLInputElement>) {
    const { id, value } = e.target;    
    const clean_num = parseFloat(value);
    if(id === "shares") {
      setShares(clean_num);
    } else if (id === "price"){
      setPrice(clean_num);
    } else {
      setFees(clean_num);
    }
  }

  function checkStringInput(e: ChangeEvent<HTMLInputElement>) {
    const { value } = e.target;
    const clean = value.replace(/\d/g, "");
    setTicker(clean.toUpperCase());
  }

  async function addTrade() {
    const response = await fetch("https://localhost:7274/investments/", {
      method: 'POST',
      headers: {
        'Accept':'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        'ticker': ticker,
        'trade_type': ordertype,
        'shares': shares,
        'buy_price': price,
        'fees': fees
      })
    });
    console.log(response);
  }

  return (
    <>
      <div className="order-entry">
        <form onSubmit={addTrade}>
          <div className="form-group">
            <span>Ticker:</span>
            <input type="text" id="ticker" placeholder="SPY" value={ticker} onChange={checkStringInput} required />
          </div>
          
          <div className="form-group">
            <span>Shares:</span>
            <input type="number" step=".1" id="shares" placeholder="Shares" value={shares} onChange={checkNumericalInput} required />
          </div>
          
          <div className="form-group">
            <span>Price:</span>
            <input type="number" step=".1" id="price" placeholder="Price" value={price} onChange={checkNumericalInput} required/>
          </div>

          <div className="form-group">
            <span>Fees:</span>
            <input type="number" step=".1" id="fees" placeholder="Fees" value={fees} onChange={checkNumericalInput} required/>
          </div>

          <div className="horizontal">
            <div className="form-group-horizontal">
              <select id="order-type" value={ordertype} onChange={(e) => setOrdertype(e.target.value)}>
                <option value="buy">Buy</option>
                <option value="sell">Sell</option>
              </select>
            </div>
            <div className="form-group-horizontal">
              <input id="submit-btn" type="submit" value="Submit"></input>
            </div>
          </div>
        </form>
      </div>
    </>
  );
}

export default OrderEntry;