import React, { useState } from 'react';
import styled from 'styled-components';
import { TextField as TextInput, InputLabel } from '@material-ui/core'

export interface SpinnerFieldProps {
    id?: string;
    name?: string; 
    className?: string;
    label: string;
    defaultValue?: number;
    onChange?: any;
    inlineField?: boolean;
}

const SpinnerField: React.FC<SpinnerFieldProps> = ({
    className,
    defaultValue=0,
    label='',
    id,
    name,
    onChange,
    inlineField=true
}) => {

    const [value, setValue] = useState<number>(defaultValue);

    const _onChange = (e: any) => {
        if(onChange) onChange(e);
        setValue(e.target.value);
    }

    return (
        <Wrapper className={["apj-spinner-field", className, inlineField ? 'inline-field' : ''].join(' ')}>
            <InputLabel className="apj-label">{label}</InputLabel>
            <TextInput type="number" onChange={_onChange} className="apj-input" value={value} variant="outlined" id={id} name={name} />
        </Wrapper>
    );
}

const Wrapper = styled.div`
    &.apj-spinner-field {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 10px 0;
    }
    &.apj-spinner-field.inline-field {
        flex-direction: column;
        @media screen and (min-width: 400px) {
            flex-direction: row;
        }
    }
    .apj-label {
        min-width: 220px; 
        font-weight: 700;
        margin-bottom: 10px;
        @media screen and (min-width: 400px) {
            margin-bottom: 0;
        }
    }
    .apj-input input {
        padding: 14px 14px;
    }
`;

export default SpinnerField;