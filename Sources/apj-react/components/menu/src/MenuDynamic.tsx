import { Collapse, List, ListItem, ListItemIcon, ListItemText } from '@material-ui/core';
import { ExpandLess, ExpandMore, StarBorder, Inbox } from '@material-ui/icons';
import React from 'react';
import styled from 'styled-components';
import MenuCollapse from './MenuCollapse';
import MenuLink from './MenuLink';

export interface MenuDynamicProps {
    className?: string; 
    menuData: any[];
}

const MenyDynamic: React.FC<MenuDynamicProps> = ({
    className,
    menuData
}) => {

    return (
        <Wrapper className={['menu-dynamic', className].join(' ')}>
            {menuData.map((menu, index) => {
                if(menu.submenus && menu.submenus.length > 0) {
                    return <MenuCollapse level={menu.level} key={index} menu={menu} />
                } else {
                    return <MenuLink level={menu.level} key={index} link={menu.url} {...menu} />
                }
            })}
        </Wrapper>
    );
}

const Wrapper = styled.div`
    &.menu-dynamic {
        min-width: 200px;
        max-width: 400px;
    }
    .MuiListItemIcon-root {
        color: var(--white);
    }
`;

export default MenyDynamic;