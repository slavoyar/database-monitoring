import React, { FC } from 'react';
import { Route, Routes } from 'react-router-dom';
import { ServerView, UserSettings, UserView, WorkspaceView } from '@components/admin';
import PrivateRoute from '@components/common/PrivateRoute';
import { Path } from '@models';
import { AdminPage, Dashboard, LoginPage, NotFound } from '@pages';
import { useGetUserInfoQuery } from '@redux/api/api';

const App: FC = () => {
  const userResult = useGetUserInfoQuery();
  return (
    <Routes>
      <Route path={Path.login} element={<LoginPage />} />
      <Route path='/' element={<PrivateRoute />} >
        <Route path={Path.dashboard} element={<Dashboard />} />
        <Route path={Path.admin}>
          <Route
            path={Path.user}
            element={
              <AdminPage>
                <UserSettings />
              </AdminPage>
            }
          />
          <Route
            path={Path.workspaces}
            element={
              <AdminPage>
                <WorkspaceView />
              </AdminPage>
            }
          />
          <Route
            path={Path.users}
            element={
              <AdminPage>
                <UserView />
              </AdminPage>
            }
          />
          <Route
            path={Path.servers}
            element={
              <AdminPage>
                <ServerView />
              </AdminPage>
            }
          />
        </Route>
        <Route path='*' element={<NotFound />} />
      </Route>
    </Routes >
  )
};


export default App;
