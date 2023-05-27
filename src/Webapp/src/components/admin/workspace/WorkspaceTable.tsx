import React, { FC, useEffect, useState } from 'react'
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons'
import { Server, User } from '@models'
import { MOCK_WORKSPACES } from '@models/Workspace'
import { Button, Table } from 'antd'

import EditWorkspaceDialog from './EditWorkspaceDialog'

enum WorkspaceTableColumn {
  NAME = 'name',
  USERS = 'users',
  SERVERS = 'servers',
}

export interface WorkspaceTableData {
  key: string
  [WorkspaceTableColumn.NAME]: string
  [WorkspaceTableColumn.USERS]: string
  [WorkspaceTableColumn.SERVERS]: string
}

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

type EntityWithName = User | Server

function objectArrayToString(arr: EntityWithName[]): string {
  return arr.map((item) => item.name).join(', ')
}

const WorkspaceTable: FC = () => {
  const [data, setData] = useState<WorkspaceTableData[]>([])
  const [deleteDisabled, setDeleteDisabled] = useState(true)
  const [selectedRows, setSelectedRows] = useState<React.Key[]>([])
  const [isModalOpen, setIsModalOpen] = useState(false)

  useEffect(() => {
    const tableData = MOCK_WORKSPACES.map((workspace) => ({
      key: workspace.id,
      name: workspace.name,
      users: objectArrayToString(workspace.users),
      servers: objectArrayToString(workspace.servers),
    }))
    setData(tableData)
  }, [])

  const onAddClick = () => {
    setIsModalOpen(true)
  }

  const onDeleteClick = () => {
    const newData = data.filter((item) => !selectedRows.includes(item.key))
    setSelectedRows([])
    setData(newData)
    setDeleteDisabled(true)
  }

  const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
    setDeleteDisabled(!newSelectedRowKeys.length)
    setSelectedRows(newSelectedRowKeys)
  }

  const onSaveHandler = (workspace: Omit<WorkspaceTableData, 'key'>): void => {
    setIsModalOpen(false)
    const newWorkspace = {
      ...workspace,
      key: String(data.length + 1),
    }
    setData([...data, newWorkspace])
  }

  const rowSelection = {
    selectedRows,
    onChange: onSelectChange,
  }

  return (
    <>
      <Button.Group>
        <Button icon={<PlusOutlined />} onClick={onAddClick} />
        <Button
          icon={<DeleteOutlined />}
          disabled={deleteDisabled}
          type='primary'
          danger
          onClick={onDeleteClick}
        />
      </Button.Group>
      <Table bordered dataSource={data} columns={columns} rowSelection={rowSelection} />
      <EditWorkspaceDialog
        workspace={undefined}
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        onSave={onSaveHandler}
      />
    </>
  )
}

export default WorkspaceTable
