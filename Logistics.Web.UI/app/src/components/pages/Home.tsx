import React from 'react';
import { Link } from 'react-router-dom';
import API from '../../api/API';
import INode from '../../models/INode';
import IEdge from '../../models/IEdge';
import Map from '../Map';
import EdgeTable from '../EdgeTable';
import EdgeEditor from '../EdgeEditor';
import { toast } from 'react-toastify';
import { ResponseType } from '../../api/Result';

interface IState {
    editorOpen: boolean;
    nodes: { [key: number]: INode };
    edges: IEdge[];
    logisticCenterId: number;
    loading: boolean;
}

export default class HomePage extends React.Component<{}, IState>{
    constructor(props) {
        super(props);

        this.state = {
            editorOpen: false,
            nodes: {},
            edges: [],
            logisticCenterId: -1,
            loading: true
        };
    }

    componentDidMount = async () => {
        this.setState({ loading: true });

        const nodesResult = await API.getNodesAsync();
        if (nodesResult.type === ResponseType.ERROR) {
            toast.error(nodesResult.error);
            return;
        }

        const edgesResult = await API.getEdgesAsync();
        if (edgesResult.type === ResponseType.ERROR) {
            toast.error(edgesResult.error);
            return;
        }

        const centerResult = await API.getLogisticCenterIdAsync();
        if (centerResult.type === ResponseType.ERROR) {
            toast.error(centerResult.error);
            return;
        }

        const obj = nodesResult.value.reduce((obj, u) => (obj[u.id] = u, obj), {});
        this.setState({
            nodes: obj,
            edges: edgesResult.value,
            logisticCenterId: centerResult.value,
            loading: false
        });
    }

    private readonly edgeExists = (edge: IEdge): boolean => {
        const edges = this.state.edges.filter((e) =>
            (e.source === edge.source && e.target === edge.target) ||
            (e.source === edge.target && e.target === edge.source));

        return edges.length > 0;
    }

    private readonly handleRemoveEdgeClick = async (edge: IEdge) => {
        const result = await API.deleteEdgeAsync(edge.source, edge.target);
        if (result.type === ResponseType.ERROR) {
            toast.error(result.error);
            return;
        }

        const deleted = result.value;
        if (!deleted) {
            toast.warning("Could not delete road");
            return;
        }

        const edges = this.state.edges.filter((e) =>
            !(e.source === edge.source && e.target === edge.target) &&
            !(e.source === edge.target && e.target === edge.source))

        this.setState({
            edges: edges
        }, () => {
            toast.success("Road removed");
        });
    };

    private readonly handleOnAddEdge = async (edge: IEdge) => {
        const result = await API.createEdgeAsync(edge.source, edge.target);
        if (result.type === ResponseType.ERROR) {
            toast.error(result.error);
            return;
        }

        const created = result.value;
        if (!created) {
            toast.warning("Could not create road");
            return;
        }

        this.setState({
            edges: [...this.state.edges, edge],
            editorOpen: false
        }, () => {
            toast.success("Road added");
        });
    }

    private readonly handleFindLogisticCenter = async () => {
        const result = await API.getLogisticCenterIdAsync();
        if (result.type === ResponseType.ERROR) {
            toast.error(result.error);
            return;
        }

        const logisticCenterId = result.value;
        if (logisticCenterId === -1) {
            toast.warning("Could not locate logistic center location. There are unreachable settlements in this infrastructure",
                {
                    autoClose: 4000
                });
        }

        this.setState({
            logisticCenterId: logisticCenterId
        }, () => {
            this.state.logisticCenterId !== -1 && toast.success("New logistic center located");
        });
    }

    private readonly renderUi = (): JSX.Element => {
        if (Object.entries(this.state.nodes).length === 0)
            return this.renderEmptyUi();

        const vertices = Object.entries(this.state.nodes).map(([key, value]) => {
            const isLogisticCenter = this.state.logisticCenterId === value.id;
            return {
                id: value.id,
                name: isLogisticCenter ? `${value.name} Logistic Center` : `${value.name}`,
                symbolType: isLogisticCenter ? 'diamond' : 'circle',
                color: isLogisticCenter ? 'orange' : 'lightgreen',
            }
        }
        );

        return <>
            <div className="border overflow-hidden">
                <Map vertices={vertices} edges={this.state.edges} />
            </div>
            <button className="mt-1 float-right font-weight-bold shadow btn btn-warning" onClick={this.handleFindLogisticCenter}>Find Logistic Center!</button>
        </>;
    }

    private readonly renderEmptyUi = (): JSX.Element => {
        return <>
            <p>There are currently no settlements.</p>
            <Link to="/settlements" className="btn btn-success">Add some!</Link>
        </>;
    }

    render = () => (
        <>
            <EdgeEditor
                onAddEdge={this.handleOnAddEdge}
                edgeExists={this.edgeExists}
                nodes={this.state.nodes}
                isOpen={this.state.editorOpen}
                onClose={() => this.setState({ editorOpen: false })}
            />

            <div className="row">
                <div className="col-12">
                    <h3>Welcome to Logistics!</h3>
                </div>
            </div>
            <div className="row">
                <div className="col-12 col-sm-7">
                    {this.state.loading
                        ? <p>Loading...</p>
                        : this.renderUi()
                    }
                </div>
                <div className="col-12 col-sm-5">
                    {this.state.loading
                        ? <p>Loading...</p>
                        : <EdgeTable
                            edges={this.state.edges}
                            nodes={this.state.nodes}
                            handleRemoveEdgeClick={this.handleRemoveEdgeClick}
                            handleAddEdgeClick={() => this.setState({ editorOpen: true })}
                        />
                    }
                </div>
            </div>
        </>
    )
}