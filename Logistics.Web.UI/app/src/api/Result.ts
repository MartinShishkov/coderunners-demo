export enum ResponseType {
    SUCCESS,
    ERROR
}

type ResultSuccess<T> = { type: ResponseType.SUCCESS; value: T }

type ResultError = { type: ResponseType.ERROR; error: Error }

export type Result<T> = ResultSuccess<T> | ResultError

export function createError(error): ResultError {
    return { type: ResponseType.ERROR, error: error };
}

export function createResponse<T>(data: T): ResultSuccess<T> {
    return { type: ResponseType.SUCCESS, value: data };
}