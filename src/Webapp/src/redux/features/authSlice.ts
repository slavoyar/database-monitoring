import { authApi } from '@redux/api/authApi'
import { TokenModel } from '@redux/api/customFetchBase'
import { createSlice, PayloadAction } from '@reduxjs/toolkit'

interface AuthState {
    accessToken?: string
    refreshToken?: string
    isLoggedIn: boolean
}

export const authSlice = createSlice({
    name: 'authSlice',
    initialState: {
        accessToken: localStorage.getItem('accessToken') ?? undefined,
        refreshToken: localStorage.getItem('refreshToken') ?? undefined,
        isLoggedIn: !!localStorage.getItem('accessToken')
    } as AuthState,
    reducers: {
        logout: (state) => {
            state.isLoggedIn = false
            state.accessToken = undefined
            state.refreshToken = undefined
            localStorage.removeItem('accessToken')
            localStorage.removeItem('refreshToken')
        },
        refreshTokens: (state, { payload }: PayloadAction<TokenModel>) => {
            state.isLoggedIn = true
            state.accessToken = payload.jwtAccessToken
            state.refreshToken = payload.jwtRefreshToken
        }
    },
    extraReducers: (builder) => {
        builder.addMatcher(
            authApi.endpoints.login.matchFulfilled,
            (state, { payload }: PayloadAction<TokenModel>) => {
                state.isLoggedIn = !!payload.jwtAccessToken
                state.accessToken = payload.jwtAccessToken
                state.refreshToken = payload.jwtRefreshToken
                localStorage.setItem('accessToken', payload.jwtAccessToken)
                localStorage.setItem('refreshToken', payload.jwtRefreshToken)
            }
        )
    },
})

export default authSlice.reducer
export const { logout, refreshTokens } = authSlice.actions