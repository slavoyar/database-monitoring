import { fetchBaseQuery } from '@reduxjs/toolkit/query';
import { createApi } from '@reduxjs/toolkit/query/react';

export interface AuthLoginModel {
  email: string;
  password: string;
}

interface TokenModel {
  JwtAccessToken: string;
  JwtRefreshToken: string;
}

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'http://localhost:5000/',
    jsonContentType: 'application/json; charset=utf-8'
  }),
  endpoints: (builder) => ({
    loginUser: builder.mutation<TokenModel, AuthLoginModel>({
      query(data) {
        return {
          url: 'api/auth/login',
          method: 'POST',
          body: data,
        };
      },
    })
  }),
});

export const { useLoginUserMutation } = authApi;

