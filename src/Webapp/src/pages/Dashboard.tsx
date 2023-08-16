import React, { FC, useEffect, useState } from 'react';
import { Navbar } from '@components/common/';
import { Cards } from '@components/dashboard/';
import { ServerShort } from '@models';
import { useGetServerByIdsShortQuery } from '@redux/api/agregationApi';
import Connector from '@signalr/connector';
import { Layout } from 'antd';

import '@css/Dashboard.css';

const Dashboard: FC = () => {
  // TODO: Add ability to get servers from workspace
  const { subscribeToGroup, events } = Connector();
  const [servers, setServers] = useState<ServerShort[]>([]);
  const MOCK_IDS = ['8d8a6029-676a-4e09-91c5-32c56602f67f', 'd13920a2-4961-43cc-bd22-12187b19f512'];
  const { data: fetchedServers } = useGetServerByIdsShortQuery(MOCK_IDS);

  useEffect(() => {
    subscribeToGroup(MOCK_IDS);
    events((server) => {
      console.error('IN DASHBOARD');
      const updatedServers = [...servers];
      console.warn(updatedServers, server);
      const serverIndex = updatedServers.findIndex(item => item.id === server.id);
      if (serverIndex >= 0) {
        updatedServers[serverIndex] = server;
        console.warn(updatedServers);
        setServers(updatedServers);
      }
    });
  });

  useEffect(() => {
    if (fetchedServers) {
      setServers(fetchedServers);
    }
  }, [fetchedServers]);

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
