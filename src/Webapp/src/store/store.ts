import { configureStore } from '@reduxjs/toolkit'

import { UserService } from '../services/users'

export const store = configureStore({
    reducer: {
        [UserService.reducerPath]: UserService.reducer,
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(UserService.middleware),
})