import React, { FC } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import {
  DashboardOutlined,
  LineChartOutlined,
  LogoutOutlined,
  SettingOutlined,
} from '@ant-design/icons';
import { Path } from '@models';
import { logout } from '@redux/features/authSlice';
import { store } from '@redux/store';
import { Layout, Menu, Select } from 'antd';

import '@css/Navbar.css';

const { Header } = Layout;

const Navbar: FC = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const onLogoClick = () => {
    if (!location.pathname.includes(Path.dashboard)) {
      navigate(`/${Path.dashboard}`);
    }
  };

  return (
    <Header className='header'>
      <button type='button' className='logo' onClick={onLogoClick}>
        МИАУ
      </button>
      <Menu
        theme='dark'
        className='navbar-menu'
        mode='horizontal'
        defaultSelectedKeys={[`/${Path.dashboard}`]}
      >
        <Menu.Item key='/dashboard'>
          <Link to={`/${Path.dashboard}`}>
            <DashboardOutlined />
            Обзорная панель
          </Link>
        </Menu.Item>
        <Menu.Item key='stats'>
          <Link to={`/${Path.dashboard}`}>
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
        <Link to={`/${Path.login}`} onClick={() => store.dispatch(logout())}>
          <LogoutOutlined />
        </Link>
      </div>
    </Header>
  );
};

export default Navbar;
