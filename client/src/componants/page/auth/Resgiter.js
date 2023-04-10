import React, { useState } from 'react'
import { resgiter } from '../../funsions/auth';
import { Link } from 'react-router-dom';

const Resgiter = () => {

    const [value, setValue] = useState({
        username: "",
        password: "",
        password1: "",
    })

    const hendleChange = (e) => {
        setValue({
            ...value,
            [e.target.name]: e.target.value
        });
        console.log(value);
    }

    const hendleSubmit = (e) => {
        e.preventDefault();
        console.log(value);
        if (value.password !== value.password1) {
            alert("Password not match");
        } else {
            resgiter(value).then((res) => {
                alert(res.data)
            }).catch((error) => {
                alert(error.response.data)
            });
        }
    }

    return (
        <div style={style.body}>
                <div style={style.wrapper}>
                    <h1>Resgiter</h1>
                    <div className="form-group">
                        <label for="">Username</label>
                        <input type="text" className="form-control" name="username"  aria-describedby="helpId" placeholder="Press enter username" onChange={hendleChange} />
                    </div>
                    <div className="form-group">
                        <label for="">Password</label>
                        <input type="text" className="form-control" name="password"  aria-describedby="helpId" placeholder="Press enter password" onChange={hendleChange} />
                    </div>
                    <div className="form-group">
                        <label for="">Confirm Password</label>
                        <input type="text" className="form-control" name="password1"  aria-describedby="helpId" placeholder="Press enter password agian" onChange={hendleChange} />
                    </div>
                    <button disabled={value.password.length < 6 || value.password1.length < 6 } type="button" className="btn btn-primary w-100" style={style.button} onClick={hendleSubmit}>Resgiter</button>
                    <p>Already resgitered? <Link to="/login">Login now.</Link></p>
                </div>
        </div>
    )
}

export default Resgiter

const style = {
    body: { height: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center', backgroundColor: 'blue' },
    wrapper: { width: '500px', backgroundColor: 'white', padding: 50, borderRadius: 15 },
    button: { marginTop: '10px' }
}