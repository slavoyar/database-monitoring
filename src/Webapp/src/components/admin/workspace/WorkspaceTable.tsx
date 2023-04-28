import React, { FC, useState } from 'react'
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons'
import { Button, Table } from 'antd'

enum WorkspaceTableColumn {
  NAME = 'name',
  USERS = 'users',
  SERVERS = 'servers',
}

interface WorkspaceTableData {
  key: string
  [WorkspaceTableColumn.NAME]: string
  [WorkspaceTableColumn.USERS]: string
  [WorkspaceTableColumn.SERVERS]: string
}

const MOCK_DATA: WorkspaceTableData[] = [
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
    key: '3',
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

const WorkspaceTable: FC = () => {
  const [data, setData] = useState<WorkspaceTableData[]>(MOCK_DATA)
  const [deleteDisabled, setDeleteDisabled] = useState(true)
  const [selectedRows, setSelectedRows] = useState<React.Key[]>([])

  const onAddClick = () => {
    setData((prevData) => {
      const newKey = String(Number(prevData[prevData.length - 1].key) + 1)
      return [
        ...prevData,
        {
          key: newKey,
          [WorkspaceTableColumn.NAME]: 'Workspace2',
          [WorkspaceTableColumn.USERS]: 'User1',
          [WorkspaceTableColumn.SERVERS]: 'Server1, Server2',
        },
      ]
    })
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
    </>
  )
}

export default WorkspaceTable
