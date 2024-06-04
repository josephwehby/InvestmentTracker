import PropTypes from "prop-types";
import "../stylesheets/PortfolioValue.css";

type PortfolioValueProps = {
  closed: number,
  unrealized: number
};

function PortfolioValue(props: PortfolioValueProps) {
  
  function getColor(value: number) {
    if (value < 0) {
      return "red";
    }
    return "green";
  }
  
  return (
    <div className="PortfolioValue">
      <div className="summary">
        <p>
          Unrealized: <span style= {{ color: getColor(props.unrealized)}}>${props.unrealized}</span>
        </p>
        <p>
          Closed: <span style= {{ color: getColor(props.closed)}}>${props.closed}</span>
          </p>
      </div>
    </div>
  );
}
PortfolioValue.propTypes = {
  closed: PropTypes.number.isRequired,
  unrealized: PropTypes.number.isRequired
};

export default PortfolioValue;