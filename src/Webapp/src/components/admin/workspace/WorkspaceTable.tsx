import React, { FC, useEffect, useState } from 'react';
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { Server, User } from '@models';
import {
  tableDataToWorkspace,
  workspacesToTableData,
  WorkspaceTableData,
} from '@models/Workspace';
import { useGetServersByPageQuery } from '@redux/api/agregationApi';
import { isAuthResponse, useFetchUsersQuery } from '@redux/api/api';
import {
  useCreateWorkspaceMutation,
  useDeleteWorkspaceMutation,
  useGetAllWorkspacesQuery,
  useUpdateWorkspaceMutation,
} from '@redux/api/workspaceApi';
import { Button, notification, Table } from 'antd';

import EditWorkspaceDialog from './EditWorkspaceDialog';

enum WorkspaceTableColumn {
  NAME = 'name',
  DESCRIPTION = 'description',
  USERS = 'users',
  SERVERS = 'servers',
}

function userArrayToString(arr: User[]): string {
  return arr.map((item) => item.fullUserName).join(', ');
}

function serverArrayToString(arr: Server[]): string {
  return arr.map((item) => item.name).join(', ');
}

const NOTIFICATION_DURATION = 10;

const columns = [
  {
    title: 'Название',
    dataIndex: WorkspaceTableColumn.NAME,
    key: WorkspaceTableColumn.NAME,
  },
  {
    title: 'Описание',
    dataIndex: WorkspaceTableColumn.DESCRIPTION,
    key: WorkspaceTableColumn.DESCRIPTION,
  },
  {
    title: 'Пользователи',
    dataIndex: WorkspaceTableColumn.USERS,
    key: WorkspaceTableColumn.USERS,
    render: (ids: User[]) => userArrayToString(ids),
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

  const { data: fetchedData, isError: isWorkspaceError } = useGetAllWorkspacesQuery();
  const { data: fetchedUsers, isError: isUserError } = useFetchUsersQuery();
  // TODO: add endpoint for all servers by user id.
  const { data: fetchedServers, isError: isServerError }
    = useGetServersByPageQuery({ page: 1, itemPerPage: 100 });
  const [createWorkspace] = useCreateWorkspaceMutation();
  const [updateWorkspace] = useUpdateWorkspaceMutation();
  const [deleteWorkspace] = useDeleteWorkspaceMutation();

  useEffect(() => {
    if (fetchedData && fetchedUsers && fetchedServers && !isAuthResponse(fetchedUsers)) {
      setData(workspacesToTableData(fetchedData, fetchedUsers, fetchedServers));
    }
  }, [fetchedData, fetchedUsers, fetchedServers]);

  useEffect(() => {
    if (isWorkspaceError || isUserError || isServerError) {
      notification.error({
        message: 'Ошибка при загрузке рабочих пространств',
        duration: NOTIFICATION_DURATION,
      });
    }
  }, [isWorkspaceError, isUserError, isServerError]);

  const onAddClick = () => {
    setIsModalOpen(true);
  };

  const onDeleteClick = async (): Promise<void> => {
    try {
      await deleteWorkspace(selectedRows[0] as string);
      setDeleteDisabled(true);
    } catch (e) {
      console.error(e);
    }
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

  const onSaveHandler = async (workspace: WorkspaceTableData): Promise<void> => {
    const workspaceIndex = data.findIndex((item) => item.id === workspace.id);
    const promise = workspaceIndex < 0 ? createWorkspace : updateWorkspace;
    try {
      await promise(tableDataToWorkspace(workspace));
      close();
    } catch (e) {
      console.error(e);
    }
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
        rowKey='id'
      />
      <EditWorkspaceDialog
        workspace={currentWorkspace}
        users={fetchedUsers && !isAuthResponse(fetchedUsers) ? fetchedUsers : []}
        servers={fetchedServers ?? []}
        isOpen={!!currentWorkspace || isModalOpen}
        onCancel={close}
        onSave={onSaveHandler}
      />
    </>
  );
};

export default WorkspaceTable;
