import AuthenticatedRouteGuard from '@/features/auth/auth-provider/authenticated-route-guard'

const Home = () => {
    return (
        <AuthenticatedRouteGuard>
            <div className="flex flex-col">webchat home</div>
        </AuthenticatedRouteGuard>
    )
}

export default Home
