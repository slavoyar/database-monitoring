import { Server } from './Server';
import User from './User';

type WorkspaceId = string

interface Workspace {
  id: WorkspaceId;
  name: string;
  servers: Server[];
  users: User[];
}

export { Workspace, WorkspaceId }