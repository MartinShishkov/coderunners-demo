import React from 'react';
import { RouteComponentProps } from 'react-router';
import API from '../../api/API';
import { ResponseType } from '../../api/Result';
import { toast } from 'react-toastify';

interface IState {
    id: number;
    name: string;
    loading: boolean;
}

export default class EditPage extends React.Component<RouteComponentProps, IState>{

    constructor(props) {
        super(props);

        this.state = {
            id: 0,
            name: "",
            loading: false
        };
    }

    componentDidMount = async () => {
        const { id } = this.props.match.params as any;

        this.setState({ loading: true });

        const result = await API.getNodeByIdAsync(id);
        if (result.type === ResponseType.ERROR) {
            toast.error(result.error);
            return;
        }

        const node = result.value;
        this.setState({
            id: node.id,
            name: node.name,
            loading: false
        });
    }

    private readonly handleFormSubmit = async (event) => {
        event.preventDefault();

        this.setState({ loading: true });
        const result = await API.updateNodeAsync(this.state.id, this.state.name);
        if (result.type === ResponseType.ERROR) {
            toast.error(result.error);
            return;
        }

        const updated = result.value;
        if (updated) {
            window.location.href = "/settlements";
        }
    }

    private readonly handleChange = ({ target }) => {
        this.setState({
            name: target.value
        });
    }

    render = () => (
        <form onSubmit={this.handleFormSubmit}>
            <div className="row justify-content-center">
                <div className="col-3 bg-light p-3 rounded-3">
                    {this.state.loading
                        ? <p>Loading...</p>
                        :
                        <>
                            <h3>Edit</h3>

                            <label htmlFor="name">Name:</label>
                            <input type="text" className="form-control" id="name" value={this.state.name} onChange={this.handleChange} />
                            <input type="submit" className="mt-3 btn btn-success w-100" value="Save" />
                        </>
                    }
                </div>
            </div>
        </form>
    )
}