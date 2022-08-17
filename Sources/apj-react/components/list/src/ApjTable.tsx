import { Checkbox, IconButton, Link, Table, TableBody, TableCell, TableContainer, TableRow } from '@material-ui/core';
import { Delete, Edit, More } from '@material-ui/icons';
import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import styled from 'styled-components';
import Button from '../../form/src/Button';
import ApjTableHeader from './ApjTableHeader';

export interface ApjListProps {
    className?: string;
    data: any[]; 
    objectModel: any;
    hiddenColumns?: string[];
    columnsLabel?: any[];
    showAddButton?: boolean;
    showMultipleDeleteAction?: boolean;
    addAction?: any;
    editAction?: any;
    deleteAction?: any;
    multipleDeleteAction?: any;
    detailsAction?: any;
    activeMultipleSelection?: boolean;
    exportAction?: any;
    importAction?: any;
    showExportButton?: boolean;
    showImportButton?: boolean;
}

const ApjList: React.FC<ApjListProps> = ({
    className,
    data = [],
    hiddenColumns=[],
    columnsLabel=[],
    showAddButton=true,
    showMultipleDeleteAction=true,
    addAction,
    editAction,
    deleteAction,
    multipleDeleteAction,
    detailsAction,
    objectModel,
    activeMultipleSelection=true,
    showExportButton=true,
    showImportButton=true,
    exportAction,
    importAction
}) => {

    const [dataList, setDataList] = useState<any[]>([]);
    const [columns, setColumns] = useState<any[]>([]);
    const [selectedRows, setSelectedRows] = useState<string[]>([]);

    const getColumns = () => {
        if(!objectModel) return [];
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
                if(column.name == columnsLabel[i].current)
                    column.label = columnsLabel[i].new;
            }
        }
        return columns;
    }

    const firstLetterToLower = (str: string) => {
        return str[0].toLowerCase() + str.substr(1);
    }

    useEffect(()=> {
        setColumns(getColumns());
    }, [objectModel]);

    const onSelectRow = (e: any, menu: any) => {
        const elt = e.target as HTMLInputElement;
        if(elt.checked) {
            setSelectedRows([
                menu.id,
                ...selectedRows
            ]);
        } else {
            setSelectedRows([
                ...selectedRows.filter((item: any)=>item != menu.id)
            ]);
        }
    }   

    const _multipleDeleteAction = async (e: any) => {
        if(multipleDeleteAction) multipleDeleteAction(e, selectedRows);
    }

    return (
        <Wrapper className={['apj-table', className].join(' ')}>
            <div className="table-actions">
                {showAddButton && <Button onClick={addAction}>Add</Button>}
                {showMultipleDeleteAction && 
                    <Button bgColor="var(--danger)" onClick={_multipleDeleteAction}>Delete</Button>}
                {showExportButton && 
                    <Button bgColor="var(--green)" 
                        onClick={() => { if(exportAction) exportAction(data); }}>
                            Export
                    </Button>}
                {showImportButton && <Button onClick={importAction}>Import</Button>}
            </div>
            <TableContainer>
                <Table>
                    <ApjTableHeader
                        activeMultipleSelection={activeMultipleSelection}
                        columnsLabel={columns} 
                        onSelectAll={(e: any) => {
                            if(!e.target.checked) setSelectedRows([]);
                            else setSelectedRows(data.map(elt=>{ return elt.id; }));
                        }} 
                        />
                    <TableBody>
                    {data.map((row: any, index: number) => (
                        <TableRow key={index}>
                            {activeMultipleSelection && <TableCell padding="checkbox">
                                <Checkbox 
                                    checked={selectedRows.includes(row.id)}
                                    onChange={(e: any)=>{ onSelectRow(e, row); }} />
                            </TableCell>}
                            {columns && columns.map((column, index) => {
                                return <TableCell key={index}>
                                    { row[firstLetterToLower(column.name)] }
                                </TableCell>
                            })}
                            <TableCell className="row-actions">
                                <IconButton className="edit-btn" onClick={(e: any)=> { editAction(row, e); }}>
                                    <Edit/>
                                </IconButton>
                                <IconButton className="delete-btn" onClick={(e: any)=> { deleteAction(row, e);}}>
                                    <Delete/>
                                </IconButton>
                                <IconButton onClick={(e: any) => {detailsAction(row, e);}}>
                                    <More/>
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Wrapper>
    );

}

const Wrapper = styled.div`
    .table-actions {
        display: flex;
        flex-direction: row-reverse;
        button {
            margin-left: 10px;
        }
    }
    .row-actions {
        display: flex;
        justify-content: center;
    }
    .delete-btn {
        color: var(--red);
    }
    .edit-btn {
        color: var(--primary);
    }
`;

export default ApjList;