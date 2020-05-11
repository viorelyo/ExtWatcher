import React, { Component } from "react";
import {
  RadarChart,
  PolarGrid,
  PolarAngleAxis,
  PolarRadiusAxis,
  Radar,
} from "recharts";
import "./Chart.scss";

class PieChartComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      files: [],
      data: [
        { subject: "PDF", A: 0, fullMark: 150 },
        { subject: "DOC", A: 0, fullMark: 150 },
        { subject: "XLS", A: 0, fullMark: 150 },
        { subject: "DLL", A: 0, fullMark: 150 },
        { subject: "EXE", A: 0, fullMark: 150 },
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
            subject: "PDF",
            A: this.getCountByType(this.props.files, "pdf"),
            fullMark: 150,
          },
          {
            subject: "DOC",
            A: this.getCountByType(this.props.files, "doc"),
            fullMark: 150,
          },
          {
            subject: "XLS",
            A: this.getCountByType(this.props.files, "xls"),
            fullMark: 0,
          },
          {
            subject: "DLL",
            A: this.getCountByType(this.props.files, "dll"),
            fullMark: 150,
          },
          {
            subject: "EXE",
            A: this.getCountByType(this.props.files, "exe"),
            fullMark: 150,
          },
        ],
      });
    }
  }

  render() {
    if (!this.props.files || !this.props.files.length) {
      return <div />;
    }

    const data = this.state.data;

    return (
      <RadarChart
        outerRadius={90}
        width={250}
        height={250}
        data={data}
        className="chart"
      >
        <PolarGrid />
        <PolarAngleAxis dataKey="subject" />
        <PolarRadiusAxis />
        <Radar
          name="Mike"
          dataKey="A"
          stroke="#8884d8"
          fill="#8884d8"
          fillOpacity={0.6}
        />
      </RadarChart>
    );
  }

  getCountByType(list, type) {
    return list.filter((x) => x.filetype === type).length;
  }
}

export default PieChartComponent;
