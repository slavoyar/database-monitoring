import React, { FC, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Path } from '@models'
import { AuthLoginModel, useLoginMutation } from '@redux/api/authApi'
import { Button, Form, Input, Layout } from 'antd'

import '@css/LoginPage.css'

const LoginPage: FC = () => {
  const navigate = useNavigate()
  const [error, setError] = useState<string>('')

  const [loginUser] = useLoginMutation();

  const onFinish = async (values: AuthLoginModel) => {
    try {
      await loginUser(values).unwrap()
      console.log('navigate to dashboard')
      navigate(`/${Path.dashboard}`)
    } catch (e) {
      if (e.data.message) {
        setError(e.data.message as string)
      }
    }
  }

  const onValuesChange = () => {
    setError('')
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
          onValuesChange={onValuesChange}
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
          <Form.Item
            wrapperCol={{ span: 24 }}
            validateStatus={error ? 'error' : ''}
            help={error}
          >
            <Button type='primary' htmlType='submit'>
              Войти
            </Button>
          </Form.Item>
        </Form>
      </div>
    </Layout >
  )
}

export default LoginPage
