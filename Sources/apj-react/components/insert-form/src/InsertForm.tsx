import React from 'react';
import styled from 'styled-components';
import FormBuilder from '../../form/src/FormBuilder';

export interface InsertFormProps {
    className?: string;
    buttonLabel?: string;
    onSubmit: any;
    objectModel: any;
    hiddenColumns?: any[];
    columnsLabel?: any[];
    withLoader?: boolean;
}

const InsertForm: React.FC<InsertFormProps> = ({
    className,
    buttonLabel='Insert',
    onSubmit,
    objectModel,
    hiddenColumns=["Id"],
    columnsLabel,
    withLoader=true
}) => {

    return (
        <Wrapper className={["insert-form", className].join(' ')}>
            <FormBuilder 
                buttonLabel={buttonLabel}
                onSubmit={onSubmit}
                objectModel={objectModel}
                hiddenColumns={hiddenColumns}
                columnsLabel={columnsLabel}
                withLoader={withLoader}
            />
        </Wrapper>
    );
}

const Wrapper = styled.div`
`;

export default InsertForm;