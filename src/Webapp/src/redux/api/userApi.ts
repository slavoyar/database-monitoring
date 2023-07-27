import { User } from '@models';
import { RootState } from '@redux/store';
import { fetchBaseQuery } from '@reduxjs/toolkit/query'
import { createApi } from '@reduxjs/toolkit/query/react'

export interface AuthResponse {
    status: 'Error' | 'Success'
    message: string
}

export function isAuthResponse(value: AuthResponse | User[]): value is AuthResponse {
    return (value as AuthResponse).status !== undefined
}

export const userApi = createApi({
    reducerPath: 'userApi',
    baseQuery: fetchBaseQuery({
        baseUrl: '/api/users',
        prepareHeaders: (headers, { getState }) => {
            const token = (getState() as RootState).authState.accessToken
            if (token) {
                headers.set('authorization', `Bearer ${token}`)
            }
            return headers
        },
    }),
    endpoints: (builder) => ({
        fetch: builder.query<User[] | AuthResponse, void>({
            query: () => ({ url: 'get' })
        }),
        create: builder.mutation<AuthResponse, User>({
            query(user: User) {
                return {
                    url: 'create',
                    method: 'POST',
                    data: user
                }
            }
        }),
        delete: builder.mutation<AuthResponse, string>({
            query(email: string) {
                return {
                    url: 'delete',
                    method: 'POST',
                    data: email
                }
            }
        })
    }),
});

export const { useCreateMutation, useDeleteMutation, useFetchQuery } = userApi;

