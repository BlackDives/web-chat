import AuthProvider from '@/features/auth/auth-provider/auth-provider'

const Home = () => {
    return (
        <AuthProvider>
            <div>webchat home</div>
        </AuthProvider>
    )
}

export default Home
