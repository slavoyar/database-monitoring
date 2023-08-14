import React, { FC, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Navbar } from '@components/common/';
import { Notification, NotificationId, UserId, WorkspaceId } from '@models';
import { useGetUnreadNotificationsQuery, useMarkNotificationsAsReadMutation }
  from '@redux/api/notificationApi';
import { RootState } from '@redux/store';
import { Button, Layout, List } from 'antd';

import '@css/Notifications.css';

// const notification: Notification[] = [{ id: '1', data: 'something', creationDate: '12.09.2001' },
// { id: '2', data: 'something x 2', creationDate: '12.09.2001' }];

const NotificationsPage: FC = () => {
  const [notificationData, setNotificationData] = useState<Notification[]>([]);
  const userId = useSelector<RootState>(state => state.authState.user?.id) as UserId;
  const workspaceId = useSelector<RootState>(state => state.authState.workspaceId) as WorkspaceId;
  const { data: notifications, isLoading } =
    useGetUnreadNotificationsQuery({ userId, workspaceId }, { skip: !userId || !workspaceId });
  const [markAsRead] = useMarkNotificationsAsReadMutation();

  const onMarkAsReadClick = async (): Promise<void> => {
    const notificationsId = notificationData.map(nd => nd.id) as NotificationId[];
    await markAsRead({ userId, notificationsId });
    setNotificationData([]);
  };


  useEffect(() => {
    if (notifications) {
      setNotificationData(
        notifications.map((notification) => ({
          id: notification.id,
          data: notification.data,
          creationDate: new Date(notification.creationDate).toUTCString(),
        })),
      );
    }
  }, [notifications]);

  return (
    <Layout>
      <Navbar />
      <Layout.Content className='notifications-content'>
        <List
          className='notifications-list'
          size="large"
          header={<div className='notifications-list_header'>
            <h1>Уведомления</h1>
            <Button
              onClick={onMarkAsReadClick}
              type="primary">Пометить как прочитано</Button>
          </div>}
          bordered
          loading={isLoading}
          dataSource={notificationData}
          renderItem={(item: Notification) =>
            <List.Item key={item.id}>
              <List.Item.Meta
                title={item.data}
                description={item.creationDate}
              />
            </List.Item>}
        />
      </Layout.Content>
    </Layout>
  );
};

export default NotificationsPage;
