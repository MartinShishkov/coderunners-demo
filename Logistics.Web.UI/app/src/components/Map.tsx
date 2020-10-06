import React from 'react';
import { Graph } from "react-d3-graph";
import IEdge from '../models/IEdge';

const graphConfig = {
    initialZoom: 1.8,
    staticGraph: false,
    d3: {
        gravity: -100,
    },
    directed: false,
    height: 400,
    focusZoom: 10,
    minZoom: 1,
    maxZoom: 15,
    nodeHighlightBehavior: true,
    node: {
        color: "lightgreen",
        size: 300,
        highlightStrokeColor: "blue",
        strokeColor: "lightgreen",
        labelProperty: "name",
        labelPosition: "center",
        strokeWidth: 1
    },
    link: {
        highlightColor: "lightblue",
    },
};

const Map: React.FC<{ vertices: any[], edges: IEdge[] }> = (props) => <>
    <p>Settlements: {props.vertices.length}; Roads: {props.edges.length}</p>
    <Graph
        id="graph-id"
        data={{
            nodes: props.vertices,
            links: props.edges
        }}
        config={graphConfig}
    />
</>

export default Map;
