import { Avatar, TextField, Typography } from '@material-ui/core';
import React, { useState } from 'react';
import styled from 'styled-components';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Button from '../form/src/Button';

export interface LoginFormProps {
    className?: string;
    buttonLabel?: string;
    onSubmit: any;
    errorMessage?: any;
}

const LoginForm: React.FC<LoginFormProps> = ({
    className,
    buttonLabel='Sign In',
    onSubmit,
    errorMessage
}) => {

    const [login, setLogin] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    const handleChange = (e: any) => {
        const elt = e.target as HTMLInputElement;
        if(elt.name == "email") {
            setLogin(elt.value);
        } else {
            setPassword(elt.value);
        }
    }

    const _onSubmit = async (e: any) => {
        if(onSubmit) await onSubmit(e, login, password);
    }

    return (
        <Wrapper className={["login-form", className].join(' ')}>
            <Avatar>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Sign in
            </Typography>
            <form noValidate onSubmit={_onSubmit}>
                <TextField
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="email"
                    label="Email Address"
                    name="email"
                    onChange={handleChange}
                    autoComplete="email"
                    value={login}
                    autoFocus
                />
                <TextField
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    name="password"
                    label="Password"
                    onChange={handleChange}
                    type="password"
                    id="password"
                    value={password}
                    autoComplete="current-password"
                />
                <div className="error-message"> { errorMessage }</div>
                <Button
                    className="submit-btn"
                    onClick={_onSubmit}
                    withLoader
                >
                    {buttonLabel}
                </Button>
            </form>
        </Wrapper>
    );
}

const Wrapper = styled.div`
    &.login-form {
        max-width: 400px;
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 5px;
    }
    .error-message {
        color: var(--red);
        font-size: 14px;
        padding: 5px 0 15px;
    }
    .submit-btn {
        width: 100%;
    }
`;

export default LoginForm;