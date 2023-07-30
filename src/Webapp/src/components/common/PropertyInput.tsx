import { FC } from 'react';
import { Input, InputProps } from 'antd';

interface PropertyInputProps extends InputProps {
  title: string
}

const PropertyInput: FC<PropertyInputProps> = ({ title, ...props }: PropertyInputProps) => (
  <div className='property'>
    <strong>{title}</strong>
    <Input {...props} />
  </div>
);

export default PropertyInput;
