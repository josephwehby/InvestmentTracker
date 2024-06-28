import { XAxis, YAxis, CartesianGrid, ResponsiveContainer, AreaChart, Area, Tooltip, TooltipProps } from 'recharts';
import { ValueType, NameType } from 'recharts/types/component/DefaultTooltipContent';
import "../stylesheets/PortfolioGraph.css";

interface DataPoint {
  day: number;
  pnl: number;
}

const data: DataPoint[] = [
  { day: 1, pnl: 20 },
  { day: 2, pnl: 15 },
  { day: 3, pnl: 5 },
  { day: 4, pnl: 10 },
  { day: 5, pnl: 20 },
  { day: 6, pnl: 12 },
  { day: 7, pnl: 3 },
  { day: 8, pnl: 18 },
  { day: 9, pnl: 10 },
  { day: 10, pnl: 7 },
];

function customeToolTip({active, payload, label} : TooltipProps<ValueType, NameType>) {
  if (active && payload && payload.length) {
    return (
      <div className='tooltip'>
        <h4>Date: {label}</h4>
        <h4>PnL: ${`${payload?.[0].value}`}</h4>
      </div>
      );
    }
}

function PortfolioGraph() {

  return (
    <ResponsiveContainer width="70%" height={400}>
      <AreaChart data={data}>
      <defs>
        <linearGradient id="color" x1="0" y1="0" x2="0" y2="1">
         <stop offset="0%" stopColor="#2451B7" stopOpacity={0.4}></stop> 
         <stop offset="75%" stopColor="#2451B7" stopOpacity={0.05}></stop> 
        </linearGradient>
      </defs>
        <Area dataKey="pnl" fill="url(#color)" />
        <XAxis dataKey="day" tickLine={false} tickFormatter={(tickItem) => tickItem%2 === 0 ? tickItem : ""}/>
        <YAxis dataKey="pnl" axisLine={false} tickLine={false} tickFormatter={ (n:number) => `$${n.toFixed(2)}`}/>
        <Tooltip content={customeToolTip}/>
        <CartesianGrid opacity={0.1} vertical={false} />
      </AreaChart>
    </ResponsiveContainer>
  );
}

export default PortfolioGraph;