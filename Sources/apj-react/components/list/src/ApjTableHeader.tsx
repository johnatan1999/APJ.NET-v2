import { Checkbox, TableCell, TableHead, TableRow, TableSortLabel } from '@material-ui/core';
import React, { useState } from 'react';
import Button from '../../form/src/Button';

export interface ApjTableHeader {
    className?: string;
    onSelectAll: any;
    selected?: any;
    numSelected?: number;
    rowCount?: number;
    columnsLabel: any[];
    activeMultipleSelection?: boolean;
}

const ApjTableHeader: React.FC<ApjTableHeader> = ({
    className    ,
    numSelected=0,
    onSelectAll,
    rowCount=0,
    selected,
    columnsLabel,
    activeMultipleSelection=true
}) => {
    
    const [selectAllChecked, setSelectAllChecked] = useState<boolean>(false);

    const _onSelectAll = (e: any) => {
        if(onSelectAll) onSelectAll(e);
        setSelectAllChecked(!!e.target.checked);
    }

    return (
        <TableHead>
            <TableRow>
                {activeMultipleSelection && <TableCell padding="checkbox">
                    <Checkbox
                        indeterminate={numSelected > 0 && numSelected < rowCount}
                        checked={selectAllChecked}
                        onChange={_onSelectAll}
                        inputProps={{ 'aria-label': 'select all' }}
                    />
                </TableCell>}
                {columnsLabel.map((headCell, index) => (
                    <TableCell key={index} >
                        {headCell.label}
                    </TableCell>
                ))}
                <TableCell style={{textAlign: "center"}}>
                    Actions
                </TableCell>
            </TableRow>
        </TableHead>
    );
}

export default ApjTableHeader;