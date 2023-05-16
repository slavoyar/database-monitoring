import { User, UserId } from '@models'
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const UserService = createApi({
    baseQuery: fetchBaseQuery({ baseUrl: '/' }),
    endpoints: (builder) => ({
        getUser: builder.query<User, UserId>({
            query: (userId: UserId) => `user/${userId}`,
        }),
        addUser: builder.mutation<User, Partial<User>>({
            query: (body) => ({
                url: 'user',
                method: 'POST',
                body,
            })
        }),
        updateUser: builder.mutation<void, Partial<User>>({
            query: (user) => ({
                url: `user/${user.id}`,
                method: 'PUT',
                body: user,
            })
        }),
        deleteUser: builder.mutation<Boolean, UserId>({
            query: (id: UserId) => ({
                url: `user/${id}`,
                method: 'DELETE',
            })
        })
    })
})

export const {
    useGetUserQuery,
    useAddUserMutation,
    useUpdateUserMutation,
    useDeleteUserMutation,
} = UserService;