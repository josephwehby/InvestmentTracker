import "../stylesheets/OrderEntry.css";
import { useState, ChangeEvent } from "react";

function OrderEntry() {
  const [ticker, setTicker] = useState<string>("");
  const [shares, setShares] = useState<number>(0);
  const [price, setPrice] = useState<number>(0);
  const [ordertype, setOrdertype] = useState<string>("buy");

  function checkNumericalInput(e: ChangeEvent<HTMLInputElement>) {
    const { id, value } = e.target;
    const clean = value.replace(/[^\d.-]/g, "");
    console.log(clean);
    const clean_num = clean === "" ? 0 : parseFloat(clean);
    if(id === "shares") {
      setShares(clean_num);
    } else {
      setPrice(clean_num);
    }
  }

  function checkStringInput(e: ChangeEvent<HTMLInputElement>) {
    const { value } = e.target;
    const clean = value.replace(/\d/g, "");

    setTicker(clean.toUpperCase());
  }

  return (
    <>
      <div className="order-entry">
        <form>
          <input type="text" id="ticker" placeholder="Ticker" value={ticker} onChange={checkStringInput} required />
          <input type="text" id="shares" placeholder="Shares" value={shares} onChange={checkNumericalInput} required />
          <input type="text" id="price" placeholder="Price" value={price} onChange={checkNumericalInput} required/>
          <select id="order-type" value={ordertype} onChange={(e) => setOrdertype(e.target.value)}>
            <option value="buy">Buy</option>
            <option value="sell">Sell</option>
          </select>
          <input type="submit" value="Submit"></input>
        </form>
      </div>
    </>
  );
}

export default OrderEntry;