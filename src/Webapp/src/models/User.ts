type UserId = string

const enum UserRole {
  ADMIN = 'admin',
  ENGINEER = 'engineer',
  MANAGER = 'manager',
}

interface User {
  id: UserId;
  firstName: string;
  secondName: string;
  role: UserRole;
  email: string;
  phone: string;
  description: string;
}

export { User, UserId, UserRole }