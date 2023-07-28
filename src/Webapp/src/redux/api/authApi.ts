import { User } from '@models';
import { RootState } from '@redux/store';
import { fetchBaseQuery } from '@reduxjs/toolkit/query'
import { createApi } from '@reduxjs/toolkit/query/react'

export interface AuthLoginModel {
  email: string;
  password: string;
}

export interface TokenModel {
  jwtAccessToken: string;
  jwtRefreshToken: string;
}

export interface AuthResponse {
  status: 'Error' | 'Success'
  message: string
}

export function isAuthResponse(value: AuthResponse | { '$values': User[] }): value is AuthResponse {
  return (value as AuthResponse).status !== undefined
}

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: fetchBaseQuery({
    baseUrl: '/api',
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).authState.accessToken
      if (token) {
        headers.set('authorization', `Bearer ${token}`)
      }
      headers.set('Content-Type', 'application/json')
      return headers
    },
  }),
  tagTypes: ['Users'],
  endpoints: (builder) => ({
    login: builder.mutation<TokenModel, AuthLoginModel>({
      query: (data) => (
        {
          url: 'auth/login',
          method: 'POST',
          body: data,
        })
    }),
    fetchUsers: builder.query<{ '$values': User[] } | AuthResponse, void>({
      query: () => ({ url: 'users/get' }),
      providesTags: ['Users']
    }),
    createUser: builder.mutation<AuthResponse, User>({
      query: (user) => (
        {
          url: 'users/create',
          method: 'POST',
          body: user
        }),
      invalidatesTags: ['Users']
    }),
    deleteUser: builder.mutation<AuthResponse, string>({
      query: (email) => (
        {
          url: 'users/delete',
          method: 'POST',
          body: JSON.stringify(email),
        }),
      invalidatesTags: ['Users']
    })
  })
});

export const { useLoginMutation, useCreateUserMutation, useDeleteUserMutation, useFetchUsersQuery } = authApi;

