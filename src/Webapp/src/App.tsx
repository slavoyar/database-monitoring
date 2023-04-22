import React, { FC } from 'react'
import { BrowserRouter, Route, Routes } from 'react-router-dom'

const App: FC = () => (
  <BrowserRouter>
    <Routes>
      <Route path='/'>
        <Route path='settings/'>
          <Route path='workspaces' />
          <Route path='servers' />
          <Route path='users' />
        </Route>
        <Route path='auth' />
      </Route>
    </Routes>
  </BrowserRouter>
)

export default App
