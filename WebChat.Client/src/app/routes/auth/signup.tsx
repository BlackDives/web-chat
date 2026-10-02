import AuthLayout from '@/components/layouts/auth-layout'
import SignupForm from '@/features/auth/signup/singup-form'

const Signup = () => {
    return (
        <AuthLayout title="register">
            <SignupForm />
        </AuthLayout>
    )
}

export default Signup
