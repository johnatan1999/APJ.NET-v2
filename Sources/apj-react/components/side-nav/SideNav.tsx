import { Link, List, ListItem, ListItemText } from '@material-ui/core';
import React from 'react';
import styled from 'styled-components';
import { Menu } from '../../model/apj.model';
import MenyDynamic from '../menu/src/MenuDynamic';

export interface SideNavProps {
    className?: string;
    menus: Menu[];
}

const SideNav: React.FC<SideNavProps> = ({
    className='',
    menus=[]
}) => {

    return (
        <Wrapper className={[className, 'side-nav'].join(' ')}>
            <div className="logo"></div>
            <MenyDynamic
                menuData={menus}
            />
        </Wrapper>
    )
}

const Wrapper = styled.div`
    &.side-nav {
        width: 20%;
        min-width: 240px;
        background-color: var(--dark); 
        display: flex;
        justify-content: start;
        flex-direction: column;
    }
    .logo {
        width: 100%;
        height: 68px;
        border-bottom: 5px solid var(--light-gray);
    }
    .nav {
        color: var(--white);
    }
    .nav-item {
        height: 64px;
        padding-left: 30px;
    }
    .nav-item a {
        color: var(--white);
        :hover {
            text-decoration: none;
        }
    }
`;

export default SideNav;