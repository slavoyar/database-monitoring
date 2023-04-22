import React, { FC } from 'react'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LoginPage from 'pages/LoginPage'

const App: FC = () => (
  <BrowserRouter>
    <Routes>
      <Route path='/'>
        <Route path='settings/'>
          <Route path='workspaces' />
          <Route path='servers' />
          <Route path='users' />
        </Route>
        <Route path='login' element={<LoginPage />} />
      </Route>
    </Routes>
  </BrowserRouter>
)

export default App
