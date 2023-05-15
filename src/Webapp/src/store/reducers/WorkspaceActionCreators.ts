import { Workspace, WorkspaceId } from '@models'
import { createAsyncThunk } from '@reduxjs/toolkit'
// eslint-disable-next-line import/no-extraneous-dependencies
import axios from 'axios'

export const enum WorkspaceThunk {
  getWorkspace = 'getWorkspace',
  createWorkspace = 'createWorkspace',
  deleteWorkspace = 'deleteWorkspace',
  addServer = 'addServer',
  addUser = 'addUser',
}

const API_URL = 'http://localhost:8080'
const WORKSPACE_URL = `${API_URL}/workspace`

export const getWorkspace = createAsyncThunk<Workspace>(
  WorkspaceThunk.getWorkspace,
  async (workspaceId: WorkspaceId, thunkAPI): Promise<Workspace> => {
    try {
      const response = await axios.get<Workspace>(
        `${WORKSPACE_URL}/${WorkspaceThunk.getWorkspace}`,
        {
          data: workspaceId,
        },
      )
      return response.data
    } catch (e) {
      return thunkAPI.rejectWithValue('Workspace fetching error')
    }
  },
)
