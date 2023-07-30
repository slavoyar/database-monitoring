import React, { FC } from 'react'
import { useSelector } from 'react-redux'
import { Navigate, Outlet } from 'react-router-dom'
import { Path } from '@models'
import { RootState } from '@redux/store'

const PrivateRoute: FC = () => {
  const isLoggedIn = useSelector<RootState>((state) => state.authState.isLoggedIn)
  return isLoggedIn ? <Outlet /> : <Navigate to={`/${Path.login}`} replace />
}

export default PrivateRoute
