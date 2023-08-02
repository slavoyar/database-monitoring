import { api } from "./api";


const workspaceApi = api.injectEndpoints({
    endpoints: (build) => ({
        getWorkspace: build.query({
            query: (id) => `api/v1/workspace/${id}`,
        }),
    }),
    overrideExisting: false,
});