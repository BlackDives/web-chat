import { useState, createContext, useEffect } from 'react'
import { useNavigate } from 'react-router'
import axios from 'axios'

export type AuthProviderProps = {
    children: React.ReactNode
}

export type AuthContextType = {
    isAuthenticated: boolean
    accessToken: string
}

export type AuthenticatedUser = {
    username: string
    email: string
}

const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<AuthenticatedUser | null>(null)
    const [isAuthenticated, setIsAuthenticated] = useState(false)
    const navigate = useNavigate()

    const AuthContext = createContext<AuthContextType>({
        isAuthenticated: false,
        accessToken: '',
    })

    useEffect(() => {}, [])

    const fetchUserDetails = async () => {
        try {
            const response = await axios.get('http://localhost:5003/user/me')

            return response
        } catch (error) {}
    }

    return <>{children}</>
}

export default AuthProvider
