import React from 'react';
import styled from 'styled-components';
import { Menu } from '../../model/apj.model';
import Header from '../header/Header';
import SideNav from '../side-nav/SideNav';


export interface PageWrapperProps {
    className?: string;
    menus: Menu[];
}

const Page:React.FC<PageWrapperProps> = ({
    className='',
    children,
    menus=[]
}) => {
    return (
        <Wrapper className={[className, "page"].join(' ')}>
            <SideNav menus={menus} />
            <div className="page-container">
                <Header/>
                <div className="page-content">
                    {children}
                </div>
            </div>
        </Wrapper>
    )
}

const Wrapper = styled.div`
    &.page {
        display: flex;
        flex-direction: row;
    }
    .page-container {
        width: 80%;
        min-width: 400px;
        min-height: 100vh;
    }
    .page-content {
        padding: 20px;
    }
`;

export default Page;