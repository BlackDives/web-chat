import AuthLayout from '@/components/layouts/auth-layout'
import GuestRouteGuard from '@/features/auth/auth-provider/guest-route-guard'
import LoginForm from '@/features/auth/login/login-form'

const Login = () => {
    return (
        <GuestRouteGuard>
            <AuthLayout>
                <p>Login</p>
                <LoginForm />
            </AuthLayout>
        </GuestRouteGuard>
    )
}

export default Login
