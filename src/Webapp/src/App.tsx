import React, { FC } from 'react'
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom'
import LoginPage from 'pages/LoginPage'
import Dashboard from 'pages/Dashboard'

const App: FC = () => (
  <BrowserRouter>
    <Routes>
      <Route path='/' element={<Navigate to='/dashboard' />} />
      <Route path='dashboard' element={<Dashboard />} />
      <Route path='settings/'>
        <Route path='user' />
        <Route path='workspaces' />
        <Route path='servers' />
        <Route path='users' />
      </Route>
      <Route path='/login' element={<LoginPage />} />
    </Routes>
  </BrowserRouter>
)

export default App
