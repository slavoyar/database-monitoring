import React, { FC, useEffect, useState } from 'react';
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { User } from '@models';
import {
  isAuthResponse,
  useCreateUserMutation,
  useDeleteUserMutation,
  useFetchUsersQuery,
  useUpdateUserMutation,
} from '@redux/api/api';
import { Button, Table } from 'antd';

import EditUserDialog from './EditUserDialog';

export type UserWithKey = Omit<User, 'id'> & { key?: string }

enum UserTableColumn {
  NAME = 'fullUserName',
  EMAIL = 'email',
  PHONE = 'phoneNumber',
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
];

const UserTable: FC = () => {
  const [tableData, setTableData] = useState<UserTableData[]>([]);
  const [deleteDisabled, setDeleteDisabled] = useState(true);
  const [selectedRows, setSelectedRows] = useState<React.Key[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentUser, setCurrentUser] = useState<UserTableData>();
  const { data: users, isLoading } = useFetchUsersQuery();
  const [deleteUser] = useDeleteUserMutation();
  const [createUser] = useCreateUserMutation();
  const [updateUser] = useUpdateUserMutation();

  useEffect(() => {
    if (users && !isAuthResponse(users)) {
      setTableData(
        users.map((user) => ({
          key: user.id,
          fullUserName: user.fullUserName,
          email: user.email,
          phoneNumber: user.phoneNumber ?? '',
        })),
      );
    }
  }, [users]);

  const onAddClick = (): void => {
    setIsModalOpen(true);
  };

  const onDeleteClick = async (): Promise<void> => {
    const userEmail = tableData.find((user) => user.key === selectedRows[0])?.email;
    if (userEmail) {
      await deleteUser(userEmail);
    }
    setSelectedRows([]);
    setDeleteDisabled(true);
  };

  const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
    setDeleteDisabled(!newSelectedRowKeys.length);
    setSelectedRows(newSelectedRowKeys);
  };

  const rowSelection = {
    selectedRows,
    onChange: onSelectChange,
  };

  const close = (): void => {
    if (isModalOpen) {
      setIsModalOpen(false);
    }
    if (currentUser) {
      setCurrentUser(undefined);
    }
  };

  const onRowClick = (record: UserTableData): void => {
    setCurrentUser(record);
  };

  const onSaveHandler = async (user: UserWithKey): Promise<void> => {
    const promise = user.key ? updateUser : createUser;
    const userToSave = { ...user } as User;
    try {
      await promise(userToSave);
      close();
    } catch (e) {
      console.error(e);
    }
  };

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
      <Table
        bordered
        loading={isLoading}
        dataSource={tableData}
        columns={columns}
        rowSelection={rowSelection}
        onRow={(record) => ({ onClick: () => onRowClick(record) })}
      />
      <EditUserDialog
        isOpen={!!currentUser || isModalOpen}
        user={currentUser}
        onSave={onSaveHandler}
        onCancel={close}
      />
    </>
  );
};

export default UserTable;
