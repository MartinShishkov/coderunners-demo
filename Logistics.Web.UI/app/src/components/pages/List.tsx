import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import Select from '../Select';
import API from '../../api/API';
import INode from '../../models/INode';
import { ResponseType } from '../../api/Result';
import { toast } from 'react-toastify';

interface IState {
    nodes: INode[];
    loading: boolean;
}

export default class ListPage extends React.Component<{}, IState>{

    constructor(props) {
        super(props);

        this.state = {
            nodes: [],
            loading: false
        };
    }

    componentDidMount = async () => {
        this.setState({ loading: true });
        const result = await API.getNodesAsync();

        if (result.type === ResponseType.ERROR) {
            toast.error(result.error);
            return;
        }

        this.setState({
            loading: false,
            nodes: result.value
        });
    }

    private readonly handleDeleteClick = async (settlement: INode) => {
        const shouldDelete = window.confirm(`Deleting ${settlement.name} will also delete associated edges. Confirm?`);
        if (!shouldDelete) {
            return;
        }

        const result = await API.deleteNodeAsync(settlement.id);
        if (result.type === ResponseType.ERROR) {
            toast.error(result.error);
            return;
        }

        const deleted = result.value;
        if (!deleted)
            return;

        this.setState({
            nodes: this.state.nodes.filter((s) => s.id !== settlement.id)
        }, () => {
            toast.success("Settlement deleted");
        });
    }

    private readonly handleCreateSettlement = async (name: string, toId: string) => {
        const settlementCreateResult = await API.createNodeAsync(name);

        if (settlementCreateResult.type === ResponseType.ERROR) {
            toast.error(settlementCreateResult.error);
            return;
        }

        const settlement = settlementCreateResult.value;
        if (!settlement)
            return;

        if (toId) {
            const edgeCreateResult = await API.createEdgeAsync(settlement.id, Number(toId));
            if (edgeCreateResult.type === ResponseType.ERROR) {
                toast.error(edgeCreateResult.error);
                return;
            }
        }

        this.setState({
            nodes: [...this.state.nodes, settlement]
        }, () => {
            toast.success("Settlement created");
        });
    }

    render = () => (
        <>
            <div className="row">
                <div className="col-6 offset-3">
                </div>
            </div>
            <div className="row justify-content-center">
                <div className="col-4">
                    {this.state.loading
                        ? <p>Loading...</p>
                        : <NodeForm
                            nodes={this.state.nodes}
                            handleCreateNode={this.handleCreateSettlement}
                        />
                    }
                </div>
                {this.state.loading
                    ? <p>Loading...</p>
                    : this.state.nodes.length > 0 &&
                    <div className="col-4">
                        <h4>Settlements list</h4>
                        <SettlementsTable
                            nodes={this.state.nodes}
                            onDeleteClick={this.handleDeleteClick}
                        />
                    </div>
                }
            </div>
        </>
    )
}

const SettlementsTable: React.FC<{ nodes: INode[], onDeleteClick(node: INode) }> = (props) =>
    <table className="table table-striped table-sm table-hover">
        <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th className="text-right">Actions</th>
            </tr>
        </thead>
        <tbody>
            {props.nodes.map((s) =>
                <tr key={s.id}>
                    <td>{s.id}</td>
                    <td>{s.name}</td>
                    <td className="text-right">
                        <div className="btn-group">
                            <Link to={"/edit/" + s.id} className="btn btn-info btn-sm">edit</Link>
                            <button onClick={() => props.onDeleteClick(s)} className="btn btn-danger btn-sm">delete</button>
                        </div>
                    </td>
                </tr>
            )}
        </tbody>
    </table>


interface IFormProps {
    nodes: INode[];
    handleCreateNode(name: string, toId: string);
}

const NodeForm: React.FC<IFormProps> = ({ nodes: nodes, handleCreateNode }) => {
    const [name, setName] = useState("");
    const [connectedNodeId, setConnection] = useState("");

    const options = nodes.map((s) => ({ value: s.id, text: `${s.name} (${s.id})` }));

    const submitHandler = (e) => {
        e.preventDefault();
        handleCreateNode(name, connectedNodeId);

        setName("");
        setConnection("");
    }

    return <form onSubmit={submitHandler} className="bg-light p-3 rounded-lg">
        <label htmlFor="name">Create settlement:</label>
        <input type="text" value={name} onChange={({ target }) => setName(target.value)} className="form-control mb-3" id="name" name="name" />

        <Select
            id={"connection"}
            label={"Connect to:"}
            selectedValue={connectedNodeId}
            options={options}
            onChange={(value) => setConnection(value)}
            className="form-control"
        />

        <input disabled={name === ""} type="submit" value="Create" className="btn btn-success w-100 mt-3" />
    </form>
}