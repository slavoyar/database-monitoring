enum ServerStatus {
  good = 'good',
  warn = 'warn',
  down = 'down',
}

type ServerId = string

interface Server {
  id: ServerId
  name: string
  status: ServerStatus
  icon: string
  address: string
}

export const MOCK_SERVERS: Server[] = [
  {
    id: 'server1',
    name: 'Server 1',
    icon: 'android',
    status: ServerStatus.good,
    address: '192.168.1.1',
  },
  {
    id: 'server2',
    name: 'Server 2',
    icon: 'apple',
    status: ServerStatus.warn,
    address: '192.168.1.1',
  },
  {
    id: 'server3',
    name: 'Server 3',
    icon: 'windows',
    status: ServerStatus.down,
    address: '192.168.1.1',
  },
  {
    id: 'server4',
    name: 'Server 4',
    icon: 'android',
    status: ServerStatus.good,
    address: '192.168.1.1',
  },
  {
    id: 'server5',
    name: 'Server 5',
    icon: 'apple',
    status: ServerStatus.warn,
    address: '192.168.1.1',
  },
  {
    id: 'server6',
    name: 'Server 6',
    icon: 'windows',
    status: ServerStatus.down,
    address: '192.168.1.1',
  },
  {
    id: 'server7',
    name: 'Server 7',
    icon: 'android',
    status: ServerStatus.good,
    address: '192.168.1.1',
  },
  {
    id: 'server8',
    name: 'Server 8',
    icon: 'apple',
    status: ServerStatus.warn,
    address: '192.168.1.1',
  },
  {
    id: 'server9',
    name: 'Server 9',
    icon: 'windows',
    status: ServerStatus.down,
    address: '192.168.1.1',
  },
];

export { ServerStatus, type Server, type ServerId };
