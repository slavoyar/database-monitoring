import Role from './Role'

type UserId = string

interface User {
  id: UserId
  name: string
  email: string
  phone?: string
  role: Role
  password?: string
}

export const MOCK_USERS = [
  {
    id: 'user1',
    name: 'User 1',
    email: 'user@mail.ru',
    phone: '+71234567890',
  },
  {
    id: 'user2',
    name: 'User 2',
    email: 'user@mail.ru',
    phone: '+71234567890',
  },
  {
    id: 'user3',
    name: 'User 3',
    email: 'user@mail.ru',
    phone: '+71234567890',
  },
] as User[]

export { type User, type UserId }
