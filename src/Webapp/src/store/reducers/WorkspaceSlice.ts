import { Server, User, Workspace } from '@models'
import { createSlice, PayloadAction } from '@reduxjs/toolkit'

import { getWorkspace } from './WorkspaceActionCreators'

interface WorkspaceState {
  name?: string
  users: User[]
  servers: Server[]
  isLoading: boolean
  error?: string
}

const initialState: WorkspaceState = {
  users: [],
  servers: [],
  isLoading: false,
}

const WORKSPACE = 'workspace'

export const workspaceSlice = createSlice({
  name: WORKSPACE,
  initialState,
  reducers: {},
  extraReducers: {
    [getWorkspace.fulfilled.type]: (state: WorkspaceState, action: PayloadAction<Workspace>) => {
      const { servers, users, name } = action.payload
      state.name = name
      state.servers = servers
      state.users = users
      state.isLoading = false
    },
    [getWorkspace.pending.type]: (state: WorkspaceState) => {
      state.isLoading = true
    },
    [getWorkspace.rejected.type]: (state: WorkspaceState, action: PayloadAction<Workspace>) => {
      state.error = action.payload
      state.isLoading = false
    },
  },
})
