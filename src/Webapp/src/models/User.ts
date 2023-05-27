import { WorkspaceId } from './Workspace'

type UserId = string

interface User {
  id: UserId
  name: string
  email: string
  phone?: string
  workspaces: WorkspaceId[]
}

export {type User, type UserId}