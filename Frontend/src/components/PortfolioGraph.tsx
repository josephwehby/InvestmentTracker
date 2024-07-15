import { XAxis, YAxis, CartesianGrid, ResponsiveContainer, AreaChart, Area, Tooltip, TooltipProps, angle } from 'recharts';
import { ValueType, NameType } from 'recharts/types/component/DefaultTooltipContent';
import "../stylesheets/PortfolioGraph.css";

interface DataPoint {
  day: string;
  pnl: number;
}

const raw_data: DataPoint[] = [
  { day: "5/1/2024", pnl: 20 },
  { day: "5/2/2024", pnl: 15 },
  { day: "5/3/2024", pnl: 5 },
  { day: "5/4/2024", pnl: 10 },
  { day: "5/5/2024", pnl: 20 },
  { day: "5/6/2024", pnl: 12 },
  { day: "5/7/2024", pnl: 3 },
  { day: "5/8/2024", pnl: 18 },
  { day: "5/9/2024", pnl: 10 },
  { day: "5/10/2024", pnl: 7 },
];

const data = raw_data.filter((_, index) => index % 2 === 0);

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
        <XAxis dataKey="day" />
        <YAxis dataKey="pnl" axisLine={false} tickLine={false} tickFormatter={ (n:number) => `$${n.toFixed(2)}`}/>
        <Tooltip content={customeToolTip}/>
        <CartesianGrid opacity={0.1} vertical={false} />
      </AreaChart>
    </ResponsiveContainer>
  );
}

export default PortfolioGraph;