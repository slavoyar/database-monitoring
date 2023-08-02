import { ServerId, UserId, Workspace, WorkspaceId } from '@models';

import { api } from './api';

export type UserWithWorkspace = { userId: UserId, workspaceId: WorkspaceId };
export type ServerWithWorkspace = { serverId: ServerId, workspaceId: WorkspaceId };

export const workspaceApi = api.injectEndpoints({
    endpoints: (build) => ({
        getAllWorkspaces: build.query<Workspace[], void>({
            query: () => 'api/v1/workspace/list',
        }),
        getWorkSpaceById: build.query<Workspace, WorkspaceId>({
            query: (id) => `api/v1/workspace/${id}`,
        }),
        createWorkspace: build.mutation<WorkspaceId, Workspace>({
            query: (workspace) => ({
                url: 'api/v1/workspace',
                method: 'post',
                body: workspace,
            }),
        }),
        deleteWorkspace: build.mutation<void, WorkspaceId>({
            query: (id) => ({
                url: `api/v1/workspace/${id}`,
                method: 'delete',
            }),
        }),
        updateWorkspace: build.mutation<void, Workspace>({
            query: (workspace) => ({
                url: `api/v1/workspace/${workspace.id}`,
                method: 'put',
                body: workspace,
            }),
        }),
        getWorkspaceUsers: build.query<UserId[], WorkspaceId>({
            query: (id) => `api/v1/users/${id}`,
        }),
        addUserToWorkspace: build.mutation<void, UserWithWorkspace>({
            query: (params) => ({
                url: `url/v1/users/${params.userId}`,
                method: 'post',
                body: JSON.stringify(params.workspaceId),
            }),
        }),
        removeUserFromWorkspace: build.mutation<void, UserWithWorkspace>({
            query: (params) => ({
                url: `url/v1/users/${params.userId}`,
                method: 'delete',
                body: JSON.stringify(params.workspaceId),
            }),
        }),
        getWorkspaceServers: build.query<ServerId[], WorkspaceId>({
            query: (id) => `api/v1/servers/${id}`,
        }),
        addServerToWorkspace: build.mutation<void, ServerWithWorkspace>({
            query: (params) => ({
                url: `url/v1/servers/${params.serverId}`,
                method: 'post',
                body: JSON.stringify(params.workspaceId),
            }),
        }),
        removeServerFromWorkspace: build.mutation<void, ServerWithWorkspace>({
            query: (params) => ({
                url: `url/v1/servers/${params.serverId}`,
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
    useAddServerToWorkspaceMutation,
    useRemoveServerFromWorkspaceMutation,
    useAddUserToWorkspaceMutation,
    useRemoveUserFromWorkspaceMutation,
} = workspaceApi;