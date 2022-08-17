import React from 'react';
import styled from 'styled-components';
import { Select, InputLabel, MenuItem } from '@material-ui/core'

export interface SelectFieldProps {
    id?: string;
    name?: string; 
    className?: string;
    label: string;
    defaultValue?: string;
    onChange?: any;
    data: any[];
    dataFieldLabel?: string;
    inlineField?: boolean;
}

const SelectField: React.FC<SelectFieldProps> = ({
    className,
    defaultValue,
    label='',
    id,
    name,
    onChange,
    data=[],
    inlineField=true,
    dataFieldLabel="Id"
}) => {
    return (
        <Wrapper className={["apj-select-field", className, inlineField ? 'inline-field' : ''].join(' ')}>
            <InputLabel className="apj-label">{label}</InputLabel>
            <Select 
                className="apj-input" 
                value={defaultValue} 
                onChange={onChange}
                variant="outlined" id={id} name={name}>
                    {data && data.map((row, index) => {
                        return <MenuItem value={row.Id} key={index}>{row[dataFieldLabel]}</MenuItem>
                    })}
            </Select>
        </Wrapper>
    );
}

const Wrapper = styled.div`
    &.apj-select-field {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 10px 0;
    }
    &.apj-select-field.inline-field {
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
        div:first-child {
            padding: 14px 14px;
        }
    }
`;

export default SelectField;