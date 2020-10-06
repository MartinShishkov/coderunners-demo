import React from 'react';
import IEdge from '../models/IEdge';
import INode from '../models/INode';

interface IProps {
    nodes: { [key: number]: INode };
    edges: IEdge[];
    handleRemoveEdgeClick(edge: IEdge);
    handleAddEdgeClick();
}

const EdgeTable: React.FC<IProps> = (props) => {
    if (Object.keys(props.nodes).length === 0) {
        return <>
            <p>No roads yet...</p>
            <p>Add some settlements first.</p>
        </>
    }

    if (props.edges.length === 0) {
        return <>
            <p>No roads yet...</p>
            <button onClick={props.handleAddEdgeClick} className="btn btn-sm btn-success">Add some!</button>
        </>;
    }

    return <div style={{ height: 500, overflow: "auto" }}>
        <table className="table table-striped table-sm">
            <thead>
                <tr>
                    <th style={{ position: "sticky", top: 0, backgroundColor: "white" }}>From</th>
                    <th style={{ position: "sticky", top: 0, backgroundColor: "white" }}></th>
                    <th style={{ position: "sticky", top: 0, backgroundColor: "white" }}>To</th>
                    <th style={{ position: "sticky", top: 0, backgroundColor: "white" }} className="text-right">
                        <button onClick={props.handleAddEdgeClick} className="btn btn-sm btn-success">Add road</button>
                    </th>
                </tr>
            </thead>
            <tbody>
                {props.edges.map((edge, i) => {
                    const from = props.nodes[edge.source];
                    const to = props.nodes[edge.target];

                    return <tr key={i}>
                        <td className="vertical-align-middle">{from.name}</td>
                        <td>{'⮂'}</td>
                        <td>{to.name}</td>
                        <td className="text-right">
                            <button
                                onClick={() => props.handleRemoveEdgeClick(edge)}
                                className="btn btn-sm btn-danger">
                                delete
                            </button>
                        </td>
                    </tr>
                })}
            </tbody>
        </table>
    </div>
}

export default EdgeTable;