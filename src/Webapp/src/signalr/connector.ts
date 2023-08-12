import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Server, ServerId } from '@models';

class Connector {

    public events: (onMessageReceived: (server: Server) => void) => void;

    private connection: HubConnection;

    private groupServerIds: ServerId[];

    // eslint-disable-next-line no-use-before-define
    private static instance: Connector;

    constructor() {
        this.groupServerIds = [];
        this.connection = new HubConnectionBuilder()
            .withUrl('/serverState', {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
            })
            .withAutomaticReconnect()
            .build();
        this.connection.start().catch(console.error);
        this.events = (onMessageReceived) => {
            this.connection.on('Receive', (server) => {
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
        if (this.groupServerIds?.length) {
            throw new Error('Need to unsubscribe first');
        }
        this.groupServerIds = serverIds;
        await this.connection.invoke('SubscribeToGroup', serverIds);
    }

    public async unsubscribeFromGroup(): Promise<void> {
        await this.connection.invoke('UnsubscribeToGroup', this.groupServerIds);
        this.groupServerIds = [];
    }
}

export default Connector.getInstance;