import AuthLayout from '@/components/layouts/auth-layout'
import GuestRouteGuard from '@/features/auth/auth-provider/guest-route-guard'
import SignupForm from '@/features/auth/signup/singup-form'

const Signup = () => {
    return (
        <GuestRouteGuard>
            <AuthLayout>
                <p>Signup</p>
                <SignupForm />
            </AuthLayout>
        </GuestRouteGuard>
    )
}

export default Signup
