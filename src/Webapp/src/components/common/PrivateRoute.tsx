import React, { FC } from 'react';
import { Navigate } from 'react-router-dom'
import { Path } from '@models'
import { store } from '@redux/store'

interface PrivateRouteProps {
    element: React.ReactElement;
}


const PrivateRoute: FC<PrivateRouteProps> = ({ element }) => {
    const { isLoggedIn } = store.getState().authState;
    console.log('PRIVATE ROUTE. CHECK LOGGED IN', isLoggedIn)
    return isLoggedIn ? element : <Navigate to={`/${Path.login}`} />
}

export default PrivateRoute