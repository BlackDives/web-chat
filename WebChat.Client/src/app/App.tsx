import { useState, useEffect, useContext } from 'react'
import api from '../old_stuff/api/AxiosInstance'
import { Outlet, Link } from 'react-router'
import { House } from 'lucide-react'
import { Button } from '../components/ui/button'
import { AuthenticationContext } from '../old_stuff/providers/auth/AuthProvider'
import { Tooltip, TooltipTrigger } from '../components/ui/tooltip'
import { TooltipContent } from '@radix-ui/react-tooltip'
import AuthProvider from '@/features/auth/auth-provider/auth-provider'

type ServerPreview = {
    id: string
    name: string
}

function App() {
    const { user, token, logout } = useContext(AuthenticationContext)
    const [servers, setServers] = useState<ServerPreview[]>([])

    useEffect(() => {
        getAllUserServers()
    }, [])

    const getAllUserServers = async () => {
        if (!user) {
            return
        }
        try {
            console.log('running this shit')
            const results = await api.get(`/v1/users/${user.id}/servers`, {
                headers: { Authorization: `Bearer ${token}` },
            })

            setServers(results.data)
        } catch (error) {
            console.log(error)
        }
    }

    return (
        <AuthProvider>
            <Outlet />
        </AuthProvider>
    )
}

export default App
