import React, { FC } from 'react'
import { Link } from 'react-router-dom'
import { DashboardOutlined, LineChartOutlined, SettingOutlined } from '@ant-design/icons'
import { Layout, Menu, Select } from 'antd'
import 'css/Navbar.css'

const { Header } = Layout

const Navbar: FC = () => (
  <Header className='header'>
    <div className='logo' />
    <Menu
      theme='dark'
      className='navbar-menu'
      mode='horizontal'
      defaultSelectedKeys={['/dashboard']}
    >
      <Menu.Item key='/dashboard'>
        <Link to='/dashboard'>
          <DashboardOutlined />
          Обзорная панель
        </Link>
      </Menu.Item>
      <Menu.Item key='stats'>
        <Link to='/dashboard'>
          <LineChartOutlined />
          Статистика
        </Link>
      </Menu.Item>
    </Menu>
    <div className='navbar-settings'>
      <Select className='navbar-workspace-select' />
      <Link to='/admin'>
        <SettingOutlined />
      </Link>
    </div>
  </Header>
)

export default Navbar
