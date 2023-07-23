import React, { FC, useEffect, useState } from 'react'
import { useLocation, useNavigate } from 'react-router-dom'
import {
  DatabaseOutlined,
  DeploymentUnitOutlined,
  SettingOutlined,
  UserOutlined,
} from '@ant-design/icons'
import { Path, Role } from '@models'
import { Layout, Menu } from 'antd'
import { MenuItemType } from 'antd/es/menu/hooks/useItems'
import { MenuInfo } from 'rc-menu/lib/interface'

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
  const location = useLocation()
  const [selectedKeys, setSeletedKeys] = useState<string[]>([])

  const onMenuClick = ({ key }: MenuInfo): void => {
    navigate(`/${Path.admin}/${key}`)
    setSeletedKeys([key])
  }

  useEffect(() => {
    const path = location.pathname.split('/').pop()
    setSeletedKeys(Object.values(Path).filter((item) => path === item))
  }, [])

  return (
    <Sider width={300}>
      <Menu
        mode='inline'
        selectedKeys={selectedKeys}
        style={{ height: '100%', borderRight: 0 }}
        items={ADMIN_MENU_ITEMS}
        onClick={onMenuClick}
      />
    </Sider>
  )
}

export default Sidebar
