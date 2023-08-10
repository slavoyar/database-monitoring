import React, { FC } from 'react';
import { Navbar } from '@components/common/';
import { Cards, SearchPanel } from '@components/dashboard/';
import { MOCK_SERVERS } from '@models/Server';
import { Layout } from 'antd';

import '@css/Dashboard.css';

const Dashboard: FC = () => (
  <Layout>
    <Navbar />
    <Layout.Content className='dashboard-content'>
      <div className='card-container'>
        <SearchPanel/>
        <Cards servers={MOCK_SERVERS} />
      </div>
    </Layout.Content>
  </Layout>
);

export default Dashboard;
