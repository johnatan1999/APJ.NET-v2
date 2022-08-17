import { List, ListItem, ListItemIcon, ListItemText } from '@material-ui/core';
import { Inbox } from '@material-ui/icons';
import React from 'react';
import styled from 'styled-components';

export interface MenuLinkProps {
    className?: string;
    label: string;
    link: string;
    level: number;
}

const MenuLink: React.FC<MenuLinkProps> = ({
    className,
    label,
    link="#",
    level=0
}) => {

    const onClick = (e: any) => {
        document.location.href = link
    }

    return (
        <Wrapper leftMargin={level + 35} className={['menu-link', className].join(' ')}>
            <List>
                <ListItem button onClick={onClick}>
                    <ListItemIcon>
                        <Inbox />
                    </ListItemIcon>
                    <ListItemText primary={label} />
                </ListItem>
            </List>
        </Wrapper>
    );
}

const Wrapper = styled.div<{ leftMargin: any }>`
    &.menu-link {
        margin-left: 0;/*${props => props.leftMargin}px*/;
    }
`;  

export default MenuLink;