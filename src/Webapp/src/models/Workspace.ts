import { Server, ServerId } from './Server';
import { User, UserId } from './User';

type WorkspaceId = string

interface Workspace {
  id?: WorkspaceId;
  name: string;
  description?: string;
  users: UserId[];
  servers: ServerId[];
}

type WorkspaceTableData
  = Omit<Workspace, 'users' | 'servers'>
  & { users: User[], servers: Server[] };

const workspaceToTableData
  = (workspace: Workspace, users: User[], servers: Server[]): WorkspaceTableData => ({
    ...workspace,
    users: users.filter(user => workspace.users.includes(user.id)),
    servers: servers.filter(server => workspace.servers.includes(server.id)),
  });

const workspacesToTableData = (
  workspaces: Workspace[],
  users: User[],
  servers: Server[],
): WorkspaceTableData[] => workspaces.map(w => workspaceToTableData(w, users, servers));

const tableDataToWorkspace
  = (data: WorkspaceTableData): Workspace => ({
    ...data,
    users: data.users.map(user => user.id),
    servers: data.servers.map(server => server.id),
  });

export {
  type Workspace,
  type WorkspaceId,
  type WorkspaceTableData,
  workspacesToTableData,
  tableDataToWorkspace,
};
