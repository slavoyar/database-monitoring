import React, { FC } from 'react'
import { useSelector } from 'react-redux'
import { Navigate, Outlet } from 'react-router-dom'
import { Path } from '@models'
import { RootState } from '@redux/store'

const PrivateRoute: FC = () => {
  const accessToken = useSelector<RootState>((state) => state.authState.accessToken)
  return accessToken ? <Outlet /> : <Navigate to={`/${Path.login}`} replace />
}

export default PrivateRoute
