import React, { FC, useEffect, useState } from 'react';
import { PropertyInput } from '@components/common';
import { ServerShort } from '@models';
import { Optional } from '@models/Types';
import { Modal, ModalFuncProps } from 'antd';

interface EditServerDialogProps extends ModalFuncProps {
  server: ServerShort | undefined
  isOpen: boolean
  onSave: (server: Omit<Optional<ServerShort, 'id'>, 'status'>) => void
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
    setAddress(server?.ipAddress ?? '');
  }, [server, isOpen]);

  const computedTitle = server ? 'Редактировать параметры сервера' : 'Добавить сервер';

  const onOkHandler = (): void => {
    onSave({
      id: server?.id,
      name,
      ipAddress: address,
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
