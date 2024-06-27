import { XAxis, YAxis, CartesianGrid, ResponsiveContainer, AreaChart, Area, Tooltip, TooltipProps } from 'recharts';
import { ValueType, NameType } from 'recharts/types/component/DefaultTooltipContent';
import "../stylesheets/PortfolioGraph.css";

interface DataPoint {
  day: number;
  pnl: number;
}

const data: DataPoint[] = [
  { day: 1, pnl: 20 },
  { day: 2, pnl: -15 },
  { day: 3, pnl: 5 },
  { day: 4, pnl: 10 },
  { day: 5, pnl: -8 },
  { day: 6, pnl: 12 },
  { day: 7, pnl: -4 },
  { day: 8, pnl: 18 },
  { day: 9, pnl: -6 },
  { day: 10, pnl: 7 },
];

function customeToolTip({active, payload, label} : TooltipProps<ValueType, NameType>) {
  if (active && payload && payload.length) {
    return (
      <div className='tooltip'>
        <h4>Date: {label}</h4>
        <p>PnL: ${`${payload?.[0].value}`}</p>
      </div>
      );
    }
}

function PortfolioGraph() {

  return (
    <ResponsiveContainer width="70%" height={300}>
      <AreaChart data={data}>
        <Area type="monotone" dataKey="pnl"/>
        <XAxis dataKey="day" />
        <YAxis dataKey="pnl" axisLine={false} tickLine={false} tickFormatter={ (n:number) => `$${n.toFixed(2)}`}/>
        <Tooltip content={customeToolTip}/>
        <CartesianGrid opacity={0.1} vertical={false} />
      </AreaChart>
    </ResponsiveContainer>
  );
}

export default PortfolioGraph;