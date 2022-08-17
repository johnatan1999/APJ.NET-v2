import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import Button from './Button';
import CheckBoxField from './CheckBoxField';
import DateField from './DateField';
import PasswordField from './PasswordField';
import SelectField from './SelectField';
import SpinnerField from './SpinnerField';
import TextField from './TextField';

export interface FormBuilderProps {
    className?: string;
    objectModel: {
        type: string;
        label: string;
        name: string;
        value: any;
    }[];
    hiddenColumns?: string[];
    columnsLabel?: any[];
    onSubmit?: (e: Event, data: any) => void;
    buttonLabel?: string;
    withDefaultValue?: boolean;
    withLoader?: boolean;
}   

const FormBuilder: React.FC<FormBuilderProps> = ({
    className,
    objectModel,
    hiddenColumns=["id"],
    columnsLabel=[],
    onSubmit,
    withDefaultValue=false,
    buttonLabel="Submit",
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

    const getColumns = () => {
        let columns = [...objectModel]; 
        // Hide column
        for(var i=0; i<hiddenColumns.length; i++) {
            for(const column of columns) {
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
                    column.type = columnsLabel[i].type;
                }
            }
        }
        return columns;
    }

    const _onSubmit = async (e: any) => {
        console.log("==>", formValues);
        if(onSubmit) onSubmit(e, formValues);
    }

    const onChange = (e: any, col: any) => {
        const values = { ...formValues };
        const elt = e.target as HTMLInputElement;
        if(col.type == 'boolean')
            values[col.name] = elt.checked;
        else if(elt.type == "number") values[col.name] = parseInt(elt.value);
        else values[col.name] = elt.value;
        setFormValues(values);
        e.stopPropagation();
    }

    useEffect(()=> {
        setColumns(getColumns());
        if(withDefaultValue) {
            updateFormValues();
        }
    }, [objectModel]);

    // useEffect(()=> {
    //     console.log(objectModel);
    //     if
    //     setFormValues(objectModel);
    // }, [objectModel]);

    return (
        <Wrapper onSubmit={_onSubmit} className={["apj-form-builder", className].join(' ')}>
            {columns.map((col, index) => {
                switch(col.type) {
                    case 'number':
                        return <SpinnerField defaultValue={formValues[col.value]} onChange={(e: any) => {onChange(e, col)}} key={index} label={col.label} />
                    case 'date':
                        return <DateField onChange={(e: any) => {onChange(e, col)}} 
                            key={index} 
                            label={col.label} 
                            defaultValue={formValues[col.name]}
                            />
                    case 'list':
                        return <SelectField data={col.data} defaultValue={formValues[col.name]} onChange={(e: any) => {onChange(e, col)}} key={index} label={col.label} />     
                    case 'select':
                        return <SelectField 
                            data={col.data} 
                            dataFieldLabel={col.dataFieldLabel}
                            defaultValue={formValues[col.name] || col.value} 
                            onChange={(e: any) => {onChange(e, col)}} 
                            key={index} label={col.label} 
                        />     
                    case 'boolean':
                        return <CheckBoxField 
                            onChange={(e: any) => {onChange(e, col)}}
                            key={index}
                            label={col.label}
                            checked={formValues[col.name]}
                        />    
                    case 'password':
                        return <PasswordField defaultValue={formValues[col.name] || ''} onChange={(e: any) => { onChange(e, col) }} key={index} label={col.label} />
                    default: 
                        return <TextField defaultValue={formValues[col.name] || ''} onChange={(e: any) => { onChange(e, col) }} key={index} label={col.label} />
                }
            })}
            <div className="button-container">
                <Button 
                    onClick={(e: any) => { _onSubmit(e); }}
                    withLoader={withLoader}
                    >{buttonLabel}</Button>
                    
            </div>
        </Wrapper>
    );

}

const Wrapper = styled.form`
    &.apj-form-builder {
        max-width: 600px;
        min-width: 500px;
        border: 1px solid #eee;
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 20px 0;
    }
    .button-container {
        padding-top: 20px; 
    }
`;

export default FormBuilder;