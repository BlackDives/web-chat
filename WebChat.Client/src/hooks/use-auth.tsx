import {
    AuthContext,
    type AuthContextType,
} from '@/features/auth/auth-provider/auth-provider'
import { useContext } from 'react'

export function useAuth(): AuthContextType {
    const context = useContext(AuthContext)

    if (context === undefined) {
        throw new Error('useAuth must be used within an AuthProvider')
    }

    return context
}
