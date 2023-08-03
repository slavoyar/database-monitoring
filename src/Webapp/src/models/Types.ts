export type Optional<T, K extends keyof T> = Pick<Partial<T>, K> & Omit<T, K>

export type ValueWithLabel = { value: string, label: string }