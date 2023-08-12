import React, { FC, useEffect, useState } from 'react';
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { Server } from '@models';
import { Optional } from '@models/Types';
import { useGetServersByPageQuery } from '@redux/api/agregationApi';
import { Button, Table, TablePaginationConfig } from 'antd';

import EditServerDialog from './EditServerDialog';

const DEFAULT_PAGE_SIZE = 10;

enum ServerTableColumn {
  NAME = 'name',
  STATUS = 'status',
  IP_ADDRESS = 'idAddress',
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
    dataIndex: ServerTableColumn.IP_ADDRESS,
    key: ServerTableColumn.IP_ADDRESS,
  },
];

const ServerTable: FC = () => {
  const [pagination, setPagination] = useState<TablePaginationConfig>({
    current: 1,
    pageSize: DEFAULT_PAGE_SIZE,
  });
  const [tableData, setTableData] = useState<Server[]>([]);
  const [deleteDisabled, setDeleteDisabled] = useState(true);
  const [selectedRows, setSelectedRows] = useState<React.Key[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentServer, setCurrentServer] = useState<Server | undefined>(undefined);

  const { data: servers, isLoading }
    = useGetServersByPageQuery({ page: pagination.current ?? 1, itemPerPage: DEFAULT_PAGE_SIZE });

  useEffect(() => {
    if (servers) {
      console.error(servers);
      setTableData(servers);
    }
  }, [servers]);

  const onAddClick = () => {
    setIsModalOpen(true);
  };

  const onDeleteClick = () => {

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
      setCurrentServer(undefined);
    }
  };

  const onSaveHandler = (server: Omit<Optional<Server, 'id'>, 'status'>): void => {
    console.error(server);
    close();
  };

  const rowSelection = {
    selectedRows,
    onChange: onSelectChange,
  };

  const onRowClick = (record: Server): void => {
    console.warn(record);
    setCurrentServer(record);
  };

  const onTableChange = (paging: TablePaginationConfig): void => {
    setPagination(paging);
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
        dataSource={tableData}
        columns={columns}
        rowSelection={rowSelection}
        rowKey='id'
        loading={isLoading}
        pagination={pagination}
        onRow={(record) => ({ onClick: () => onRowClick(record) })}
        onChange={onTableChange}
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
