import React, { FC } from 'react';
import { useSelector } from 'react-redux';
import { Navigate, Outlet } from 'react-router-dom';
import { Path } from '@models';
import { useGetUserInfoQuery } from '@redux/api/api';
import { RootState } from '@redux/store';

const PrivateRoute: FC = () => {
  const accessToken = useSelector<RootState>((state) => state.authState.accessToken);
  const user = useSelector<RootState>(state => state.authState.user);
  // fetch user only if logged in 
  // eslint-disable-next-line no-undef, @typescript-eslint/no-unused-vars
  const userResult = useGetUserInfoQuery(undefined, { skip: !!user });
  return accessToken ? <Outlet /> : <Navigate to={`/${Path.login}`} replace />;
};

export default PrivateRoute;
