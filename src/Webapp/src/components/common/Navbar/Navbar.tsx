import React, { FC } from 'react'
import { Link } from 'react-router-dom'
import { DashboardOutlined, LineChartOutlined, SettingOutlined } from '@ant-design/icons'
import { Layout, Menu, MenuProps, Select } from 'antd'
import { MenuItemType } from 'antd/es/menu/hooks/useItems'
import 'css/Navbar.css'

const { Header, Content, Footer } = Layout

const Navbar: FC = () => {
  return (
    <Header className='header'>
      <div className='logo'></div>
      <Menu theme='dark' className='navbar-menu' mode='horizontal' defaultSelectedKeys={['/']}>
        <Menu.Item key={'/'}>
          <Link to={'/'}>
            <DashboardOutlined />
            Обзорная панель
          </Link>
        </Menu.Item>
        <Menu.Item key={'stats'}>
          <Link to={'/stats'}>
            <LineChartOutlined />
            Статистика
          </Link>
        </Menu.Item>
      </Menu>
      <div className='navbar-settings'>
        <Select className='navbar-workspace-select'></Select>
        <Link to={'/admin'}>
          <SettingOutlined />
        </Link>
      </div>
    </Header>
  )
}

export default Navbar
