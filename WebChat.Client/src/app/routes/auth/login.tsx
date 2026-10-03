import AuthLayout from '@/components/layouts/auth-layout'
import LoginForm from '@/features/auth/login/login-form'

const Login = () => {
    return (
        <AuthLayout>
            <p>Login</p>
            <LoginForm />
        </AuthLayout>
    )
}

export default Login
