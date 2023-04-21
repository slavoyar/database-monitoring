import React, { FC } from 'react'
import { BrowserRouter } from 'react-router-dom'
import { Layout } from 'antd'
import Navbar from 'components/common/Navbar/Navbar'

const { Content } = Layout

const App: FC = () => (
  <BrowserRouter>
    <Layout>
      <Navbar />
      <Layout>
        <Layout style={{ padding: '0 24px 24px' }}>
          <Content
            style={{
              padding: 24,
              margin: 0,
              minHeight: 280,
            }}
          >
            Content
          </Content>
        </Layout>
      </Layout>
    </Layout>
  </BrowserRouter>
)

export default App
