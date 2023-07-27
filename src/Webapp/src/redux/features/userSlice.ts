import { User } from '@models'
import { isAuthResponse, userApi } from '@redux/api/userApi'
import { createSlice } from '@reduxjs/toolkit'

interface UsersState {
    users: User[]
    isLoading: boolean
    error: string
}

export const userSlice = createSlice({
    name: 'userSlice',
    initialState: {
        users: [],
        isLoading: false,
        error: ''
    } as UsersState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addMatcher(
                userApi.endpoints.fetch.matchFulfilled,
                (state, { payload }) => {
                    state.isLoading = false
                    if (isAuthResponse(payload)) {
                        state.error = payload.message
                    } else {
                        state.error = ''
                        state.users = payload
                    }
                }
            ).addMatcher(
                userApi.endpoints.fetch.matchPending,
                (state) => {
                    state.isLoading = true
                }
            ).addMatcher(
                userApi.endpoints.create.matchFulfilled,
                (state) => {
                    state.isLoading = false
                    console.error('TODO: return created user from backend');
                }
            ).addMatcher(
                userApi.endpoints.create.matchPending,
                (state) => {
                    state.isLoading = true
                }
            ).addMatcher(
                userApi.endpoints.delete.matchFulfilled,
                (state) => {
                    state.isLoading = false
                    console.error('TODO: return deleted users email from backend');
                }
            ).addMatcher(
                userApi.endpoints.delete.matchPending,
                (state) => {
                    state.isLoading = true
                }
            )
    },
})

export default userSlice.reducer