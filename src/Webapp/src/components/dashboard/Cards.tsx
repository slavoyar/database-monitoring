import React, { FC } from 'react'

import { Server } from 'models'
import ServerCard from './ServerCard'

interface CardsProps {
  servers: Server[]
}

const Cards: FC<CardsProps> = ({ servers }: CardsProps) => (
  <>
    {servers.map((server) => (
      <ServerCard {...server} key={server.name} />
    ))}
  </>
)

export default Cards
