import React, { FC, useEffect, useState } from 'react';
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { Server, User } from '@models';
import { Optional } from '@models/Types';
import { MOCK_WORKSPACES } from '@models/Workspace';
import { Button, Table } from 'antd';

import EditWorkspaceDialog from './EditWorkspaceDialog';

enum WorkspaceTableColumn {
  NAME = 'name',
  USERS = 'users',
  SERVERS = 'servers',
}

export interface WorkspaceTableData {
  key: string
  [WorkspaceTableColumn.NAME]: string
  [WorkspaceTableColumn.USERS]: User[]
  [WorkspaceTableColumn.SERVERS]: Server[]
}

function userArrayToString(arr: User[]): string {
  return arr.map((item) => item.fullUserName).join(', ');
}

function serverArrayToString(arr: Server[]): string {
  return arr.map((item) => item.name).join(', ');
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
    render: (data: User[]) => userArrayToString(data),
  },
  {
    title: 'Сервера',
    dataIndex: WorkspaceTableColumn.SERVERS,
    key: WorkspaceTableColumn.SERVERS,
    render: (data: Server[]) => serverArrayToString(data),
  },
];

const WorkspaceTable: FC = () => {
  const [data, setData] = useState<WorkspaceTableData[]>([]);
  const [deleteDisabled, setDeleteDisabled] = useState(true);
  const [selectedRows, setSelectedRows] = useState<React.Key[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentWorkspace, setCurrentWorkspace] = useState<WorkspaceTableData | undefined>(
    undefined,
  );

  useEffect(() => {
    const tableData = MOCK_WORKSPACES.map((workspace) => ({
      key: workspace.id as string,
      name: workspace.name,
      users: workspace.users,
      servers: workspace.servers,
    }));
    setData(tableData);
  }, []);

  const onAddClick = () => {
    setIsModalOpen(true);
  };

  const onDeleteClick = () => {
    const newData = data.filter((item) => !selectedRows.includes(item.key));
    setSelectedRows([]);
    setData(newData);
    setDeleteDisabled(true);
  };

  const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
    setDeleteDisabled(!newSelectedRowKeys.length);
    setSelectedRows(newSelectedRowKeys);
  };

  const close = (): void => {
    if (isModalOpen) {
      setIsModalOpen(false);
    }
    if (currentWorkspace) {
      setCurrentWorkspace(undefined);
    }
  };

  const onSaveHandler = (workspace: Optional<WorkspaceTableData, 'key'>): void => {
    const result = [...data];
    const workspaceIndex = data.findIndex((item) => item.key === workspace.key);
    if (workspaceIndex < 0) {
      const newWorkspace = {
        ...workspace,
        key: String(data.length + 1),
      };
      result.push(newWorkspace);
    }
    result[workspaceIndex] = { ...workspace } as WorkspaceTableData;
    setData(result);
    close();
  };
  const rowSelection = {
    selectedRows,
    onChange: onSelectChange,
  };

  const onRowClick = (record: WorkspaceTableData): void => {
    setCurrentWorkspace(record);
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
        dataSource={data}
        columns={columns}
        rowSelection={rowSelection}
        onRow={(record) => ({ onClick: () => onRowClick(record) })}
      />
      <EditWorkspaceDialog
        workspace={currentWorkspace}
        isOpen={!!currentWorkspace || isModalOpen}
        onCancel={close}
        onSave={onSaveHandler}
      />
    </>
  );
};

export default WorkspaceTable;
