import React, { FC, useEffect, useState } from 'react';
import { PropertyInput, PropertySelect } from '@components/common';
import { Role } from '@models';
import { arrayToOptions } from '@utils/utils';
import { Modal, ModalFuncProps } from 'antd';

import { UserTableData, UserWithKey } from './UserTable';

interface EditUserDialogProps extends ModalFuncProps {
  user: UserTableData | undefined
  isOpen: boolean
  onSave: (user: UserWithKey) => void
}

const EditUserDialog: FC<EditUserDialogProps> = ({
  user,
  isOpen,
  onSave,
  ...props
}: EditUserDialogProps) => {
  const [name, setName] = useState<string>('');
  const [email, setEmail] = useState<string>('');
  const [phone, setPhone] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [role, setRole] = useState<Role>(Role.engineer);

  useEffect(() => {
    setName(user?.fullUserName ?? '');
    setEmail(user?.email ?? '');
    setPhone(user?.phoneNumber ?? '');
  }, [user, isOpen]);

  const computedTitle = user ? 'Редактировать пользователя' : 'Создать пользователя';

  const onOkHandler = (): void => {
    onSave({
      key: user?.key,
      fullUserName: name,
      phoneNumber: phone,
      email,
      password,
      role,
    });
  };

  const onInputChange = (event: React.ChangeEvent<HTMLInputElement>, cb: (value: string) => void): void => {
    cb(event.target.value);
  };

  const onRoleChange = (value: string): void => {
    setRole(value as Role);
  };

  return (
    <Modal title={computedTitle} onOk={onOkHandler} open={isOpen} {...props}>
      <PropertyInput title='Имя' value={name} onChange={(event) => onInputChange(event, setName)} />
      <PropertyInput title='Email' value={email} onChange={(event) => onInputChange(event, setEmail)} />
      <PropertyInput title='Телефон' value={phone} onChange={(event) => onInputChange(event, setPhone)} />
      <PropertyInput title='Пароль' value={password} onChange={(event) => onInputChange(event, setPassword)} />
      <PropertySelect
        title='Роль'
        options={arrayToOptions<Role>(Object.values(Role))}
        value={role}
        onChange={onRoleChange}
      />
    </Modal>
  );
};

export default EditUserDialog;
