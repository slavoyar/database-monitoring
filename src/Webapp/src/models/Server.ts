enum ServerStatus {
  good = 'good',
  warn = 'warn',
  down = 'down',
}

interface Server {
  name: string
  status: ServerStatus
  icon: string
  address: string
}

export { ServerStatus, type Server }
