import React from 'react';
import styled from 'styled-components';
import FormBuilder from '../../form/src/FormBuilder';

export interface UpdateFormProps {
    className?: string;
    buttonLabel?: string;
    onSubmit: any;
    objectModel: any;
    hiddenColumns?: any[];
    columnsLabel?: any[];
    withLoader?: boolean;
}

const UpdateForm: React.FC<UpdateFormProps> = ({
    className,
    buttonLabel='Update',
    onSubmit,
    objectModel,
    hiddenColumns,
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
                withDefaultValue={true}
                withLoader={withLoader}
            />
        </Wrapper>
    );
}

const Wrapper = styled.div`
`;

export default UpdateForm;