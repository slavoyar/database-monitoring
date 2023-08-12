import React, { FC } from 'react';
import { Navbar } from '@components/common/';
import { Cards } from '@components/dashboard/';
import { useGetServerByIdsShortQuery } from '@redux/api/agregationApi';
import { Layout } from 'antd';

import '@css/Dashboard.css';

const Dashboard: FC = () => {
  const MOCK_IDS = ['8d8a6029-676a-4e09-91c5-32c56602f67f', 'd13920a2-4961-43cc-bd22-12187b19f512']
  const { data: servers } = useGetServerByIdsShortQuery(MOCK_IDS);
  return (
    <Layout>
      <Navbar />
      <Layout.Content className='dashboard-content'>
        <div className='card-container'>
          <Cards servers={servers ?? []} />
        </div>
      </Layout.Content>
    </Layout>
  );
};

export default Dashboard;
