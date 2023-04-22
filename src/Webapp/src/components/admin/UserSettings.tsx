import React, { FC } from 'react'
import { Form, Input, Button } from 'antd'
import { ValidateErrorEntity } from 'rc-field-form/es/interface'
import 'css/UserSettings.css'

interface UserSettingsForm {
  login: string
  firstName: string
  lastName: string
  email: string
  phone: string
  password: string
}

const UserSettings: FC = () => {
  const onFinish = (values: UserSettingsForm) => {
    console.log('LOGIN', values)
  }

  const onFinishFailed = (errorInfo: ValidateErrorEntity<UserSettingsForm>) => {
    console.error('ERROR in fields', errorInfo)
  }
  return (
    <div className='user-settings'>
      <Form
        name='user'
        labelCol={{ span: 2 }}
        wrapperCol={{ span: 8 }}
        initialValues={{ remember: true }}
        autoComplete='off'
        className='user-settings__form'
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
      >
        <Form.Item wrapperCol={{ span: 6, offset: 2 }}>
          <h1>Настройки профиля</h1>
        </Form.Item>
        <Form.Item
          label='Логин'
          name='login'
          rules={[{ required: true, message: 'Пожалуйста введите логин!' }]}
        >
          <Input />
        </Form.Item>

        <Form.Item label='Имя' name='firstName'>
          <Input />
        </Form.Item>

        <Form.Item label='Фамилия' name='lastName'>
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
          rules={[{ required: true, message: 'Пожалуйста введите пароль!' }]}
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
  )
}

export default UserSettings
