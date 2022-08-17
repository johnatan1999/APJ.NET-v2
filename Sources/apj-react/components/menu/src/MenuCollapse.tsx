import { Collapse, List, ListItem, ListItemIcon, ListItemText } from '@material-ui/core';
import { ExpandLess, ExpandMore, Inbox, StarBorder } from '@material-ui/icons';
import React from 'react';
import styled from 'styled-components';
import MenuLink from './MenuLink';

export interface Menu {
    label: string;
    url?: string;
    submenus: Menu[];
}

export interface MenuCollapseProps {
    className?: string;
    level: number;
    menu: Menu;
    opened?: boolean;
}

const MenuCollapse: React.FC<MenuCollapseProps> = ({
    className,
    menu,
    level,
    opened=false
}) => {

    const [open, setOpen] = React.useState(opened);

    const handleClick = () => {
        setOpen(!open);
    };

    return (
        <Wrapper leftMargin={level + 35} className={['menu-link', className].join(' ')}>
            <List>
                <ListItem button onClick={handleClick}>
                    <ListItemIcon>
                    <Inbox />
                    </ListItemIcon>
                    <ListItemText primary={menu.label} />
                    {open ? <ExpandLess /> : <ExpandMore />}
                </ListItem>
                <Collapse className="submenu" in={open} timeout="auto" unmountOnExit>
                    {menu.submenus.map((menu, index) => {
                        console.log(menu);
                        if(menu.submenus && menu.submenus.length > 0) {
                            return <MenuCollapse level={level + 1} menu={menu} />   
                        }
                        else return <MenuLink  level={level + 1} key={index} label={menu.label} link={menu.url || ''} />
                    })}
                    {/* <List component="div" >
                        <ListItem button>
                            <ListItemIcon>
                            <StarBorder />
                            </ListItemIcon>
                            <ListItemText primary="Starred" />
                        </ListItem>
                    </List> */}
                </Collapse>
            </List>
        </Wrapper>
    );
}

const Wrapper = styled.div<{ leftMargin: any }>`
    &.menu-link {
        margin-left: 0;/*${props => props.leftMargin}px;*/
    }

    .submenu {
        background-color: rgba(0,0,0,0.4);
    }
`;  

export default MenuCollapse;