import { useState } from 'react'

type AuthLayoutProps = {
    children: React.ReactNode
    title: string
}

const AuthLayout = ({ children, title }: AuthLayoutProps) => {
    return (
        <div>
            <p>{title}</p>
            <div>{children}</div>
        </div>
    )
}

export default AuthLayout
