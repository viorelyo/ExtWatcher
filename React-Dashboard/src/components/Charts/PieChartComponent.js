import React, { Component } from "react";
import { PieChart, Pie, Cell, Tooltip, Legend } from "recharts";
import "./Chart.scss";

class PieChartComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      files: [],
      data: [
        {
          name: "benign",
          value: 0,
        },
        {
          name: "malicious",
          value: 0,
        },
      ],
    };
  }

  static getDerivedStateFromProps(props, state) {
    if (props.files !== state.files) {
      return {
        files: props.files,
      };
    }
    return null;
  }

  componentDidUpdate(prevProps, prevState) {
    if (this.props.files !== prevProps.files) {
      this.setState({
        files: this.props.files,
        data: [
          {
            name: "benign",
            value: this.getCountByVerdict(this.state.files, "benign"),
          },
          {
            name: "malicious",
            value: this.getCountByVerdict(this.state.files, "malicious"),
          },
        ],
      });
    }
  }

  render() {
    if (!this.props.files || !this.props.files.length) {
      return <div />;
    }

    const COLORS = ["#82ca9d", "#ca5240"];
    const data = this.state.data;

    return (
      <PieChart width={250} height={250} className="chart">
        <Pie
          dataKey="value"
          data={data}
          innerRadius={60}
          outerRadius={80}
          fill="#8884d8"
          label
          className="chart"
        >
          {data.map((entry, index) => (
            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
          ))}
        </Pie>
        <Tooltip />
      </PieChart>
    );
  }

  getCountByVerdict(list, verdict) {
    return list.filter((x) => x.result === verdict).length;
  }
}

export default PieChartComponent;
