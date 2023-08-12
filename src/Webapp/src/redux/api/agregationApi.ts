import { Server, ServerId, ServerShort } from '@models';

import { api } from './api';

export interface LogRequest {
    serverId: ServerId;
    page: number;
    itemPerPage: number;
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
    useCreateServerMutation,
    useDeleteServerMutation,
    useUpdateServerMutation,
    useGetLogByPageQuery,
} = agregationApi;