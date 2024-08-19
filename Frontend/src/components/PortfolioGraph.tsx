import { useState, useEffect } from "react";
import { XAxis, YAxis, CartesianGrid, ResponsiveContainer, AreaChart, Area, Tooltip, TooltipProps } from 'recharts';
import { ValueType, NameType } from 'recharts/types/component/DefaultTooltipContent';
import useAxios from "../hooks/useAxios";
import "../stylesheets/PortfolioGraph.css";

interface DataPoint {
  pnl: number;
  closing_pnl_date: string;
}

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
  const [data, setData] = useState<DataPoint[] | null>(null);
  const axiosInstance = useAxios();
  
  function swap(closing_date: string) {
    const [year, month, day] = closing_date.split("-");
    return `${month}-${day}-${year}`
  }

  async function getGraphData() {

    try {
      const response = await axiosInstance.get("/graph");
      const graph = response.data.map((point) => ({
        ...point,
        closing_pnl_date: swap(point.closing_pnl_date.split("T")[0]),
      }));
      setData(graph);
    } catch (error) {
      console.error("Error while fetching portfolio graph", error);
    }
  }

  useEffect(() => {
    getGraphData();
  }, []);


  return (
    <ResponsiveContainer width="70%" height={440} max-wdith="80%">
      <AreaChart data={data || []} margin={{ top: 20, right: 5, left: 20, bottom: 60}}>
      <defs>
        <linearGradient id="color" x1="0" y1="0" x2="0" y2="1">
         <stop offset="0%" stopColor="#2451B7" stopOpacity={0.4}></stop> 
         <stop offset="75%" stopColor="#2451B7" stopOpacity={0.05}></stop> 
        </linearGradient>
      </defs>
        <Area dataKey="pnl" fill="url(#color)" />
        <XAxis dataKey="closing_pnl_date" padding={{ left: 20 , right: 20 }} tick={customXAxis}/>
        <YAxis dataKey="pnl" axisLine={false} tickLine={false} tickFormatter={ (n:number) => `$${n.toFixed(2)}`}/>
        <Tooltip content={customeToolTip}/>
        <CartesianGrid opacity={0.1} vertical={false} />
      </AreaChart>
    </ResponsiveContainer>
  );
}

export default PortfolioGraph;
