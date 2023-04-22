import React, { FC } from 'react'
import { useNavigate } from 'react-router-dom'
import { Layout, Menu } from 'antd'
import { MenuItemType } from 'antd/es/menu/hooks/useItems'
import { MenuInfo } from 'rc-menu/lib/interface'
import {
  SettingOutlined,
  UserOutlined,
  DeploymentUnitOutlined,
  DatabaseOutlined,
} from '@ant-design/icons'

import Path from 'src/models/Path'

const { Sider } = Layout
const ADMIN_MENU_ITEMS: MenuItemType[] = [
  {
    label: 'Настройки профиля',
    icon: <SettingOutlined />,
    key: Path.user,
  },
  {
    label: 'Рабочие пространства',
    icon: <DeploymentUnitOutlined />,
    key: Path.workspaces,
  },
  {
    label: 'Сервера',
    icon: <DatabaseOutlined />,
    key: Path.servers,
  },
  {
    label: 'Пользователи',
    icon: <UserOutlined />,
    key: Path.users,
  },
]

const Sidebar: FC = () => {
  const navigate = useNavigate()
  const onMenuClick = ({ key }: MenuInfo): void => {
    navigate(`/${Path.admin}/${key}`)
  }

  return (
    <Sider width={300}>
      <Menu
        mode='inline'
        defaultSelectedKeys={[Path.user]}
        style={{ height: '100%', borderRight: 0 }}
        items={ADMIN_MENU_ITEMS}
        onClick={onMenuClick}
      />
    </Sider>
  )
}

export default Sidebar
