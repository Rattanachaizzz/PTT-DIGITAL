import React, { useState } from 'react'
import { login } from '../../funsions/auth';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';

const Login = () => {

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const redirectPage = (role) => {
        console.log("role" + role);
        if (role === 'admin') {
            navigate('/admin/index');
            return;
        }
        navigate('/user/index');
    }

    const [value, setValue] = useState({
        username: "",
        password: "",
    })

    const hendleChange = (e) => {
        setValue({
            ...value,
            [e.target.name]: e.target.value
        });
    }
    const hendleSubmit = (e) => {
        e.preventDefault();
        login(value).then((res) => {
            alert(res.data)

            dispatch({
                type: "LOGIN",
                payload: {
                    token: res.data.token,
                    username: res.data.payload.user.username,
                    role: res.data.payload.user.role
                }
            })
            localStorage.setItem('token', res.data.token)
            redirectPage(res.data.payload.user.role)
        }).catch((error) => {
            alert(error.response.data)
        });
    }
    return (
        <div style={style.body}>
            <div style={style.wrapper}>
                <h1>Login</h1>
                <div className="form-group">
                    <label for="">Username</label>
                    <input type="text" className="form-control" name="username" aria-describedby="helpId" placeholder="Press Enter Username" onChange={hendleChange} />
                </div>
                <div className="form-group">
                    <label for="">Password</label>
                    <input type="text" className="form-control" name="password" aria-describedby="helpId" placeholder="Press Enter Password" onChange={hendleChange} />
                </div>
                <button type="button" className="btn btn-primary w-100" style={style.button} onClick={hendleSubmit}>Login</button>
                <p>Don't have an account yes? <Link to="/register">LinkRegister now.</Link></p>
            </div>
        </div>
    );
}

export default Login

const style = {
    body: { height: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center', backgroundColor: 'blue' },
    wrapper: { width: '500px', backgroundColor: 'white', padding: 50, borderRadius: 15 },
    button: { marginTop: '10px' }
}