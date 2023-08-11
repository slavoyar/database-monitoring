import { UserId, WorkspaceId } from '@models';

import { api } from './api';


type NotificationId = string;

interface GetNotificationRequest {
    userId: UserId;
    workspaceId: WorkspaceId;
};

interface NotificationResponse {
    id: NotificationId;
    data: string;
    creationDate: string;
}

interface MarkNotificationsAsReadRequest {
    userId: UserId;
    notificationsId: NotificationId[];
};

export const notificationApi = api.injectEndpoints({
    endpoints: (build) => ({
        getUnreadNotifications: build.query<NotificationResponse[], GetNotificationRequest>({
            query: (request) => ({
                url: 'notification',
                params: { userId: request.userId, workspaceId: request.workspaceId },
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