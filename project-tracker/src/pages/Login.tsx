import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';


export const Login = () => {
    const [typedUser,setUser] = useState('');
    const [typedPass,setPass] = useState('');
    const navigate = useNavigate();

    

    const verifyUserLogin = async() => {
        
        const UserLogin = {
            Username: typedUser,
            Password: typedPass
        };

        console.log("Sending login:", UserLogin);
    
        try {
            const response = await fetch ('http://localhost:5041/api/login',{
                method: 'POST',
                headers:{
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(UserLogin),
            });
            const result = await response.json();
            if(response.ok){
                navigate('/projects');  
            }
            else{
                alert(result.message);
            }

            
        } catch (error) {
            
        }
        

    }

  return (
    <div>
        <h1>Welcome to Ton's Project Tracker</h1>
        <div className='userInfo'>
        <div className='userName'>
            <p>User name:</p>
            <input type='String' placeholder='Enter your username' onChange={(e) => setUser(e.target.value)}></input>
        </div>

        <div className='pass'>
            <p>Password:</p>
            <input type='String' placeholder='Enter your password' onChange={(e) => setPass(e.target.value)}></input>
        </div>

        <button onClick={() => verifyUserLogin()} style={{margin:'10px'}}>
            Sign In
        </button>
        <div>
        <Link to="/register">Register your account</Link>

        </div>
        <div>
            <Link to = "/forgotUsername">Forgor your Username?</Link>
        </div>
        <div>
            <Link to = "/resetPass">Forgor your password?</Link>
        </div>
        


        </div>
        
        
        
    </div>
  )
}
