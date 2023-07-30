import Role from './Role';

type UserId = string

interface User {
  id: UserId
  fullUserName: string
  email: string
  phoneNumber?: string
  role: Role
  password?: string
}

export const MOCK_USERS = [
  {
    id: 'user1',
    fullUserName: 'User 1',
    email: 'user@mail.ru',
    phoneNumber: '+71234567890',
  },
  {
    id: 'user2',
    fullUserName: 'User 2',
    email: 'user@mail.ru',
    phoneNumber: '+71234567890',
  },
  {
    id: 'user3',
    fullUserName: 'User 3',
    email: 'user@mail.ru',
    phoneNumber: '+71234567890',
  },
] as User[];

export { type User, type UserId };
