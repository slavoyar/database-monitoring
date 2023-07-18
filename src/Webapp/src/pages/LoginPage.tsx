import React, { FC } from 'react'
import { useNavigate } from 'react-router-dom'
import { AuthLoginModel, useLoginUserMutation } from '@stores/api/authApi'
import { Button, Form, Input, Layout } from 'antd'
import { ValidateErrorEntity } from 'rc-field-form/es/interface'

import '@css/LoginPage.css'

const LoginPage: FC = () => {
  const navigate = useNavigate()

  const [loginUser] = useLoginUserMutation();

  const onFinish = (values: AuthLoginModel) => {
    loginUser(values)
    console.log('LOGIN', values)
    navigate('/')
  }

  const onFinishFailed = (errorInfo: ValidateErrorEntity<AuthLoginModel>) => {
    console.error('ERROR in fields', errorInfo)
  }

  return (
    <Layout className='login-layout'>
      <div className='login-container'>
        <h1>Авторизация</h1>
        <Form
          name='login'
          labelCol={{ span: 4 }}
          wrapperCol={{ span: 20 }}
          initialValues={{ remember: true }}
          autoComplete='off'
          className='login-form'
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
        >
          <Form.Item
            label='Email'
            name='email'
            rules={[{ required: true, message: 'Введите email!' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label='Пароль'
            name='password'
            rules={[{ required: true, message: 'Введите пароль!' }]}
          >
            <Input.Password />
          </Form.Item>
          <Form.Item wrapperCol={{ span: 24 }}>
            <Button type='primary' htmlType='submit'>
              Войти
            </Button>
          </Form.Item>
        </Form>
      </div>
    </Layout>
  )
}

export default LoginPage
