import { useEffect } from 'react'
import { useNavigate } from 'react-router'
import { useAuth } from '@/hooks/use-auth'

export type AuthenticatedRouteGuardProps = {
    children: React.ReactNode
}

const AuthenticatedRouteGuard = ({
    children,
}: AuthenticatedRouteGuardProps) => {
    const { status } = useAuth()
    const navigate = useNavigate()

    useEffect(() => {
        if (status === 'unauthenticated') {
            navigate('/login')
        }
    }, [status])

    return status === 'authenticated' && <>{children}</>
}

export default AuthenticatedRouteGuard
