"use client";

import { Pie, Legend, Cell, PieChart, ResponsiveContainer, Tooltip } from "recharts";

const data = [
  { name: "Альжанов Тлек Кабыкенович", value: 1139097 },
  { name: "Альжанова Татьяна Муратовна", value: 149850 },
  { name: "АЛЬЖАНОВА ШЫНАР ТАНИРБЕРГЕНОВНА", value: 149850 },
  { name: "АПЕНКО СЕРГЕЙ СЕРГЕЕВИЧ", value: 59008 },
  { name: "БАБАЕВ САБИТ АБДРАХМАНОВИЧ", value: 1849 },
  { name: 'МП "ЗЕРЕК" (ТОО "МАДИКАЙ")', value: 346 },
];

const COLORS = ['#756AB6', '#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#A1EEBD'];


export default function StockHolders() {
  return (
    <ResponsiveContainer>
      <PieChart width={1000} height={1000} >
        <Legend layout="vertical" align="right" verticalAlign="middle" />
        <Pie
          dataKey="value"
          data={data}
          paddingAngle={2}
          label
        >
          {data.map((entry, index) => (
            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
          ))}
        </Pie>
        <Tooltip />
      </PieChart>

    </ResponsiveContainer>
  );
}
