import { User, WorkspaceId } from '@models';
import { api } from '@redux/api/api';
import { TokenModel } from '@redux/api/customFetchBase';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AuthState {
    accessToken?: string;
    refreshToken?: string;
    user?: Partial<User>;
    workspaceId?: WorkspaceId;
}

export const authSlice = createSlice({
    name: 'authSlice',
    initialState: {
        accessToken: localStorage.getItem('accessToken') ?? undefined,
        refreshToken: localStorage.getItem('refreshToken') ?? undefined,
    } as AuthState,
    reducers: {
        logout: (state) => {
            state.accessToken = undefined;
            state.refreshToken = undefined;
            localStorage.removeItem('accessToken');
            localStorage.removeItem('refreshToken');
        },
        // TODO: refactor use this reducer in extrareducers
        refreshTokens: (state, { payload }: PayloadAction<TokenModel>) => {
            state.accessToken = payload.jwtAccessToken;
            state.refreshToken = payload.jwtRefreshToken;
            localStorage.setItem('accessToken', payload.jwtAccessToken);
            localStorage.setItem('refreshToken', payload.jwtRefreshToken);
        },
        updateUser: (state, { payload }: PayloadAction<User>) => {
            state.user = payload;
        },
        updateWorkspaceId: (state, { payload }: PayloadAction<WorkspaceId>) => {
            state.workspaceId = payload;
        },
    },
    extraReducers: (builder) => {
        builder.addMatcher(
            api.endpoints.login.matchFulfilled,
            (state, { payload }: PayloadAction<TokenModel>) => {
                state.accessToken = payload.jwtAccessToken;
                state.refreshToken = payload.jwtRefreshToken;
                localStorage.setItem('accessToken', payload.jwtAccessToken);
                localStorage.setItem('refreshToken', payload.jwtRefreshToken);

                state.user = payload.user;
            },
        );
        builder.addMatcher(
            api.endpoints.getUserInfo.matchFulfilled,
            (state, { payload }: PayloadAction<User>) => {
                state.user = payload;
            },
        );
    },
});

export default authSlice.reducer;
export const { logout, refreshTokens, updateUser, updateWorkspaceId } = authSlice.actions;