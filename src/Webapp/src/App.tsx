import React, { FC } from 'react'
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom'
import LoginPage from 'pages/LoginPage'
import Dashboard from 'pages/Dashboard'
import AdminPage from 'pages/admin/AdminPage'
import Path from './models/Path'

const App: FC = () => (
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Navigate to={Path.dashboard} />} />
        <Route path={Path.dashboard} element={<Dashboard />} />
        <Route path={Path.admin}>
          <Route path={Path.user} element={<AdminPage>user</AdminPage>} />
          <Route path={Path.workspaces} element={<AdminPage>workspaces</AdminPage>} />
          <Route path={Path.users} element={<AdminPage>users</AdminPage>} />
          <Route path={Path.servers} element={<AdminPage>servers</AdminPage>} />
        </Route>
        <Route path={Path.login} element={<LoginPage />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
)

export default App
