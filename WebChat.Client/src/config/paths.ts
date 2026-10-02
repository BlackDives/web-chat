export const paths = {
    app: {
        path: '/',
        getHref: () => '/',
        home: {
            path: '/home',
            gerHref: () => '/home',
        },
        space: {
            path: '/space',
            getHref: '/space',
            channel: {
                href: ':channelId',
                getHref: (channelId: string) => channelId,
            },
        },
    },
    auth: {
        root: {
            path: '/auth',
            getHref: () => '/auth',
        },
        login: {
            path: 'login',
            getHref: () => '/login',
        },
        signup: {
            path: 'signup',
            getHref: () => '/signup',
        },
        completeProfile: {
            path: 'complete-profile',
            getHref: () => '/complete-profile',
        },
    },
}
