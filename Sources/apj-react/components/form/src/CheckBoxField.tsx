import React, { useState } from 'react';
import styled from 'styled-components';
import { Checkbox, InputLabel } from '@material-ui/core'

export interface CheckBoxFieldProps {
    id?: string;
    name?: string; 
    className?: string;
    label: string;
    checked?: boolean;
    onChange?: any;
    inlineField?: boolean;
}

const CheckBoxField: React.FC<CheckBoxFieldProps> = ({
    className,
    checked=false,
    label='',
    id,
    name,
    onChange,
    inlineField=true
}) => {

    const [value, setValue] = useState<boolean>(checked);

    const _onChange = (e: any) => {
        if(onChange) onChange(e);
        setValue(e.target.checked);
    }

    return (
        <Wrapper className={["apj-cbx-field", className, inlineField ? 'inline-field' : ''].join(' ')}>
            <InputLabel className="apj-label">{label}</InputLabel>
            <Checkbox onChange={_onChange} className="apj-input" checked={value}  id={id} name={name} />
        </Wrapper>
    );
}

const Wrapper = styled.div`
    &.apj-cbx-field {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 10px 0;
    }
    &.apj-cbx-field.inline-field {
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
    .apj-input {
        min-width: 206px;
        border-radius: 0;
        input {
            padding: 14px 14px;
        }
    }
`;

export default CheckBoxField;