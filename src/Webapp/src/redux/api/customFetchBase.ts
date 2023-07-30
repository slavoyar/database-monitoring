import { redirect } from 'react-router-dom';
import { Path } from '@models';
import { RootState, store } from '@redux/store';
import {
    BaseQueryFn,
    FetchArgs,
    fetchBaseQuery,
    FetchBaseQueryError,
} from '@reduxjs/toolkit/query'

import { logout, refreshTokens } from '../features/authSlice'

export interface TokenModel {
    jwtAccessToken: string;
    jwtRefreshToken: string;
}

const baseQuery = fetchBaseQuery({
    baseUrl: '/api',
    prepareHeaders: (headers, { getState }) => {
        const token = (getState() as RootState).authState.accessToken
        if (token) {
            headers.set('authorization', `Bearer ${token}`)
        }
        headers.set('Content-Type', 'application/json')
        return headers
    },
});

const customFetchBase: BaseQueryFn<
    string | FetchArgs,
    unknown,
    FetchBaseQueryError
> = async (args, api, extraOptions) => {
    let result = await baseQuery(args, api, extraOptions)
    if (result.error?.status === 401) {
        const { authState } = store.getState()
        const refreshResult = await baseQuery(
            {
                url: 'auth/refresh',
                method: 'POST',
                body: {
                    accessToken: authState.accessToken,
                    refreshToken: authState.refreshToken,
                }
            }, api, extraOptions)
        if (refreshResult.error?.status) {
            store.dispatch(logout())
            redirect(`/${Path.login}`)
        } else {
            store.dispatch(refreshTokens(refreshResult as unknown as TokenModel))
            result = await baseQuery(args, api, extraOptions)
        }
    }
    return result;
};

export default customFetchBase;

