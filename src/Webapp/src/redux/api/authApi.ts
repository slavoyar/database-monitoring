import { User } from '@models';
import { createApi } from '@reduxjs/toolkit/query/react'

import customFetchBase, { TokenModel } from './customFetchBase';

export interface AuthLoginModel {
  email: string;
  password: string;
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
  baseQuery: customFetchBase,
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
    updateUser: builder.mutation<AuthResponse, Partial<User>>({
      query: (user) => (
        {
          url: 'users/update',
          method: 'POST',
          body: user
        }
      )
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

export const { useLoginMutation, useCreateUserMutation, useDeleteUserMutation, useUpdateUserMutation, useFetchUsersQuery } = authApi;

