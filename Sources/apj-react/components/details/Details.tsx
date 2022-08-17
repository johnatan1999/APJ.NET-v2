import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
export interface DetailsProps {
    className?: string;
    objectModel: {
        type: string;
        label: string;
        name: string;
        value: any;
    }[];
    hiddenColumns?: string[];
    columnsLabel?: any[];
}   

const Details: React.FC<DetailsProps> = ({
    className,
    objectModel,
    hiddenColumns=["id"],
    columnsLabel=[]
}) => {

    const [columns, setColumns] = useState<any[]>([]);

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

    useEffect(()=> {
        console.log(objectModel)
        setColumns(getColumns());
    }, [objectModel]);

    return (
        <Wrapper className={["details", className].join(' ')}>
            {columns.map((col, index) => {
                return (
                    <div key={index} className="details-field">
                        <div className="label">{col.label}</div>
                        <div className="value">{col.value}</div>
                    </div>
                );
            })}
        </Wrapper>
    );

}

const Wrapper = styled.div`
    &.details {
        max-width: 600px;
        min-width: 500px;
        border: 1px solid #eee;
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 50px;
    }
    .details-field {
        display: flex;
        width: 80%;
        flex-direction: row;
        align-items: center;
        margin-bottom: 10px;
    }
    .label, 
    .value {
        width: 50%;
    }
    .label {
        font-size: 16px;
        font-weight: 600;
    }
    .value {
        padding-left: 20px;
        text-align: right;
    }
`;

export default Details;