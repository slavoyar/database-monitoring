import React, { FC, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Navbar } from '@components/common/';
import { Cards } from '@components/dashboard/';
import { ServerShort, WorkspaceId } from '@models';
import { useGetServerByIdsShortQuery } from '@redux/api/agregationApi';
import { useGetWorkspaceServersQuery } from '@redux/api/workspaceApi';
import { RootState } from '@redux/store';
import serverStateService from '@signalr/serverStateService';
import { Layout, notification } from 'antd';

import '@css/Dashboard.css';

const NOTIFICATION_DURATION = 10;

const Dashboard: FC = () => {
  // TODO: Add ability to get servers from workspace
  const [servers, setServers] = useState<ServerShort[]>([]);
  const workspaceId = useSelector<RootState>(store => store.authState.workspaceId);

  const { data: serverIds, isError }
    = useGetWorkspaceServersQuery(workspaceId as WorkspaceId, { skip: !workspaceId });
  const { data: fetchedServers }
    = useGetServerByIdsShortQuery(serverIds ?? [], { skip: !serverIds });

  useEffect(() => {
    serverStateService.startConnection();
  }, []);

  useEffect(() => {
    serverStateService.onReceive(server => {
      const newServers = [...servers];
      const index = newServers.findIndex(s => s.id === server.id);
      if (index >= 0) {
        newServers[index] = server;
      }
      setServers(newServers);
    });
  }, [servers]);

  useEffect(() => {
    if (fetchedServers) {
      setServers(fetchedServers);
    }
    if (serverIds) {
      serverStateService.subscribeToGroup(serverIds);
    }

  }, [fetchedServers]);

  useEffect(() => {
    if (isError) {
      notification.error({
        message: 'Ошибка при получении серверов для рабочего пространства',
        duration: NOTIFICATION_DURATION,
      });
    }
  }, [isError]);

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
