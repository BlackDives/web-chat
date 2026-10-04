import { useState, createContext, useEffect } from 'react'
import axios from 'axios'

export type AuthProviderProps = {
    children: React.ReactNode
}

export type AuthContextType = {
    user: AuthenticatedUser | null
    status: AuthStatus
}

export type AuthenticatedUser = {
    id: string
    username: string
    email: string
}

export type AuthStatus = 'loading' | 'authenticated' | 'unauthenticated'

export const AuthContext = createContext<AuthContextType | undefined>(undefined)

const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<AuthenticatedUser | null>(null)
    const [status, setStatus] = useState<AuthStatus>('loading')

    useEffect(() => {
        axios
            .get('http://localhost:5003/api/users/me', {
                withCredentials: true,
            })
            .then((res) => {
                const data: AuthenticatedUser = res.data
                setUser(data)
                setStatus('authenticated')
            })
            .catch((err) => {
                console.log(err)
                if (axios.isAxiosError(err)) {
                    if (err.response?.status === 401) {
                        setUser(null)
                        setStatus('unauthenticated')
                    }
                }
            })
    }, [])

    return (
        <AuthContext.Provider value={{ user, status }}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthProvider
