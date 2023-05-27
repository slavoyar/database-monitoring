import { FC } from 'react'
import { Select, SelectProps } from 'antd'

interface PropertySelectProps extends SelectProps {
  title: string
}

const PropertySelect: FC<PropertySelectProps> = ({ title, ...props }: PropertySelectProps) => (
  <div className='property'>
    <strong>{title}</strong>
    <Select style={{ display: 'block' }} {...props} />
  </div>
)

export default PropertySelect
