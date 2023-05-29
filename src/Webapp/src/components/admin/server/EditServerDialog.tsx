import React, { FC, useEffect, useState } from 'react'
import { PropertyInput } from '@components/common'
import { Optional } from '@models/Types'
import { Modal, ModalFuncProps } from 'antd'

import { ServerTableData } from './ServerTable'

interface EditServerDialogProps extends ModalFuncProps {
  server: ServerTableData | undefined
  isOpen: boolean
  onSave: (server: Omit<Optional<ServerTableData, 'key'>, 'status'>) => void
}

const EditServerDialog: FC<EditServerDialogProps> = ({
  server,
  isOpen,
  onSave,
  ...props
}: EditServerDialogProps) => {
  const [name, setName] = useState<string>('')
  const [address, setAddress] = useState<string>('')

  useEffect(() => {
    setName(server?.name ?? '')
    setAddress(server?.address ?? '')
  }, [server, isOpen])

  const computedTitle = server ? 'Редактировать параметры сервера' : 'Добавить сервер'

  const onOkHandler = (): void => {
    onSave({
      key: server?.key,
      name,
      address,
    })
  }

  const onNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value)
  }

  const onAddressChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setAddress(event.target.value)
  }

  return (
    <Modal title={computedTitle} onOk={onOkHandler} open={isOpen} {...props}>
      <PropertyInput title='Название' value={name} onChange={onNameChange} />
      <PropertyInput title='Адрес' value={address} onChange={onAddressChange} />
    </Modal>
  )
}

export default EditServerDialog
