import { FC } from 'react';
import { ServerShort } from '@models';

import ServerCard from './ServerCard';

interface CardsProps {
  servers: ServerShort[]
}

const Cards: FC<CardsProps> = ({ servers }: CardsProps) => (
  <>
    {servers.map((server) => (
      <ServerCard {...server} key={server.id} />
    ))}
  </>
);

export default Cards;
