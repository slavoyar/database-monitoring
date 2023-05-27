import { Server } from './Server';
import { User } from './User'

type WorkspaceId = string;

interface Workspace {
  id: WorkspaceId
  name: string
  users: User[]
  servers: Server[]
}

export {type Workspace, type WorkspaceId}