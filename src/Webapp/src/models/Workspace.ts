import { MOCK_SERVERS, ServerId } from './Server';
import { MOCK_USERS, UserId } from './User';

type WorkspaceId = string

interface Workspace {
  id?: WorkspaceId;
  name: string;
  description?: string;
  users: UserId[];
  servers: ServerId[];
}

export const MOCK_WORKSPACES = [
  {
    id: 'workspace1',
    name: 'Workspace 1',
    users: MOCK_USERS.map(item => item.id),
    servers: MOCK_SERVERS.filter((_, index) => index % 2).map(item => item.id),
  },
  {
    id: 'workspace2',
    name: 'Workspace 2',
    users: MOCK_USERS.filter((_, index) => index % 2).map(item => item.id),
    servers: MOCK_SERVERS.map(item => item.id),
  },
] as Workspace[];

export { type Workspace, type WorkspaceId };
