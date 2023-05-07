type UserId = string

interface User {
  id: UserId;
  firstName: string;
  secondName: string;
  email: string;
  phone: string;
  description: string;
}

export { User, UserId }