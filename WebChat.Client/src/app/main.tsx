import { createRoot } from 'react-dom/client'
import { createBrowserRouter } from 'react-router'
import { RouterProvider } from 'react-router/dom'
import App from './App.tsx'
import { paths } from '@/config/paths.ts'
import Login from './routes/auth/login.tsx'
import Signup from './routes/auth/signup.tsx'
import './index.css'
import CompleteProfile from './routes/auth/complete-profile.tsx'

const router = createBrowserRouter([
    {
        path: paths.app.path,
        Component: App,
        children: [
            {
                path: paths.auth.root.path,
                children: [
                    {
                        path: paths.auth.login.path,
                        Component: Login,
                    },
                    {
                        path: paths.auth.signup.path,
                        Component: Signup,
                    },
                    {
                        path: paths.auth.completeProfile.path,
                        Component: CompleteProfile,
                    },
                ],
            },
        ],
    },
])

createRoot(document.getElementById('root')!).render(
    <RouterProvider router={router} />
)
