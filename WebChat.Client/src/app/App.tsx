import AuthProvider from '@/features/auth/auth-provider/auth-provider'
import { Outlet } from 'react-router'

function App() {
    return (
        <AuthProvider>
            <Outlet />
        </AuthProvider>
    )
}

export default App
