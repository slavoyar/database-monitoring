import React, { FC } from 'react'
import { useNavigate } from 'react-router-dom'
import { Form, Input, Button } from 'antd'
import { ValidateErrorEntity } from 'rc-field-form/es/interface'
import 'css/LoginPage.css'

interface LoginForm {
  login: string
  password: string
}

const LoginPage: FC = () => {
  const navigate = useNavigate()

  const onFinish = (values: LoginForm) => {
    console.log('LOGIN', values)
    navigate('/')
  }

  const onFinishFailed = (errorInfo: ValidateErrorEntity<LoginForm>) => {
    console.error('ERROR in fields', errorInfo)
  }

  return (
    <div className='login-page'>
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
          label='Логин'
          name='login'
          rules={[{ required: true, message: 'Пожалуйста введите логин!' }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label='Пароль'
          name='password'
          rules={[{ required: true, message: 'Пожалуйста введите пароль!' }]}
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
  )
}

export default LoginPage
