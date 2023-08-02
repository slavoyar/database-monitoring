import React, { FC, useEffect, useState } from 'react';
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { MOCK_SERVERS } from '@models/Server';
import { Optional } from '@models/Types';
import { Button, Table } from 'antd';

import EditServerDialog from './EditServerDialog';

enum ServerTableColumn {
  NAME = 'name',
  STATUS = 'status',
  ADDRESS = 'address',
}

export interface ServerTableData {
  key: string
  [ServerTableColumn.NAME]: string
  [ServerTableColumn.STATUS]: string
  [ServerTableColumn.ADDRESS]: string
}

const columns = [
  {
    title: 'Название',
    dataIndex: ServerTableColumn.NAME,
    key: ServerTableColumn.NAME,
  },
  {
    title: 'Статус',
    dataIndex: ServerTableColumn.STATUS,
    key: ServerTableColumn.STATUS,
  },
  {
    title: 'Адрес',
    dataIndex: ServerTableColumn.ADDRESS,
    key: ServerTableColumn.ADDRESS,
  },
];

const ServerTable: FC = () => {
  const [tableDate, setTableData] = useState<ServerTableData[]>([]);
  const [deleteDisabled, setDeleteDisabled] = useState(true);
  const [selectedRows, setSelectedRows] = useState<React.Key[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentServer, setcurrentServer] = useState<ServerTableData | undefined>(undefined);

  useEffect(() => {
    const data = MOCK_SERVERS.map((server) => ({
      key: server.id,
      address: server.address,
      name: server.name,
      status: server.status,
    }));
    setTableData(data);
  }, []);

  const onAddClick = () => {
    setIsModalOpen(true);
  };

  const onDeleteClick = () => {
    const newData = tableDate.filter((item) => !selectedRows.includes(item.key));
    setSelectedRows([]);
    setTableData(newData);
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
    if (currentServer) {
      setcurrentServer(undefined);
    }
  };

  const onSaveHandler = (server: Omit<Optional<ServerTableData, 'key'>, 'status'>): void => {
    const result = [...tableDate];
    const index = tableDate.findIndex((item) => item.key === server.key);
    if (index < 0) {
      const newWorkspace = {
        ...server,
        key: String(tableDate.length + 1),
        status: 'bad',
      };
      result.push(newWorkspace);
    }
    result[index] = { ...server } as ServerTableData;
    setTableData(result);
    close();
  };

  const rowSelection = {
    selectedRows,
    onChange: onSelectChange,
  };

  const onRowClick = (record: ServerTableData): void => {
    setcurrentServer(record);
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
        dataSource={tableDate}
        columns={columns}
        rowSelection={rowSelection}
        onRow={(record) => ({ onClick: () => onRowClick(record) })}
      />
      <EditServerDialog
        server={currentServer}
        isOpen={!!currentServer || isModalOpen}
        onCancel={close}
        onSave={onSaveHandler}
      />
    </>
  );
};

export default ServerTable;
