import React, { FC } from 'react'
import { useNavigate } from 'react-router-dom'
import { AuthLoginModel, useLoginMutation } from '@redux/api/authApi'
import { Button, Form, Input, Layout } from 'antd'

import '@css/LoginPage.css'

const LoginPage: FC = () => {
  const navigate = useNavigate()

  const [loginUser, { isSuccess }] = useLoginMutation();

  const onFinish = async (values: AuthLoginModel) => {
    await loginUser(values)
    if (isSuccess) {
      navigate('/')
    }
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
