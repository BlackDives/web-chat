import AuthLayout from '@/components/layouts/auth-layout'
import { Button } from '@/components/ui/button'
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from '@/components/ui/card'
import {
    Field,
    FieldError,
    FieldGroup,
    FieldLabel,
} from '@/components/ui/field'
import { Input } from '@/components/ui/input'
import { zodResolver } from '@hookform/resolvers/zod'
import axios from 'axios'
import { Controller, useForm } from 'react-hook-form'
import { useNavigate } from 'react-router'
import z from 'zod'

const completeProfileSchema = z.object({
    username: z
        .string()
        .min(4, 'Username must be atleast 4 characters.')
        .max(25, 'Username can be no longer than 20 characters.'),
    firstName: z.string().min(2, 'Not a valid name').max(30, 'Name too long'),
    lastName: z.string().min(2, 'Not a valid name').max(30, 'Name too long'),
})

const CompleteProfile = () => {
    const navigate = useNavigate()
    const form = useForm<z.infer<typeof completeProfileSchema>>({
        resolver: zodResolver(completeProfileSchema),
        defaultValues: {
            username: '',
            firstName: '',
            lastName: '',
        },
    })
    async function onFormSubmit(data: z.infer<typeof completeProfileSchema>) {
        const { username, firstName, lastName } = data

        try {
            const completeProfile = await axios.post(
                'http://localhost:5003/api/auth/complete-profile',
                {
                    username,
                    firstName,
                    lastName,
                },
                {
                    withCredentials: true,
                }
            )

            navigate('/')
        } catch (error) {}
    }
    return (
        <AuthLayout>
            <div className="w-full h-full flex flex-col">
                <div className="flex flex-col">
                    <form
                        id="complete-profile-form"
                        onSubmit={form.handleSubmit(onFormSubmit)}
                    >
                        <Card>
                            <CardHeader>
                                <CardTitle>Complete your profile!</CardTitle>
                                <CardDescription>
                                    Fill out the fields to complete your WebChat
                                    account creation.
                                </CardDescription>
                            </CardHeader>
                            <CardContent>
                                <FieldGroup>
                                    <Controller
                                        name="username"
                                        control={form.control}
                                        render={({ field, fieldState }) => (
                                            <Field>
                                                <FieldLabel>
                                                    Username
                                                </FieldLabel>
                                                <Input
                                                    {...field}
                                                    aria-invalid={
                                                        fieldState.invalid
                                                    }
                                                    placeholder="Enter username..."
                                                    autoComplete="off"
                                                />
                                                {fieldState.invalid && (
                                                    <FieldError
                                                        errors={[
                                                            fieldState.error,
                                                        ]}
                                                    />
                                                )}
                                            </Field>
                                        )}
                                    />
                                    <Controller
                                        name="firstName"
                                        control={form.control}
                                        render={({ field, fieldState }) => (
                                            <Field>
                                                <FieldLabel>
                                                    First Name
                                                </FieldLabel>
                                                <Input
                                                    {...field}
                                                    aria-invalid={
                                                        fieldState.invalid
                                                    }
                                                    placeholder="Enter first name..."
                                                    autoComplete="off"
                                                />
                                                {fieldState.invalid && (
                                                    <FieldError
                                                        errors={[
                                                            fieldState.error,
                                                        ]}
                                                    />
                                                )}
                                            </Field>
                                        )}
                                    />
                                    <Controller
                                        name="lastName"
                                        control={form.control}
                                        render={({ field, fieldState }) => (
                                            <Field>
                                                <FieldLabel>
                                                    Last Name
                                                </FieldLabel>
                                                <Input
                                                    {...field}
                                                    aria-invalid={
                                                        fieldState.invalid
                                                    }
                                                    placeholder="Enter last name..."
                                                    autoComplete="off"
                                                />
                                                {fieldState.invalid && (
                                                    <FieldError
                                                        errors={[
                                                            fieldState.error,
                                                        ]}
                                                    />
                                                )}
                                            </Field>
                                        )}
                                    />
                                </FieldGroup>
                                <div>
                                    <Button
                                        type="submit"
                                        form="complete-profile-form"
                                    >
                                        Complete Profile
                                    </Button>
                                </div>
                            </CardContent>
                        </Card>
                    </form>
                </div>
            </div>
        </AuthLayout>
    )
}

export default CompleteProfile
