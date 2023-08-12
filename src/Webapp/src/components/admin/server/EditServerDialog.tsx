import React, { FC, useEffect, useState } from 'react';
import { PropertyInput } from '@components/common';
import { Server, ServerStatus } from '@models';
import { Optional } from '@models/Types';
import { Modal, ModalFuncProps } from 'antd';

interface EditServerDialogProps extends ModalFuncProps {
  server: Server | undefined
  isOpen: boolean
  onSave: (server: Optional<Server, 'id'>) => void
}

const EditServerDialog: FC<EditServerDialogProps> = ({
  server,
  isOpen,
  onSave,
  ...props
}: EditServerDialogProps) => {
  const [name, setName] = useState<string>('');
  const [address, setAddress] = useState<string>('');

  useEffect(() => {
    setName(server?.name ?? '');
    setAddress(server?.idAddress ?? '');
  }, [server, isOpen]);

  const computedTitle = server ? 'Редактировать параметры сервера' : 'Добавить сервер';

  const onOkHandler = (): void => {
    onSave({
      status: ServerStatus.down,
      connectionStatus: false,
      pingStatus: false,
      ...server,
      name,
      idAddress: address,
    });
  };

  const onNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  };

  const onAddressChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setAddress(event.target.value);
  };

  return (
    <Modal title={computedTitle} onOk={onOkHandler} open={isOpen} {...props}>
      <PropertyInput title='Название' value={name} onChange={onNameChange} />
      <PropertyInput title='IP Адрес' value={address} onChange={onAddressChange} />
    </Modal>
  );
};

export default EditServerDialog;
