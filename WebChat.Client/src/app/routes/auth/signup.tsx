import AuthLayout from '@/components/layouts/auth-layout'
import SignupForm from '@/features/auth/signup/singup-form'

const Signup = () => {
    return (
        <AuthLayout>
            <p>Signup</p>
            <SignupForm />
        </AuthLayout>
    )
}

export default Signup
