import React, { FC } from 'react';
import { Navbar } from '@components/common/';
import { Cards } from '@components/dashboard/';
import { Layout } from 'antd';

import '@css/Dashboard.css';

const Dashboard: FC = () => (
  <Layout>
    <Navbar />
    <Layout.Content className='dashboard-content'>
      <div className='card-container'>
        <Cards servers={[]} />
      </div>
    </Layout.Content>
  </Layout>
);

export default Dashboard;
