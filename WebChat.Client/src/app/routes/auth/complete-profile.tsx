import axios from 'axios'

const CompleteProfile = () => {
    async function submit() {
        try {
            const completeProfile = await axios.post(
                'http:/localhost:5003/auth/complete-profile'
            )
        } catch (error) {}
    }
    return (
        <div className="h-[100dvh]">
            <div>Complete your profile!</div>
            <div className="flex flex-col">
                <form onSubmit={submit}>
                    <div className="flex flex col">
                        <label>Username</label>
                        <input className="border-2 border-black" />
                    </div>
                    <div className="flex flex col">
                        <label>First Name</label>
                        <input className="border-2 border-black" />
                    </div>
                    <div className="flex flex col">
                        <label>Last Name</label>
                        <input className="border-2 border-black" />
                    </div>
                    <div>
                        <button className="bg-amber-400">
                            Complete Profile
                        </button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default CompleteProfile
