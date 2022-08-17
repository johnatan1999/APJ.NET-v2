import React, { useState } from 'react';
import styled from 'styled-components';
import { TextField as TextInput, InputLabel } from '@material-ui/core'

export interface DateFieldProps {
    id?: string;
    name?: string; 
    className?: string;
    label: string;
    defaultValue?: any;
    onChange?: any;
    inlineField?: boolean;
}

const DateField: React.FC<DateFieldProps> = ({
    className,
    defaultValue,
    label='',
    id,
    name,
    onChange,
    inlineField=true
}) => {

    const [value, setValue] = useState<any>(defaultValue);

    const _onChange = (e: any) => {
        if(onChange) onChange(e);
        setValue(e.target.value);
    }

    return (
        <Wrapper className={["apj-date-field", className, inlineField ? 'inline-field' : ''].join(' ')}>
            <InputLabel className="apj-label">{label}</InputLabel>
            <TextInput type="date" onChange={_onChange} className="apj-input" value={value || new Date()} variant="outlined" id={id} name={name} />
        </Wrapper>
    );
}

const Wrapper = styled.div`
    &.apj-date-field {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 10px 0;
    }
    &.apj-date-field.inline-field {
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
        min-width: 222px;
        input {
            padding: 14px 14px;
        }
    }
`;

export default DateField;