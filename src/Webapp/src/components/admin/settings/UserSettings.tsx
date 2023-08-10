import React, { FC, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { User } from '@models';
import { useUpdateUserMutation } from '@redux/api/api';
import { updateUser } from '@redux/features/authSlice';
import { RootState } from '@redux/store';
import { Button, Form, Input } from 'antd';

import '@css/UserSettings.css';

const UserSettings: FC = () => {
  const [form] = Form.useForm();
  const userState = useSelector<RootState>(state => state.authState.user) as User;
  const dispatch = useDispatch();
  const [user, setUser] = useState<User>(userState);

  const [updateUserQuery] = useUpdateUserMutation();

  useEffect(() => {
    if (userState) {
      form.setFieldsValue({ ...userState });
    }
  }, [userState]);

  const onUpdateClick = async (): Promise<void> => {
    await updateUserQuery(user);
    dispatch(updateUser(user));
  };

  const onFormChange = () => {
    setUser({ ...form.getFieldsValue() });
  };

  return (
    <div className='user-settings'>
      <Form
        form={form}
        name='user'
        labelCol={{ span: 2 }}
        wrapperCol={{ span: 8 }}
        initialValues={{ remember: true }}
        autoComplete='off'
        className='user-settings__form'
        onChange={onFormChange}
      >
        <Form.Item wrapperCol={{ span: 6, offset: 2 }}>
          <h1>Настройки профиля</h1>
        </Form.Item>

        <Form.Item label='Имя' name='fullUserName'>
          <Input value={user?.fullUserName} />
        </Form.Item>

        <Form.Item label='Email' name='email'>
          <Input value={user?.email} />
        </Form.Item>

        <Form.Item label='Телефон' name='phoneNumber'>
          <Input value={user?.phoneNumber} />
        </Form.Item>

        <Form.Item
          label='Пароль'
          name='password'
        >
          <Input.Password value={user?.password} />
        </Form.Item>
        <Form.Item wrapperCol={{ span: 8, offset: 2 }}>
          <Button type='primary' htmlType='submit' onClick={onUpdateClick}>
            Обновить
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default UserSettings;
