import { authApi } from '@redux/api/authApi'
import { createSlice } from '@reduxjs/toolkit'

interface AuthState {
    accessToken?: string
    refreshToken?: string
    isLogin: boolean
}

export const authSlice = createSlice({
    name: 'authSlice',
    initialState: {
        accessToken: localStorage.getItem('accessToken') ?? undefined,
        refreshToken: localStorage.getItem('refreshToken') ?? undefined,
        isLogin: !!localStorage.getItem('accessToken')
    } as AuthState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addMatcher(
            authApi.endpoints.login.matchFulfilled,
            (state, { payload }) => {
                state.isLogin = !!payload.jwtAccessToken
                state.accessToken = payload.jwtAccessToken
                state.refreshToken = payload.jwtRefreshToken
                localStorage.setItem('accessToken', payload.jwtAccessToken)
                localStorage.setItem('refreshToken', payload.jwtRefreshToken)
            }
        )
    },
})

export default authSlice.reducer