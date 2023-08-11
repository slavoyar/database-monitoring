enum ServerStatus {
  good = 'good',
  warn = 'warn',
  down = 'down',
}

type ServerId = string

interface Server {
  id: ServerId;
  name: string;
  status: ServerStatus;
  pingStatus: boolean;
  connectionStatus: string;
  lastSuccessLog: string;
  ipAddress: string;
}

interface ServerShort {
  id: ServerId;
  name: string;
  ipAddress: string;
  status?: ServerStatus;
  countOfLogs?: number;
}

export { ServerStatus, type Server, type ServerShort, type ServerId };
