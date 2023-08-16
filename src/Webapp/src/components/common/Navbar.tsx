import React, { FC, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import {
  DashboardOutlined,
  LineChartOutlined,
  LogoutOutlined,
  MailOutlined,
  SettingOutlined,
} from '@ant-design/icons';
import { Path, UserId, WorkspaceId } from '@models';
import { ValueWithLabel } from '@models/Types';
import { useGetUserWorkspacesQuery } from '@redux/api/workspaceApi';
import { logout, updateWorkspaceId } from '@redux/features/authSlice';
import { RootState, store } from '@redux/store';
import { Layout, Menu, Select } from 'antd';

import '@css/Navbar.css';

const { Header } = Layout;

const Navbar: FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const [workspace, setWorkspace] = useState<WorkspaceId>('');
  const [workspaceOptions, setWorkspaceOptions] = useState<ValueWithLabel[]>([]);

  const userId = useSelector<RootState>(state => state.authState.user?.id) as UserId;

  const { data: workspaces } = useGetUserWorkspacesQuery(userId, { skip: !userId });

  const setCurrentWorkspace = (workspaceId: WorkspaceId) => {
    setWorkspace(workspaceId);
    store.dispatch(updateWorkspaceId(workspaceId));
  };

  useEffect(() => {
    if (!workspaces) {
      setCurrentWorkspace('');
      setWorkspaceOptions([]);
      return;
    }
    if (!workspace && workspaces.length) {
      setCurrentWorkspace(workspaces[0].id as WorkspaceId);
    }
    const options = workspaces.map(w => ({ value: w.id, label: w.name } as ValueWithLabel));
    setWorkspaceOptions(options);
  }, [workspaces]);

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
        <Select
          placeholder='Workspace'
          className='navbar-workspace-select'
          value={workspace}
          options={workspaceOptions}
          onChange={setCurrentWorkspace}
        />
        <Link to={`/${Path.notifications}`}>
          <MailOutlined />
        </Link>
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
