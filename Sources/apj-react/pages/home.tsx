import { GetServerSideProps } from 'next';
import React from 'react';
import styled from 'styled-components';

const HomePage = (props: any) => {
    const {

    } = props;

    return (
        <Wrapper>Homepage</Wrapper>
    );
}

const Wrapper = styled.div`
    
`;

export const getServerSideProps: GetServerSideProps = async (ctx) => {    
    return {
        props: {
            
        }
    }
}

export default HomePage;