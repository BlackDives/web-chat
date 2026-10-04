import { useState, createContext, useEffect } from 'react'
import { useNavigate } from 'react-router'
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

const AuthContext = createContext<AuthContextType | undefined>(undefined)

const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<AuthenticatedUser | null>(null)
    const [status, setStatus] = useState<AuthStatus>('loading')
    const navigate = useNavigate()

    useEffect(() => {
        axios
            .get('http://localhost:5003/api/users/me', {
                withCredentials: true,
            })
            .then((res) => {
                console.log(res)
                const data: AuthenticatedUser = res.data
                setUser(data)
            })
            .catch((err) => {
                console.log(err)
                if (axios.isAxiosError(err)) {
                    if (err.response?.status === 401) {
                        setUser(null)
                        setStatus('authenticated')
                        navigate('/auth/login')
                    }
                }
            })
    }, [])

    return (
        <AuthContext.Provider value={{ user, status }}>
            {status == 'authenticated' && <>{children}</>}
        </AuthContext.Provider>
    )
}

export default AuthProvider
