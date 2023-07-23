import { FC } from 'react'
import { Navbar } from '@components/common'
import { Layout, Result } from 'antd'


const NotFound: FC = () => (
    <Layout>
        <Navbar />
        <Layout.Content className='dashboard-content'>
            <Result
                status="404"
                title="404"
                subTitle="Страницы с таким путем не существует"
            />
        </Layout.Content>
    </Layout>

)

export default NotFound