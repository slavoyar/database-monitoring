import { FC } from 'react'
import { Select, SelectProps } from 'antd'

interface PropertySelectProps extends SelectProps {
  title: string
}

const PropertySelect: FC<PropertySelectProps> = ({ title, ...props }: PropertySelectProps) => (
  <>
    <strong>{{ title }}</strong>
    <Select {...props} />
  </>
)

export default PropertySelect
