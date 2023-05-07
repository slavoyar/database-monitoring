/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable class-methods-use-this */
import { Workspace, WorkspaceId } from '@models';

import * as Workspaces from './mock/Workspace.json'

class WorkspaceService {
  public getWorkspaces(): Workspace[] {
    return Workspaces;
  }

  public createWorkspace(workspace: Workspace): WorkspaceId {
    throw new Error('Not implemented')
  }

  public updateWorkspace(workspace: Workspace): void {
    throw new Error('Not implemented')
  }

  public deleteWorkspace(id: WorkspaceId): boolean {
    throw new Error('Not implemented')
  }
}

export default WorkspaceService;