import React, { FC } from 'react';
import { Navigate, Outlet } from 'react-router-dom'
import { Path } from '@models'
import { store } from '@redux/store'

const PrivateRoute: FC = () => {
    const { isLoggedIn } = store.getState().authState;
    return isLoggedIn ? <Outlet /> : <Navigate to={`/${Path.login}`} replace />
}

export default PrivateRoute