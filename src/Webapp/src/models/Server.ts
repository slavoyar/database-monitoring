enum ServerStatus {
  good = 'good',
  warn = 'warn',
  down = 'down',
}

type ServerId = string

interface ServerBase {
  id: ServerId;
  name: string;
  ipAddress: string;
}

interface Server extends ServerBase {
  status: ServerStatus;
  pingStatus: boolean;
  connectionStatus: boolean;
  lastSuccessLog: string;
}

interface ServerShort extends ServerBase {
  status?: ServerStatus;
  countOfLogs?: number;
}

export { ServerStatus, type Server, type ServerShort, type ServerId };
