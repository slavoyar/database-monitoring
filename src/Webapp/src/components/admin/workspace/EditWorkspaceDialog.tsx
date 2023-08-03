import React, { FC, useEffect, useState } from 'react';
import { PropertyInput, PropertySelect } from '@components/common';
import { Workspace } from '@models';
import { MOCK_SERVERS } from '@models/Server';
import { ValueWithLabel } from '@models/Types';
import { isAuthResponse, useFetchUsersQuery } from '@redux/api/api';
import { Modal, ModalFuncProps } from 'antd';

interface EditWorkspaceDialogProps extends ModalFuncProps {
  workspace: Workspace | undefined
  isOpen: boolean
  onSave: (workspace: Workspace) => void
}

const EditWorkspaceDialog: FC<EditWorkspaceDialogProps> = ({
  workspace,
  isOpen,
  onSave,
  ...props
}: EditWorkspaceDialogProps) => {
  const [name, setName] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [users, setUsers] = useState<string[]>([]);
  const [servers, setServers] = useState<string[]>([]);
  const [userOptions, setUserOptions] = useState<ValueWithLabel[]>([]);

  const { data: fetchedUsers } = useFetchUsersQuery();

  useEffect(() => {
    setName(workspace?.name ?? '');
    setDescription(workspace?.description ?? '');
    setUsers(workspace?.users ?? []);
    setServers(workspace?.servers ?? []);
  }, [workspace, isOpen]);

  useEffect(() => {
    if (fetchedUsers && !isAuthResponse(fetchedUsers)) {
      setUserOptions(fetchedUsers.$values.map(user => (
        {
          value: user.id,
          label: user.fullUserName,
        }
      )));
    }
  }, [fetchedUsers]);

  const serverOptions = MOCK_SERVERS.map((server) => ({ value: server.id, label: server.name }));

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

  return (
    <Modal title={computedTitle} onOk={onOkHandler} open={isOpen} {...props}>
      <PropertyInput title='Название' value={name} onChange={onNameChange} />
      <PropertyInput title='Описание' value={description} onChange={onDescriptionChange} />
      <PropertySelect
        title='Пользователи'
        options={userOptions}
        mode='multiple'
        value={users}
        onChange={setUsers}
        allowClear
      />
      <PropertySelect
        title='Серверы'
        options={serverOptions}
        mode='multiple'
        value={servers}
        onChange={setServers}
        allowClear
      />
    </Modal>
  );
};

export default EditWorkspaceDialog;
