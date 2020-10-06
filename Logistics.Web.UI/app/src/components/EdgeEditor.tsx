import React, { useState } from 'react';
import Select from '../components/Select';
import INode from '../models/INode';
import Modal from 'react-modal';
import IEdge from '../models/IEdge';
Modal.setAppElement('#root')

interface IProps {
    nodes: { [key: number]: INode };
    edgeExists(edge: IEdge): boolean;
    isOpen: boolean;
    onClose();
    onAddEdge(edge: IEdge);
}

const EdgeEditor: React.FC<IProps> = ({ isOpen, nodes, onClose, edgeExists, onAddEdge }) => {
    const [fromId, setFrom] = useState("");
    const [toId, setTo] = useState("");

    const submitHandler = (e) => {
        e.preventDefault();
        onAddEdge({ source: Number(fromId), target: Number(toId) });
        setFrom("");
        setTo("");
    }

    const fromOptions = Object.entries(nodes).map(([key, value]) =>
        ({ value: key, text: `${value.name} (${value.id})` }));

    const toOptions = fromId !== ""
        ? Object.entries(nodes)
            .filter(([key, value]) =>
                value.id !== Number(fromId) &&
                !edgeExists({ source: Number(fromId), target: value.id }))
            .map(([key, value]) =>
                ({ value: key, text: `${value.name} (${value.id})` }))
        : [];

    return (
        <Modal
            shouldCloseOnOverlayClick={true}
            onRequestClose={onClose}
            style={{ content: { width: 400, margin: "0 auto" } }} isOpen={isOpen}>

            <h3>Add road</h3>
            <form onSubmit={submitHandler}>
                <Select
                    id={"from"}
                    label={"From:"}
                    selectedValue={fromId}
                    options={fromOptions}
                    onChange={(value) => { setFrom(value); setTo("") }}
                    className="form-control"
                />

                {fromId !== "" &&
                    <Select
                        id={"to"}
                        label={"To:"}
                        selectedValue={toId}
                        options={toOptions}
                        onChange={(value) => setTo(value)}
                        className="form-control"
                    />
                }

                <input type="submit" value="Connect" className="w-100 mt-3 btn btn-success" disabled={fromId === "" || toId === ""} />

            </form>
        </Modal>
    )
}

export default EdgeEditor;