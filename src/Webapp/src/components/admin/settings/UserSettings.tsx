import React, { FC, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { User } from '@models';
import { useGetUserInfoQuery, useUpdateUserMutation } from '@redux/api/authApi';
import { RootState } from '@redux/store';
import { Button, Form, Input } from 'antd';

import '@css/UserSettings.css';

const UserSettings: FC = () => {
  const [form] = Form.useForm();
  const [updateUser] = useUpdateUserMutation();

  const userEmail = useSelector<RootState, string>(state => state.authState.email);
  const { data: user } = useGetUserInfoQuery(userEmail);

  useEffect(() => {
    if (user) {
      form.setFieldsValue({ ...user });
    }
  }, [user]);

  const onUpdateClick = (): void => {
    if (user) {
      updateUser(user);
    }
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
