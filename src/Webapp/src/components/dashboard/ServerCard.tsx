import React, { FC } from 'react'
import { CheckCircleFilled, CloseCircleFilled, WarningFilled } from '@ant-design/icons'
import { Server, ServerStatus } from '@models'
import { Card } from 'antd'

import '@css/ServerCard.css'

type StatusIconProps = Pick<Server, 'status'>

const getStatusIcon: FC<StatusIconProps> = ({ status }: StatusIconProps) => {
  let icon
  switch (status) {
    case ServerStatus.good:
      icon = <CheckCircleFilled style={{ color: '#52c41a' }} />
      break
    case ServerStatus.warn:
      icon = <WarningFilled style={{ color: '#faad14' }} />
      break
    case ServerStatus.down:
    default:
      icon = <CloseCircleFilled style={{ color: '#ff4d4f' }} />
      break
  }
  return icon
}

const ServerCard: FC<Server> = ({ name, status, address }: Server) => {
  const iconComponent = getStatusIcon({ status })
  return (
    <Card title={name} extra={iconComponent} className='server-card'>
      <span>
        <strong>IP-address: </strong>
        {address}
      </span>
    </Card>
  )
}

export default ServerCard
