import { Server, ServerId, ServerShort } from '@models';

import { api } from './api';

// TODO: move to models, since table data can be used in different places
export interface TableDataRequest {
    page: number;
    itemPerPage: number;
}

export interface LogRequest extends TableDataRequest {
    serverId: ServerId;
}

export const agregationApi = api.injectEndpoints({
    endpoints: (build) => ({
        getServerByIds: build.query<Server, ServerId[]>({
            query: (ids) => ({ url: 'server', params: { guids: ids } }),
            providesTags: ['Servers'],
        }),
        getServerByIdsShort: build.query<ServerShort, ServerId[]>({
            query: (ids) => ({ url: 'server/short', params: { guids: ids } }),
            providesTags: ['Servers'],
        }),
        getServersByPage: build.query<Server[], TableDataRequest>({
            query: (params) => `server/${params.page}/${params.itemPerPage}`,
        }),
        createServer: build.mutation<Server, Server>({
            query: (server) => ({
                url: 'server',
                method: 'POST',
                body: server,
            }),
            invalidatesTags: ['Servers'],
        }),
        deleteServer: build.mutation<void, ServerId>({
            query: (id) => ({
                url: `server/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['Servers'],
        }),
        updateServer: build.mutation<void, Server>({
            query: (server) => ({
                url: 'server',
                method: 'PUT',
                body: server,
            }),
            invalidatesTags: ['Servers'],
        }),
        getLogByPage: build.query<void, LogRequest>({
            query: ({ serverId, page, itemPerPage }) => `log/${serverId}/${page}/${itemPerPage}`,
        }),
    }),
    overrideExisting: false,
});

export const {
    useGetServerByIdsQuery,
    useGetServerByIdsShortQuery,
    useGetServersByPageQuery,
    useCreateServerMutation,
    useDeleteServerMutation,
    useUpdateServerMutation,
    useGetLogByPageQuery,
} = agregationApi;