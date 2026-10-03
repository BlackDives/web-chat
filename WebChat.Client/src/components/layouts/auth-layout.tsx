type AuthLayoutProps = {
    children: React.ReactNode
}

const AuthLayout = ({ children }: AuthLayoutProps) => {
    return (
        <div className="w-full h-[100dvh]">
            <div className="max-w-7xl h-full px-8 py-6 flex flex-col mx-auto">
                {children}
            </div>
        </div>
    )
}

export default AuthLayout
