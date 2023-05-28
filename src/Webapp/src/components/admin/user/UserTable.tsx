import React, { FC, useEffect, useState } from 'react'
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons'
import { MOCK_USERS } from '@models/User'
import { Button, Table } from 'antd'

enum UserTableColumn {
  NAME = 'name',
  EMAIL = 'email',
  PHONE = 'phone',
}

export interface UserTableData {
  key: string
  [UserTableColumn.NAME]: string
  [UserTableColumn.EMAIL]: string
  [UserTableColumn.PHONE]: string
}

const columns = [
  {
    title: 'Имя',
    dataIndex: UserTableColumn.NAME,
    key: UserTableColumn.NAME,
  },
  {
    title: 'Email',
    dataIndex: UserTableColumn.EMAIL,
    key: UserTableColumn.EMAIL,
  },
  {
    title: 'Телефон',
    dataIndex: UserTableColumn.PHONE,
    key: UserTableColumn.PHONE,
  },
]

const UserTable: FC = () => {
  const [tableData, setTableData] = useState<UserTableData[]>([])
  const [deleteDisabled, setDeleteDisabled] = useState(true)
  const [selectedRows, setSelectedRows] = useState<React.Key[]>([])

  useEffect(() => {
    const users = MOCK_USERS.map((user) => ({
      key: user.id,
      name: user.name,
      email: user.email,
      phone: user.phone ?? '',
    }))
    setTableData(users)
  }, [])

  const onAddClick = (): void => {
    console.log('on add click')
  }

  const onDeleteClick = (): void => {
    const newData = tableData.filter((item) => !selectedRows.includes(item.key))
    setSelectedRows([])
    setTableData(newData)
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
      <Table bordered dataSource={tableData} columns={columns} rowSelection={rowSelection} />
    </>
  )
}

export default UserTable
