import {
    HubConnection,
    HubConnectionBuilder,
    HubConnectionState,
} from '@microsoft/signalr';
import { Server, ServerId } from '@models';

enum MethodName {
    subscribeToGroup = 'SubscribeToGroup',
    unsubscribeToGroup = 'UnsubscribeToGroup',
}

class Connector {

    public events: (onMessageReceived: (server: Server) => void) => void;

    private connection: HubConnection;

    private groupServerIds: ServerId[];

    private invokeQueue: Record<string, ServerId[]> = {};

    // eslint-disable-next-line no-use-before-define
    private static instance: Connector;

    constructor() {
        this.groupServerIds = [];
        this.connection = new HubConnectionBuilder()
            .withUrl('http://localhost:5002/serverState', { withCredentials: false })
            .withAutomaticReconnect()
            .build();
        this.connection.start()
            .then(() => {
                console.log('SignalR connection STARTED');
                // eslint-disable-next-line no-underscore-dangle
                this.subscribeToGroup(this.invokeQueue[MethodName.subscribeToGroup]);
            })
            .catch(console.error);
        this.events = (onMessageReceived) => {
            console.warn('onMessageReceived');
            const _instance = Connector.getInstance();
            _instance.connection.on('Receive', (server) => {
                console.log('on Receive');
                onMessageReceived(server);
            });
        };
    }

    public static getInstance(): Connector {
        if (!Connector.instance)
            Connector.instance = new Connector();
        return Connector.instance;
    }

    public async subscribeToGroup(serverIds: ServerId[]): Promise<void> {
        // eslint-disable-next-line no-underscore-dangle
        const _instance = Connector.getInstance();
        if (_instance.connection.state !== HubConnectionState.Connected) {
            _instance.invokeQueue[MethodName.subscribeToGroup] = serverIds;
            return;
        }
        await _instance.connection.invoke('SubscribeToGroup', serverIds);
    }
}

export default Connector.getInstance;