import React, { FC } from 'react';
import { Navbar } from '@components/common/';
import { Button, Layout, List } from 'antd';

import '@css/Notifications.css';

const notification = ['something', 'happend', 'rigth', 'here'];

const NotificationsPage: FC = () => (
  <Layout>
    <Navbar />
    <Layout.Content className='notifications-content'>
        <List
            className='notifications-list'
            size="large"
            header={<><h1>Уведомления</h1> <Button type="primary">Primary Button</Button></>}
            bordered
            dataSource={notification}
            renderItem={(item) => <List.Item>{item}</List.Item>}
        />
    </Layout.Content>
  </Layout>
);

export default NotificationsPage;
