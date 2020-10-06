import axios from 'axios';
import { Result, createError, createResponse } from './Result';
import INode from "../models/INode";
import IEdge from "../models/IEdge";

export default class API {
    private static readonly BASE_URL: string = process.env.REACT_APP_API_URL;
    private static readonly ENDPOINT_NODES = `${API.BASE_URL}/nodes`;
    private static readonly ENDPOINT_GET_LOGISTIC_CENTER_ID = `${API.BASE_URL}/nodes/getlogisticcenterid`;
    private static readonly ENDPOINT_GET_NODE = `${API.BASE_URL}/nodes/getbyid/`;
    private static readonly ENDPOINT_CREATE_NODE = `${API.BASE_URL}/nodes/create`;
    private static readonly ENDPOINT_UPDATE_NODE = `${API.BASE_URL}/nodes/update`;
    private static readonly ENDPOINT_DELETE_NODE = `${API.BASE_URL}/nodes/delete`;
    private static readonly ENDPOINT_CREATE_EDGE = `${API.BASE_URL}/edges/create`;
    private static readonly ENDPOINT_DELETE_EDGE = `${API.BASE_URL}/edges/delete`;
    private static readonly ENDPOINT_EDGES = `${API.BASE_URL}/edges`;

    public static readonly getLogisticCenterIdAsync = async (): Promise<Result<number>> => {
        try {
            let result = await axios.get(API.ENDPOINT_GET_LOGISTIC_CENTER_ID)
            if (typeof result.data === undefined)
                return createError("GetLogistic");

            return createResponse<number>(result.data);
        } catch (error) {
            return createError(error.response.data);
        }
    }

    public static readonly getEdgesAsync = async (): Promise<Result<IEdge[]>> => {
        try {
            const response = await axios.get<{ from: number, to: number }[]>(API.ENDPOINT_EDGES);
            return createResponse<IEdge[]>(response.data.map((e) =>
                ({ source: e.from, target: e.to }))
            );
        } catch (error) {
            return createError(error.response.data);
        }
    }

    public static readonly getNodesAsync = async (): Promise<Result<INode[]>> => {
        try {
            const response = await axios.get<INode[]>(API.ENDPOINT_NODES);
            return createResponse(response.data);
        } catch (error) {
            return createError(error.response.data);
        }
    }

    public static readonly getNodeByIdAsync = async (id: number): Promise<Result<INode>> => {
        try {
            const response = await axios.get<INode>(API.ENDPOINT_GET_NODE + id);
            return createResponse(response.data);
        } catch (error) {
            return createError(error.response.data);
        }
    }

    public static readonly createNodeAsync = async (name: string): Promise<Result<INode>> => {
        try {
            const response = await axios.post<INode>(API.ENDPOINT_CREATE_NODE, { name: name });
            return createResponse(response.data);
        } catch (error) {
            return createError(error.response.data);
        }
    }

    public static readonly updateNodeAsync = async (id: number, name: string): Promise<Result<boolean>> => {
        try {
            const response = await axios.post<boolean>(API.ENDPOINT_UPDATE_NODE, { id: id, name: name });
            return createResponse(response.data);
        } catch (error) {
            return createError(error.response.data);
        }
    }

    public static readonly deleteNodeAsync = async (id: number): Promise<Result<boolean>> => {
        try {
            const response = await axios.post<boolean>(API.ENDPOINT_DELETE_NODE, { nodeId: id });
            return createResponse(response.data);
        } catch (error) {
            return createError(error.response.data);
        }
    }

    public static readonly deleteEdgeAsync = async (from: number, to: number): Promise<Result<boolean>> => {
        try {
            const response = await axios.post<boolean>(API.ENDPOINT_DELETE_EDGE, { from: from, to: to });
            return createResponse(response.data);
        } catch (error) {
            return createError(error.response.data);
        }
    }

    public static readonly createEdgeAsync = async (from: number, to: number): Promise<Result<boolean>> => {
        try {
            const response = await axios.post<boolean>(API.ENDPOINT_CREATE_EDGE, { from: from, to: to });
            return createResponse(response.data);
        } catch (error) {
            return createError(error.response.data);
        }
    }
}