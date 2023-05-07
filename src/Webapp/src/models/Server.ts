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

export { ServerStatus, Server, ServerId }
