import axios from 'axios'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'

export type CompleteProfileGuarProps = {
    children: React.ReactNode
}

const CompleteProfileGuard = ({ children }: CompleteProfileGuarProps) => {
    const [completingProfile, setIsCompletingProfile] = useState<boolean>(false)
    const navigate = useNavigate()

    useEffect(() => {
        axios
            .get('http://localhost:5003/api/users/complete-profile', {
                withCredentials: true,
            })
            .then((res) => {
                console.log(res)
                setIsCompletingProfile(true)
            })
            .catch((error) => {
                console.log(error)
                if (axios.isAxiosError(error)) {
                    if (error.response?.status === 401) {
                        setIsCompletingProfile(false)
                        navigate('/auth/login')
                    }
                }
            })
    }, [])
    return <>{children}</>
}

export default CompleteProfileGuard
