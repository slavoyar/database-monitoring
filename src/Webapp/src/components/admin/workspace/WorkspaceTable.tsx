import React, { FC } from 'react'
import { Table } from 'antd'

enum WorkspaceTableColumn {
  NAME = 'name',
  USERS = 'users',
  SERVERS = 'servers',
}

const MOCK_DATA = [
  {
    key: '1',
    [WorkspaceTableColumn.NAME]: 'Workspace1',
    [WorkspaceTableColumn.USERS]: 'User1, User2',
    [WorkspaceTableColumn.SERVERS]: 'Server1, Server2',
  },
  {
    key: '2',
    [WorkspaceTableColumn.NAME]: 'Workspace2',
    [WorkspaceTableColumn.USERS]: 'User1, User2',
    [WorkspaceTableColumn.SERVERS]: 'Server1',
  },
  {
    key: '2',
    [WorkspaceTableColumn.NAME]: 'Workspace2',
    [WorkspaceTableColumn.USERS]: 'User1',
    [WorkspaceTableColumn.SERVERS]: 'Server1, Server2',
  },
]

const columns = [
  {
    title: 'Название',
    dataIndex: WorkspaceTableColumn.NAME,
    key: WorkspaceTableColumn.NAME,
  },
  {
    title: 'Пользователи',
    dataIndex: WorkspaceTableColumn.USERS,
    key: WorkspaceTableColumn.USERS,
  },
  {
    title: 'Сервера',
    dataIndex: WorkspaceTableColumn.SERVERS,
    key: WorkspaceTableColumn.SERVERS,
  },
]

const WorkspaceTable: FC = () => <Table dataSource={MOCK_DATA} columns={columns} />

export default WorkspaceTable
