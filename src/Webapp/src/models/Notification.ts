type NotificationId = string;

interface Notification {
    id: NotificationId
    data: string
    creationDate: string
}


export { type Notification, type NotificationId };