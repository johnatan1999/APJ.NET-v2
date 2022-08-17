import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import Button from '../../form/src/Button';
import CheckBoxField from '../../form/src/CheckBoxField';
import DateField from '../../form/src/DateField';
import SelectField from '../../form/src/SelectField';
import SpinnerField from '../../form/src/SpinnerField';
import TextField from '../../form/src/TextField';

export interface SearchFormProps {
    className?: string;
    objectModel?: {
        type: string;
        label: string;
        name: string;
        value: any;
    }[];
    hiddenColumns?: string[];
    columnsLabel?: any[];
    buttonLabel?: string;
    onSubmit?: any;
    withLoader?: boolean;
}

const SearchForm: React.FC<SearchFormProps> = ({
    className,
    objectModel=[],
    hiddenColumns=["id"],
    columnsLabel=[],
    buttonLabel='Search',
    onSubmit=() => {},
    withLoader=true
}) => {

    const [columns, setColumns] = useState<any[]>([]);
    const [formValues, setFormValues] = useState<any>({});

    const updateFormValues = () => {
        const _formValues: any = {};
        objectModel.forEach((field: any) => {
            _formValues[field.name] = field.value;
        });
        setFormValues(_formValues);
    }

    const onChange = (e: any, col: any) => {
        const values = { ...formValues };
        if(col.type == 'boolean')
            values[col.name] = e.target.checked;
        else values[col.name] = e.target.value;
        console.log(e.target.value);
        setFormValues(values);
    }

    const _onSubmit = async (e: any) => {
        if(onSubmit) await onSubmit(e, formValues);
    }

    const getColumns = () => {
        
        // Hide column
        const columns = [...objectModel];
        for(var i=0; i<hiddenColumns.length; i++) {
            for(const column of objectModel) {
                if(column.name == hiddenColumns[i])  {
                    columns.splice(columns.indexOf(column), 1);
                    break;
                }
            }
        }
        // replace column label
        for(var i=0; i<columnsLabel.length; i++) {
            for(const column of columns) {
                if(column.name == columnsLabel[i].current) {
                    column.label = columnsLabel[i].new;
                    // column.type = columnsLabel[i].type;
                }
            }
        }
        return  columns;
    }

    useEffect(()=> {
        setColumns(getColumns());
        
        // updateFormValues();
    }, [objectModel]);

    return (
        <Wrapper className={["apj-search-form", className].join(' ')}>
            {columns.map((col, index) => {
                switch(col.type) {
                    case "number":
                        return <SpinnerField 
                            defaultValue={formValues[col.name]} 
                            onChange={(e: any) => {onChange(e, col)}} 
                            key={index} 
                            label={col.label} 
                            inlineField={false}
                            className='search-field'
                        />
                    case 'date':
                        return <DateField onChange={(e: any) => {onChange(e, col)}} 
                            key={index} 
                            label={col.label} 
                            defaultValue={formValues[col.name]}
                            inlineField={false}
                            className='search-field'
                        />
                    case 'select':
                        return <SelectField 
                            data={col.data} 
                            dataFieldLabel={col.dataFieldLabel}
                            defaultValue={formValues[col.name] || col.value} 
                            onChange={(e: any) => {onChange(e, col)}} 
                            key={index} label={col.label} 
                            inlineField={false} 
                            className='search-field'   
                        />     
                    case 'boolean':
                        return <CheckBoxField 
                            onChange={(e: any) => {onChange(e, col)}}
                            key={index}
                            label={col.label}
                            checked={formValues[col.value]}
                            inlineField={false}
                            className='search-field'
                        />    
                    default: 
                        return <TextField   
                            className='search-field'
                            inlineField={false} 
                            defaultValue={formValues[col.name]} 
                            onChange={(e: any) => { onChange(e, col) }} 
                            key={index} 
                            label={col.label} 
                        />
                }
            })}
            <div className="button-container">
                <Button 
                    onClick={_onSubmit}
                    withLoader={withLoader}
                    >{buttonLabel}</Button>
            </div>
        </Wrapper>
    );
}

const Wrapper = styled.form`
    &.apj-search-form {
        min-width: 200px;
        max-width: 1200px;
        display: grid;
        grid-template-columns: auto auto auto;
        flex-direction: row;
        flex-wrap: wrap;
        justify-content: center;
        align-items: center;
        padding: 20px 0;
    }
    .search-field {
        margin: 10px 10px;
    }
    .apj-label { 
        margin-bottom: 4px;
    }
    .button-container {
        padding-top: 20px; 
        grid-column-start: 3;
        display: flex;
        justify-content: flex-end;
        padding-right: 10px;
    }
`;

export default SearchForm;