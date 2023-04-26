import React, { FC } from 'react'
import { Layout } from 'antd'
import { Server,ServerStatus } from 'models'

import { Navbar } from 'components/common/'
import { Cards } from 'components/dashboard/'

import 'css/Dashboard.css'

const SERVERS_MOCK_DATA: Server[] = [
  {
    name: 'Server 1',
    icon: 'android',
    status: ServerStatus.good,
    address: '192.168.1.1',
  },
  {
    name: 'Server 2',
    icon: 'apple',
    status: ServerStatus.warn,
    address: '192.168.1.1',
  },
  {
    name: 'Server 3',
    icon: 'windows',
    status: ServerStatus.down,
    address: '192.168.1.1',
  },
  {
    name: 'Server 4',
    icon: 'android',
    status: ServerStatus.good,
    address: '192.168.1.1',
  },
  {
    name: 'Server 5',
    icon: 'apple',
    status: ServerStatus.warn,
    address: '192.168.1.1',
  },
  {
    name: 'Server 6',
    icon: 'windows',
    status: ServerStatus.down,
    address: '192.168.1.1',
  },
  {
    name: 'Server 7',
    icon: 'android',
    status: ServerStatus.good,
    address: '192.168.1.1',
  },
  {
    name: 'Server 8',
    icon: 'apple',
    status: ServerStatus.warn,
    address: '192.168.1.1',
  },
  {
    name: 'Server 9',
    icon: 'windows',
    status: ServerStatus.down,
    address: '192.168.1.1',
  },
]

const Dashboard: FC = () => (
  <Layout>
    <Navbar />
    <Layout.Content className='dashboard-content'>
      <div className='card-container'>
        <Cards servers={SERVERS_MOCK_DATA} />
      </div>
    </Layout.Content>
  </Layout>
)

export default Dashboard
