import React, { FC, ReactNode } from 'react'
import { Layout } from 'antd'

import { Navbar, Sidebar } from 'components/common/'

import 'css/AdminPage.css'

interface AdminPageProps {
  children: ReactNode
}

const AdminPage: FC<AdminPageProps> = ({ children }: AdminPageProps) => (
  <Layout>
    <Navbar />
    <Layout className='sidebar-layout'>
      <Sidebar />
      <Layout.Content className='admin-page-content'>{children}</Layout.Content>
    </Layout>
  </Layout>
)

export default AdminPage
