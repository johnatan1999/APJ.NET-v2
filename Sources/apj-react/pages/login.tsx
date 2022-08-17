import { GetServerSideProps } from 'next';
import React, { useState } from 'react';
import styled from 'styled-components';
import Login from '../components/login/Login';
import { Users } from '../model/apj.model';
import ApiResponse from '../model/response/ApiResponse';
import LoginService from '../services/authentification/login.service';

const LoginPage = (props: any) => {
    
    const [errorMessage, setErrorMessage] = useState<any>();

    const doLogin = async (e: any, login: string, password: string) => {
        await LoginService.doLogin(login, password, (res: ApiResponse<Users>) => {
            setErrorMessage(res.message);
        });
    }

    return (
        <Wrapper className="login-page">
            <Login errorMessage={errorMessage} onSubmit={doLogin} />
        </Wrapper>
    );
}

const Wrapper = styled.div`
    &.login-page {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 50vh;
        .login-form {
            
        }
    }
`;


export default LoginPage;