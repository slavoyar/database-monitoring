import { UserId, WorkspaceId } from '@models';

import { api } from './api';


type NotificationId = string;

interface NotificationRequest { 
    userId: UserId;
    workspaceId: WorkspaceId;
};

interface NotificationResponse {
    id: NotificationId;
    data: string;
    dateTime: string;
    serverId?: string;
    userId?: string;
}

interface MarkNotificationsAsReadRequest { 
    userId: UserId;
    notificationsId: NotificationId[];
};

export const notificationApi = api.injectEndpoints({
    endpoints: (build) => ({
        getUnreadNotifications: build.query<NotificationResponse[], NotificationRequest>({
            query: (request) => ({
                url: 'notification',
                params: request,
            }),
        }),
        markNotificationsAsRead: build.mutation<void, MarkNotificationsAsReadRequest>({
            query: (request) => ({
                url: 'notification',
                method: 'post',
                body: request,
            }),
        }),
    }),
    overrideExisting: false,
});

export const {
    useGetUnreadNotificationsQuery,
    useMarkNotificationsAsReadMutation,
} = notificationApi;