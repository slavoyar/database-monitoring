import React, { FC } from 'react'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { DashboardOutlined, LineChartOutlined, SettingOutlined } from '@ant-design/icons'
import { Layout, Menu, Select } from 'antd'
import Path from 'src/models/Path'
import 'css/Navbar.css'

const { Header } = Layout

const Navbar: FC = () => {
  const navigate = useNavigate()
  const location = useLocation()
  const onLogoClick = () => {
    if (location.pathname.includes(Path.dashboard)) {
      navigate(`/${Path.dashboard}`)
    }
  }
  return (
    <Header className='header'>
      <button type='button' className='logo' onClick={onLogoClick}>
        МИАУ
      </button>
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
        <Select placeholder='Workspace' className='navbar-workspace-select' />
        <Link to={`/${Path.admin}/${Path.user}`}>
          <SettingOutlined />
        </Link>
      </div>
    </Header>
  )
}

export default Navbar
