import React, { FC, useEffect, useState } from 'react'
import { PropertyInput } from '@components/common'
import { Optional } from '@models/Types'
import { Modal, ModalFuncProps } from 'antd'

import { UserTableData } from './UserTable'

interface EditUserDialogProps extends ModalFuncProps {
  user: UserTableData | undefined
  isOpen: boolean
  onSave: (user: Optional<UserTableData, 'key'>) => void
}

const EditUserDialog: FC<EditUserDialogProps> = ({
  user,
  isOpen,
  onSave,
  ...props
}: EditUserDialogProps) => {
  const [name, setName] = useState<string>('')
  const [email, setEmail] = useState<string>('')
  const [phone, setPhone] = useState<string>('')

  useEffect(() => {
    setName(user?.name ?? '')
    setEmail(user?.email ?? '')
    setPhone(user?.phone ?? '')
  }, [user, isOpen])

  const computedTitle = user ? 'Редактировать пользователя' : 'Создать пользователя'

  const onOkHandler = (): void => {
    onSave({
      key: user?.key,
      name,
      phone,
      email,
    })
  }

  const onNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value)
  }

  const onEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value)
  }

  const onPhoneChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPhone(event.target.value)
  }

  return (
    <Modal title={computedTitle} onOk={onOkHandler} open={isOpen} {...props}>
      <PropertyInput title='Имя' value={name} onChange={onNameChange} />
      <PropertyInput title='Email' value={email} onChange={onEmailChange} />
      <PropertyInput title='Телефон' value={phone} onChange={onPhoneChange} />
    </Modal>
  )
}

export default EditUserDialog
