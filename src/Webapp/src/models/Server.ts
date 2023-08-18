enum ServerStatus {
  good = 'Working',
  warn = 'Warn',
  down = 'Down',
}

type ServerId = string

interface ServerBase {
  id: ServerId;
  name: string;
  idAddress: string;
}

interface Server extends ServerBase {
  status: ServerStatus;
  pingStatus: boolean;
  connectionStatus: boolean;
  lastSuccessLog?: string;
}

interface ServerShort extends ServerBase {
  status?: ServerStatus;
  countOfLogs?: number;
}

export { ServerStatus, type ServerBase, type Server, type ServerShort, type ServerId };
