import React, { FC, useEffect, useState } from 'react'
import { PropertyInput, PropertySelect } from '@components/common'
import { MOCK_SERVERS } from '@models/Server'
import { Optional } from '@models/Types'
import { MOCK_USERS } from '@models/User'
import { Modal, ModalFuncProps } from 'antd'

import { WorkspaceTableData } from './WorkspaceTable'

interface EditWorkspaceDialogProps extends ModalFuncProps {
  workspace: WorkspaceTableData | undefined
  isOpen: boolean
  onSave: (workspace: Optional<WorkspaceTableData, 'key'>) => void
}

const EditWorkspaceDialog: FC<EditWorkspaceDialogProps> = ({
  workspace,
  isOpen,
  onSave,
  ...props
}: EditWorkspaceDialogProps) => {
  const [name, setName] = useState<string>('')
  const [users, setUsers] = useState<string[]>([])
  const [servers, setServers] = useState<string[]>([])

  useEffect(() => {
    
      setName(workspace?.name ?? '')
    
      setUsers(workspace?.users.map((item) => item.id) ?? [])
    
      setServers(workspace?.servers.map((item) => item.id) ?? [])
    
  }, [workspace, isOpen])

  const userOptions = MOCK_USERS.map((user) => ({ value: user.id, label: user.name }))
  const serverOptions = MOCK_SERVERS.map((server) => ({ value: server.id, label: server.name }))

  const computedTitle = workspace
    ? 'Редактировать рабочее пространство'
    : 'Создать рабочее пространство'

  const onOkHandler = (): void => {
    onSave({
      key: workspace?.key,
      name,
      users: MOCK_USERS.filter((item) => users.includes(item.id)),
      servers: MOCK_SERVERS.filter((item) => servers.includes(item.id)),
    })
  }

  const onNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value)
  }
  return (
    <Modal title={computedTitle} onOk={onOkHandler} open={isOpen} {...props}>
      <PropertyInput title='Название' value={name} onChange={onNameChange} />
      <PropertySelect
        title='Пользователи'
        options={userOptions}
        mode='multiple'
        value={users}
        onChange={setUsers}
        allowClear
      />
      <PropertySelect
        title='Серверы'
        options={serverOptions}
        mode='multiple'
        value={servers}
        onChange={setServers}
        allowClear
      />
    </Modal>
  )
}

export default EditWorkspaceDialog
