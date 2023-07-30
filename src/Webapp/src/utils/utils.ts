import { DefaultOptionType } from 'antd/es/select';

// eslint-disable-next-line import/prefer-default-export
export function arrayToOptions<T extends string>(values: T[]): DefaultOptionType[] {
    return values.map(value => ({ value, label: value }));
}
