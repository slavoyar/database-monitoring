enum ServerStatus {
  good = 'good',
  warn = 'warn',
  down = 'down',
}

export interface Server {
  name: string
  status: ServerStatus
  icon: string
  address: string
}

export default ServerStatus
