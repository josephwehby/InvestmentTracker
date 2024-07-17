import { XAxis, YAxis, CartesianGrid, ResponsiveContainer, AreaChart, Area, Tooltip, TooltipProps } from 'recharts';
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
  { day: "5/11/2024", pnl: 30 },
  { day: "5/12/2024", pnl: 28 },
  { day: "5/13/2024", pnl: 20 },
  { day: "5/14/2024", pnl: 29 },
  { day: "5/15/2024", pnl: 23 },
  { day: "5/16/2024", pnl: 16 },
  { day: "5/17/2024", pnl: 21 },
];

const data = raw_data.filter((_, index) => index % 2 === 0);

function customeToolTip({active, payload, label} : TooltipProps<ValueType, NameType>) {
  if (active && payload && payload.length) {
    return (
      <div className='tooltip'>
        <h5>Date: {label}</h5>
        <h5>PnL: ${`${payload?.[0].value}`}</h5>
      </div>
      );
    }
}

function customXAxis({ x, y, payload}: any) {
  return (
    <g transform={`translate(${x},${y})`}>
      <text x={0} y={0} dy={16} textAnchor='end' fill="#666" transform='rotate(-35)'>{payload.value}</text>
    </g>
  );
}

function PortfolioGraph() {

  return (
    <ResponsiveContainer width="70%" height={440} max-wdith="80%">
      <AreaChart data={data} margin={{ top: 20, right: 5, left: 20, bottom: 30}}>
      <defs>
        <linearGradient id="color" x1="0" y1="0" x2="0" y2="1">
         <stop offset="0%" stopColor="#2451B7" stopOpacity={0.4}></stop> 
         <stop offset="75%" stopColor="#2451B7" stopOpacity={0.05}></stop> 
        </linearGradient>
      </defs>
        <Area dataKey="pnl" fill="url(#color)" />
        <XAxis dataKey="day" padding={{ left: 20 , right: 20 }} tick={customXAxis}/>
        <YAxis dataKey="pnl" axisLine={false} tickLine={false} tickFormatter={ (n:number) => `$${n.toFixed(2)}`}/>
        <Tooltip content={customeToolTip}/>
        <CartesianGrid opacity={0.1} vertical={false} />
      </AreaChart>
    </ResponsiveContainer>
  );
}

export default PortfolioGraph;