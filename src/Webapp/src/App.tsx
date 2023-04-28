import React, { FC } from 'react'
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom'
import { UserSettings, WorkspaceTable } from '@components/admin'
import { Path } from '@models'
import { AdminPage, Dashboard, LoginPage } from '@pages'

const App: FC = () => (
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Navigate to={Path.dashboard} />} />
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
                <WorkspaceTable />
              </AdminPage>
            }
          />
          <Route path={Path.users} element={<AdminPage>users</AdminPage>} />
          <Route path={Path.servers} element={<AdminPage>servers</AdminPage>} />
        </Route>
        <Route path={Path.login} element={<LoginPage />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
)

export default App
