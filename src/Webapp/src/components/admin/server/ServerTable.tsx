import React, { FC, useEffect, useState } from 'react';
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { ServerShort } from '@models';
import { Optional } from '@models/Types';
import { Button, Table } from 'antd';

import EditServerDialog from './EditServerDialog';

enum ServerTableColumn {
  NAME = 'name',
  STATUS = 'status',
  ADDRESS = 'address',
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
  const [tableDate, setTableData] = useState<ServerShort[]>([]);
  const [deleteDisabled, setDeleteDisabled] = useState(true);
  const [selectedRows, setSelectedRows] = useState<React.Key[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentServer, setcurrentServer] = useState<ServerShort | undefined>(undefined);

  useEffect(() => {
  }, []);

  const onAddClick = () => {
    setIsModalOpen(true);
  };

  const onDeleteClick = () => {
    const newData = tableDate.filter((item) => !selectedRows.includes(item.id));
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

  const onSaveHandler = (server: Omit<Optional<ServerShort, 'id'>, 'status'>): void => {
    console.error(server);
    close();
  };

  const rowSelection = {
    selectedRows,
    onChange: onSelectChange,
  };

  const onRowClick = (record: ServerShort): void => {
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
        rowKey='id'
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
