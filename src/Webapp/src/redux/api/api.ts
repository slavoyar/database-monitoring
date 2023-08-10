import { User } from '@models';
import { createApi } from '@reduxjs/toolkit/query/react';

import customFetchBase, { TokenModel } from './customFetchBase';

export interface AuthLoginModel {
  email: string;
  password: string;
}

export interface AuthResponse {
  status: 'Error' | 'Success'
  message: string
}

export function isAuthResponse(value: AuthResponse | User[]): value is AuthResponse {
  return (value as AuthResponse).status !== undefined;
}

export const api = createApi({
  reducerPath: 'api',
  baseQuery: customFetchBase,
  tagTypes: ['Users', 'UserInfo', 'Workspace'],
  endpoints: (builder) => ({
    login: builder.mutation<TokenModel, AuthLoginModel>({
      query: (data) => (
        {
          url: 'auth/login',
          method: 'POST',
          body: data,
        }),
    }),
    getUserInfo: builder.query<User, void>({
      query: () => 'users/info',
      providesTags: ['Users', 'UserInfo'],
    }),
    fetchUsers: builder.query<User[] | AuthResponse, void>({
      query: () => ({ url: 'users' }),
      providesTags: ['Users', 'UserInfo'],
    }),
    createUser: builder.mutation<AuthResponse, User>({
      query: (user) => (
        {
          url: 'users/create',
          method: 'POST',
          body: user,
        }),
      invalidatesTags: ['Users'],
    }),
    updateUser: builder.mutation<AuthResponse, Partial<User>>({
      query: (user) => (
        {
          url: 'users/update',
          method: 'PATCH',
          body: user,
        }
      ),
      invalidatesTags: ['Users', 'UserInfo'],
    }),
    deleteUser: builder.mutation<AuthResponse, string>({
      query: (email) => (
        {
          url: 'users',
          method: 'DELETE',
          body: JSON.stringify(email),
        }),
      invalidatesTags: ['Users'],
    }),
  }),
});

export const {
  useLoginMutation,
  useCreateUserMutation,
  useDeleteUserMutation,
  useUpdateUserMutation,
  useFetchUsersQuery,
  useGetUserInfoQuery,
} = api;

