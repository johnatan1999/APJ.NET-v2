import React, { useState } from 'react';
import styled from 'styled-components';
import { Button as ReactButton } from '@material-ui/core'
import Loader from '../../svg-icons/Loader';

export interface ButtonProps {
    id?: string;
    name?: string; 
    className?: string;
    onClick?: any;
    withLoader?: boolean;
    bgColor?: string;
    textColor?: string;
}

const Button: React.FC<ButtonProps> = ({
    className,
    id,
    name,
    onClick,
    withLoader,
    children,
    bgColor='#1976d2',
    textColor='white'
}) => {

    const [showLoader, setShowLoader] = useState<boolean>(false);

    const _onClick = async (e: any) => {
        if(onClick) {
            if(withLoader) setShowLoader(true);
            await onClick(e);
            setShowLoader(false);
        }
    } 

    return (
        <Wrapper bgColor={bgColor} textColor={textColor} className={["apj-button", className].join(' ')}>
            <ReactButton 
                variant="contained" 
                className={className}
                name={name} id={id} 
                onClick={_onClick} >
                    {showLoader && <Loader />}
                    {children}
                    </ReactButton>
        </Wrapper>
    );
}

const Wrapper = styled.div<{ bgColor: any; textColor: any }>`
    &.apj-button button {
        background-color: ${props => props.bgColor};
        color: ${props => props.textColor};
    }  
`;

export default Button;