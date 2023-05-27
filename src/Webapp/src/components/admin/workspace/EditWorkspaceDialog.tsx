import React, { FC, useState } from 'react'
import { PropertyInput, PropertySelect } from '@components/common'
import { MOCK_SERVERS } from '@models/Server'
import { MOCK_USERS } from '@models/User'
import { Modal, ModalFuncProps } from 'antd'

import { WorkspaceTableData } from './WorkspaceTable'

interface EditWorkspaceDialogProps extends ModalFuncProps {
  workspace: WorkspaceTableData | undefined
  onSave: (workspace: Omit<WorkspaceTableData, 'key'>) => void
}

const EditWorkspaceDialog: FC<EditWorkspaceDialogProps> = ({
  workspace,
  onSave,
  ...props
}: EditWorkspaceDialogProps) => {
  const [name, setName] = useState<string>(workspace ? workspace.name : '')
  const [users, setUsers] = useState<string[]>([])
  const [servers, setServers] = useState<string[]>([])

  const userOptions = MOCK_USERS.map((user) => ({ value: user.id, label: user.name }))
  const serverOptions = MOCK_SERVERS.map((server) => ({ value: server.id, label: server.name }))

  const computedTitle = workspace
    ? 'Редактировать рабочее пространство'
    : 'Создать рабочее пространство'

  const onOkHandler = (): void => {
    onSave({ name, users: users.join(', '), servers: servers.join(', ') })
  }

  const onNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value)
  }
  return (
    <Modal title={computedTitle} onOk={onOkHandler} {...props}>
      <PropertyInput title='Название' value={name} onChange={onNameChange} />
      <PropertySelect
        title='Пользователи'
        options={userOptions}
        mode='multiple'
        value={users}
        onChange={setUsers}
      />
      <PropertySelect
        title='Серверы'
        options={serverOptions}
        mode='multiple'
        value={servers}
        onChange={setServers}
      />
    </Modal>
  )
}

export default EditWorkspaceDialog
