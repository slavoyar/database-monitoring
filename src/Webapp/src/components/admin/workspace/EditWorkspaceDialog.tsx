import React, { FC, useEffect, useState } from 'react';
import { PropertyInput, PropertySelect } from '@components/common';
import { User, UserId, WorkspaceTableData } from '@models';
import { Server, ServerId } from '@models/Server';
import { ValueWithLabel } from '@models/Types';
import { Modal, ModalFuncProps } from 'antd';

interface EditWorkspaceDialogProps extends ModalFuncProps {
  workspace: WorkspaceTableData | undefined;
  users: User[];
  servers: Server[];
  isOpen: boolean;
  onSave: (workspace: WorkspaceTableData) => void;
}

const EditWorkspaceDialog: FC<EditWorkspaceDialogProps> = ({
  workspace,
  isOpen,
  onSave,
  users: fetchedUsers,
  servers: fetchedServers,
  ...props
}: EditWorkspaceDialogProps) => {
  const [name, setName] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [users, setUsers] = useState<User[]>([]);
  const [servers, setServers] = useState<Server[]>([]);
  const [userOptions, setUserOptions] = useState<ValueWithLabel[]>([]);
  const [serverOptions, setServerOptions] = useState<ValueWithLabel[]>([]);

  useEffect(() => {
    setName(workspace?.name ?? '');
    setDescription(workspace?.description ?? '');
    setUsers(workspace?.users ?? []);
    setServers(workspace?.servers ?? []);
  }, [workspace, isOpen]);

  useEffect(() => {
    setUserOptions(fetchedUsers.map(user => (
      {
        value: user.id,
        label: user.fullUserName,
      }
    )));
  }, [fetchedUsers]);

  useEffect(() => {
    setServerOptions(fetchedServers.map(server => (
      {
        value: server.id,
        label: server.name,
      }
    )));
  }, [fetchedServers]);


  const computedTitle = workspace
    ? 'Редактировать рабочее пространство'
    : 'Создать рабочее пространство';

  const onOkHandler = (): void => {
    onSave({
      id: workspace?.id,
      name,
      users,
      servers,
      description,
    });
  };

  const onNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  };

  const onDescriptionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setDescription(event.target.value);
  };

  const onUserChange = (ids: UserId[]) => {
    setUsers(fetchedUsers.filter(u => ids.includes(u.id)));
  };

  const onServerChange = (ids: ServerId[]) => {
    setServers(fetchedServers.filter(s => ids.includes(s.id)));
  };

  return (
    <Modal title={computedTitle} onOk={onOkHandler} open={isOpen} {...props}>
      <PropertyInput title='Название' value={name} onChange={onNameChange} />
      <PropertyInput title='Описание' value={description} onChange={onDescriptionChange} />
      <PropertySelect
        title='Пользователи'
        options={userOptions}
        mode='multiple'
        value={users.map(u => u.id)}
        onChange={onUserChange}
        allowClear
      />
      <PropertySelect
        title='Серверы'
        options={serverOptions}
        mode='multiple'
        value={servers.map(s => s.id)}
        onChange={onServerChange}
        allowClear
      />
    </Modal>
  );
};

export default EditWorkspaceDialog;
