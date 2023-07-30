import React, { FC } from 'react';
import { User } from '@models';
import { Button, Form, Input } from 'antd';
import { ValidateErrorEntity } from 'rc-field-form/es/interface';

import '@css/UserSettings.css';

const UserSettings: FC = () => (
    <div className='user-settings'>
      <Form
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

        <Form.Item label='Имя' name='firstName'>
          <Input />
        </Form.Item>

        <Form.Item label='Email' name='email'>
          <Input />
        </Form.Item>

        <Form.Item label='Телефон' name='phone'>
          <Input />
        </Form.Item>

        <Form.Item
          label='Пароль'
          name='password'
        >
          <Input.Password />
        </Form.Item>
        <Form.Item wrapperCol={{ span: 8, offset: 2 }}>
          <Button type='primary' htmlType='submit'>
            Обновить
          </Button>
        </Form.Item>
      </Form>
    </div>
  );

export default UserSettings;
