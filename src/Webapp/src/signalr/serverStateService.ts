/* eslint-disable no-underscore-dangle */
import {
    HubConnection,
    HubConnectionBuilder,
    HubConnectionState,
} from '@microsoft/signalr';
import { ServerId, ServerShort } from '@models';

class ServerStateService {
    private connection: HubConnection;

    private currentGroupServers: ServerId[];

    constructor() {
        this.currentGroupServers = [];
        this.connection = new HubConnectionBuilder()
            .withUrl('http://localhost:5002/serverState', { withCredentials: false })
            .withAutomaticReconnect()
            .build();
    }

    public async startConnection(): Promise<void> {
        if (this.connection.state !== HubConnectionState.Connected) {
            console.log('Start SignalR connection');
            await this.connection.start();;
        }

    }

    public async subscribeToGroup(serverIds: ServerId[]): Promise<void> {
        if (this.currentGroupServers.length) {
            const isEqual = this.currentGroupServers.length === serverIds.length
                && this.currentGroupServers.every(id => serverIds.includes(id));
            if (!isEqual) {
                console.log('unsubscribeFromGroup');
                await this.connection.invoke('UnsubscribeToGroup', this.currentGroupServers);
            }
        }
        this.currentGroupServers = serverIds;
        console.log('SubscribeToGroup', serverIds);
        await this.connection.invoke('SubscribeToGroup', this.currentGroupServers);
    }

    public onReceive(cb: (server: ServerShort) => void): void {
        if (!this.connection) {
            return;
        }
        this.connection.on('Receive', cb);
    }
}

const serverStateService = new ServerStateService();

export default serverStateService;