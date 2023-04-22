import React, { FC } from 'react'
import { Layout } from 'antd'
import Navbar from 'components/common/Navbar'
import Sidebar from 'components/common/Sidebar'
import 'css/AdminPage.css'

interface AdminPageProps {
  children: string
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
