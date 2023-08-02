import { ServerId, UserId, Workspace, WorkspaceId } from '@models';

import { api } from './api';

export type UserWithWorkspace = { userId: UserId, workspaceId: WorkspaceId };
export type ServerWithWorkspace = { serverId: ServerId, workspaceId: WorkspaceId };

export const workspaceApi = api.injectEndpoints({
    endpoints: (build) => ({
        getAllWorkspaces: build.query<Workspace[], void>({
            query: () => 'v1/workspace/list',
            providesTags: ['Workspace'],
        }),
        getWorkSpaceById: build.query<Workspace, WorkspaceId>({
            query: (id) => `v1/workspace/${id}`,
            providesTags: ['Workspace'],
        }),
        createWorkspace: build.mutation<WorkspaceId, Workspace>({
            query: (workspace) => ({
                url: 'v1/workspace',
                method: 'post',
                body: workspace,
            }),
            invalidatesTags: ['Workspace'],
        }),
        deleteWorkspace: build.mutation<void, WorkspaceId>({
            query: (id) => ({
                url: `v1/workspace/${id}`,
                method: 'delete',
            }),
            invalidatesTags: ['Workspace'],
        }),
        updateWorkspace: build.mutation<void, Workspace>({
            query: (workspace) => ({
                url: `v1/workspace/${workspace.id}`,
                method: 'put',
                body: workspace,
            }),
            invalidatesTags: ['Workspace'],
        }),
        getWorkspaceUsers: build.query<UserId[], WorkspaceId>({
            query: (id) => `v1/users/${id}`,
        }),
        addUserToWorkspace: build.mutation<void, UserWithWorkspace>({
            query: (params) => ({
                url: `v1/users/${params.userId}`,
                method: 'post',
                body: JSON.stringify(params.workspaceId),
            }),
        }),
        removeUserFromWorkspace: build.mutation<void, UserWithWorkspace>({
            query: (params) => ({
                url: `v1/users/${params.userId}`,
                method: 'delete',
                body: JSON.stringify(params.workspaceId),
            }),
        }),
        getWorkspaceServers: build.query<ServerId[], WorkspaceId>({
            query: (id) => `v1/servers/${id}`,
        }),
        addServerToWorkspace: build.mutation<void, ServerWithWorkspace>({
            query: (params) => ({
                url: `v1/servers/${params.serverId}`,
                method: 'post',
                body: JSON.stringify(params.workspaceId),
            }),
        }),
        removeServerFromWorkspace: build.mutation<void, ServerWithWorkspace>({
            query: (params) => ({
                url: `v1/servers/${params.serverId}`,
                method: 'delete',
                body: JSON.stringify(params.workspaceId),
            }),
        }),
    }),
    overrideExisting: false,
});

export const {
    useGetAllWorkspacesQuery,
    useGetWorkSpaceByIdQuery,
    useGetWorkspaceServersQuery,
    useGetWorkspaceUsersQuery,
    useCreateWorkspaceMutation,
    useUpdateWorkspaceMutation,
    useDeleteWorkspaceMutation,
    useAddServerToWorkspaceMutation,
    useRemoveServerFromWorkspaceMutation,
    useAddUserToWorkspaceMutation,
    useRemoveUserFromWorkspaceMutation,
} = workspaceApi;