import { useState} from 'react';
import { useLocation } from 'react-router-dom';

const ResetPassword = () => {
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');
    const location = useLocation();

    const queryParams = new URLSearchParams(location.search);
    const token = queryParams.get('token');

    const handleSubmit = async () => {
        if (password !== confirmPassword) {
            setError('Passwords do not match.');
            return;
        }

        try {
            const PasswordResetModel = {
                Token: token,
                NewPassword: password
            }
            const response = await fetch('http://localhost:5041/api/reset-password', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(PasswordResetModel),
            });

            const result = await response.json();
            if (response.ok) {
                alert('Password reset successfully');
            } else {
                setError(result.message);
            }
        } catch (error) {
            setError('Error resetting password.');
        }
    };

    return (
        <div>
            <h3>Reset your password</h3>
            <input
                type="password"
                placeholder="New password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <input
                type="password"
                placeholder="Confirm new password"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
            />
            {error && <p>{error}</p>}
            <button onClick={handleSubmit}>Reset Password</button>
        </div>
    );
};

export default ResetPassword;
