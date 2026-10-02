import AuthLayout from '@/components/layouts/auth-layout'
import LoginForm from '@/features/auth/login/login-form'

const Login = () => {
    return (
        <AuthLayout title="login">
            <LoginForm />
        </AuthLayout>
    )
}

export default Login
