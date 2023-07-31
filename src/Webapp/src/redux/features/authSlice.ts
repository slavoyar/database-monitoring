import { User } from '@models';
import { authApi } from '@redux/api/authApi';
import { TokenModel } from '@redux/api/customFetchBase';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AuthState {
    accessToken?: string;
    refreshToken?: string;
    user?: Partial<User>;
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
        refreshTokens: (state, { payload }: PayloadAction<TokenModel>) => {
            state.accessToken = payload.jwtAccessToken;
            state.refreshToken = payload.jwtRefreshToken;
        },
    },
    extraReducers: (builder) => {
        builder.addMatcher(
            authApi.endpoints.login.matchFulfilled,
            (state, { payload }: PayloadAction<TokenModel>) => {
                state.accessToken = payload.jwtAccessToken;
                state.refreshToken = payload.jwtRefreshToken;
                state.user = payload.user;
                state.email = payload.user.email;
                localStorage.setItem('accessToken', payload.jwtAccessToken);
                localStorage.setItem('refreshToken', payload.jwtRefreshToken);
            },
        );
    },
});

export default authSlice.reducer;
export const { logout, refreshTokens } = authSlice.actions;