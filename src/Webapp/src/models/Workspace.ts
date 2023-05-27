import { MOCK_SERVERS, Server } from './Server'
import { MOCK_USERS, User } from './User'

type WorkspaceId = string

interface Workspace {
  id: WorkspaceId
  name: string
  users: User[]
  servers: Server[]
}

export const MOCK_WORKSPACES = [
  {
    id: 'workspace1',
    name: 'Workspace 1',
    users: MOCK_USERS,
    servers: [MOCK_SERVERS.pop()] as Server[],
  },
  {
    id: 'workspace2',
    name: 'Workspace 2',
    users: [MOCK_USERS.pop()] as User[],
    servers: MOCK_SERVERS,
  },
] as Workspace[]

export { type Workspace, type WorkspaceId }
