import React, { FC } from 'react'
import { Navigate, Route, Routes } from 'react-router-dom'
import { ServerView, UserSettings, UserView, WorkspaceView } from '@components/admin'
import PrivateRoute from '@components/common/PrivateRoute'
import { Path } from '@models'
import { AdminPage, Dashboard, LoginPage } from '@pages'
import { store } from '@redux/store'

const App: FC = () => {
  const { isLoggedIn } = store.getState().authState;
  return (
    <Routes>
      <Route path={Path.login} element={<LoginPage />} />
      <Route path='/' element={isLoggedIn ? <Navigate to={Path.dashboard} /> : <Navigate to={Path.login} />} />
      <Route path={Path.dashboard} element={<PrivateRoute element={<Dashboard />} />} />
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
    </Routes >
  )
}

export default App
