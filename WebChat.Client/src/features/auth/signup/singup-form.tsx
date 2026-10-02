import { Button } from '@/components/ui/button'
import { Card, CardHeader } from '@/components/ui/card'

const SignupForm = () => {
    const AUTH_BASE_URL = 'https://accounts.google.com/o/oauth2/v2/auth'

    const initiateGoogleSSO = async () => {
        window.location.href = `${AUTH_BASE_URL}?response_type=code&client_id=511948095605-4ruqsfgbl3pug6pcc1mboq5dhrlc60h3.apps.googleusercontent.com&scope=openid%20email&redirect_uri=http://localhost:5003/api/auth/google/signup&state=security_token%3D138r5719ru3e1%26url%3Dhttps%3A%2F%2Foauth2-login-demo.example.com%2FmyHome`
    }

    return (
        <form>
            <Card>
                <CardHeader>
                    <Button type="button" onClick={initiateGoogleSSO}>
                        Sign up with Google
                    </Button>
                </CardHeader>
            </Card>
        </form>
    )
}

export default SignupForm
