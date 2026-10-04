import { useEffect } from 'react'
import { useNavigate } from 'react-router'
import { useAuth } from '@/hooks/use-auth'

export type GuestRouteGuardProps = {
    children: React.ReactNode
}

const GuestRouteGuard = ({ children }: GuestRouteGuardProps) => {
    const { status } = useAuth()
    const navigate = useNavigate()

    useEffect(() => {
        if (status === 'authenticated') {
            navigate('/home')
        }
    }, [status])

    return status === 'unauthenticated' && <>{children}</>
}

export default GuestRouteGuard
