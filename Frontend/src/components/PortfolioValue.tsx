import PropTypes from "prop-types";

type PortfolioValueProps = {
  closed: number,
  unrealized: number
};

function PortfolioValue(props: PortfolioValueProps) {
  return (
    <div className="PortfolioValue">
      <h3>Closed PnL: ${props.closed}</h3>
      <h3>Unrealized PnL: ${props.unrealized}</h3>
    </div>
  );
}
PortfolioValue.propTypes = {
  closed: PropTypes.number.isRequired,
  unrealized: PropTypes.number.isRequired
};

export default PortfolioValue;