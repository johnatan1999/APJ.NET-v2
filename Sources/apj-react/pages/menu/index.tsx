import TablePagination from '@material-ui/core/TablePagination';
import { GetServerSideProps } from 'next';
import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import TitleBorder from '../../components/border/src/TitleBorder';
import Details from '../../components/details/Details';
import InsertForm from '../../components/insert-form/src/InsertForm';
import ApjList from '../../components/list/src/ApjTable';
import ConfirmDialog from '../../components/modal/ConfirmDialog';
import Modal from '../../components/modal/Modal';
import Page from '../../components/page-wrapper/Page';
import SearchForm from '../../components/search/src/SearchForm';
import UpdateForm from '../../components/update-form/src/UpdateForm';
import { Menu } from '../../model/apj.model';
import ApiResponse, { ApiListResponse } from '../../model/response/ApiResponse';
import MenuService from '../../services/menu/menu.service';

interface MenuPageProps {
    menus: ApiListResponse<Menu>;
    hierarchicalMenus: ApiResponse<Menu[]>;
}

const MenuPage = (props: MenuPageProps) => {
    const {
        menus,
        hierarchicalMenus
    } = props;

    const [addModalVisible, setAddModalVisible] = useState<boolean>(false);
    const [editModalVisible, setEditModalVisible] = useState<boolean>(false);
    const [detailsModalVisible, setDetailsModalVisible] = useState<boolean>(false);
    const [deleteModalVisible, setDeleteModalVisible] = useState<boolean>(false);
    const [menuList, setMenuList] = useState<Menu[]>([]);

    const [currentPage, setCurrentPage] = useState<number>(1);
    const [rowsPerPage, setRowsPerPage] = useState<number>(5);
    const [dataLoading, setDataLoading] = useState<boolean>(false);
    const [totalCount, setTotalCount] = useState<number>(10);
    const [selectedMenu, setSelectedMenu] = useState<Menu>();
    const [objectModel, setObjectModel] = useState<any>();

    useEffect(() => {
        setMenuList(menus.content.data);
        setRowsPerPage(menus.content.limit);
        setTotalCount(menus.content.totalCount);
        setCurrentPage(menus.content.page-1);
        setObjectModel(menus.objectModel);
    }, [menus]);

    const searchMenu = async (e: any, menu: any) => {
        const service = new MenuService();
        service.searchPaginatedMenu(menu)
        .then((res: ApiListResponse<Menu>) => {
            setMenuList(res.content.data);
            setTotalCount(res.content.totalCount);
            setCurrentPage(res.content.page);
        })
        .catch((err)=>{
            console.log(err);
        });
    }

    const onAddMenu = async (e: any, menu: any) => {
        const service = new MenuService();
        service.addMenu(menu)
        .then((res: ApiResponse<Menu>) => {
            if(res.success) {
                setMenuList([
                    res.content,
                    ...menuList
                ]);
                setTotalCount(totalCount+1);
            }
            setAddModalVisible(false);
        })
        .catch((err) => {
            console.log(err);
            setAddModalVisible(false);
        });
    }

    const onEditMenu = async (e: any, menu: any) => {
        const service = new MenuService();
        service.editMenu(menu)
        .then((res: ApiResponse<Menu>) => {
            if(res.success) {
                setMenuList(menuList.map((m: Menu) => {
                    return m.id == res.content.id ? res.content : m;
                }));
                setEditModalVisible(false);
            }
        })
        .catch((err) => {
            console.log(err);
            setEditModalVisible(false);
        });
    }

    const onDeleteMenu = async (e: any) => {
        if(selectedMenu != null) {
            const service = new MenuService();
            service.deleteMenu(selectedMenu.id)
            .then((res: ApiResponse<Menu>) => {
                if(res.success) {
                    setMenuList(menuList.filter((m: Menu) => {
                        return m.id != selectedMenu.id;
                    }));
                    setTotalCount(totalCount-1);
                    setDeleteModalVisible(false);
                }
            })
            .catch((err) => {
                console.log(err);
                setDeleteModalVisible(false);
            });
        }
    }

    const onDeleteMultipleMenu = async (e: any, menusId=[]) => {
        const service = new MenuService();
        await service.deleteMultipleMenus(menusId)
        .then((res: ApiResponse<Menu>) => {
            if(res.success) {
                console.log(res.message);
            }
        })
        .catch((err: any) => {
            console.log(err);
        });
    } 

    const onChangePage = async (e: any, page: number) => {
        setDataLoading(true);
        const service = new MenuService();
        const result: ApiListResponse<Menu> = await service.getPaginatedMenu(page+1, rowsPerPage);
        if(result) {
            console.log(result);
            setMenuList([
                ...result.content.data,
            ])
            setCurrentPage(page);
        }
        setDataLoading(false);
    }

    const onChangeRowsPerPage = (e: any) => {
        setRowsPerPage(e.target.value)
    }

    const onOpenEditModal = (menu: any, e: any) => {
        setSelectedMenu(menu);
        changeObjectModelValue(menu);
        setEditModalVisible(true);
    }

    const onOpenDeleteModal = (menu: any, e: any) => {
        setSelectedMenu(menu);
        setDeleteModalVisible(true);
    }

    const onOpenDetailsModal = (menu: any, e: any) => {
        setSelectedMenu(menu);
        changeObjectModelValue(menu);
        setDetailsModalVisible(true);
    }

    const changeObjectModelValue = (menu: any) => {
        var _objectModel = [...objectModel];
        const keys = Object.keys(menu);
        for(const key of keys) {
            for(const field of _objectModel) {
                if(field.name.toLowerCase() === key.toLowerCase()) {
                    field.value = menu[key]
                }
            }
        }
        setObjectModel(_objectModel);
    }

    return (
        <Wrapper>
            <Page menus={hierarchicalMenus.content}>
                <TitleBorder title="Search">
                    <SearchForm
                        objectModel={objectModel}
                        onSubmit={searchMenu}
                    />
                </TitleBorder>
                <TitleBorder title="Menu List">
                    <Modal 
                        width="large" 
                        show={addModalVisible} 
                        title="Add menu" 
                        closeOnClickOutside={true}
                        onClose={()=>{ setAddModalVisible(false); }}> 
                        <InsertForm 
                            className="insert-form"
                            objectModel={objectModel}
                            onSubmit={onAddMenu}
                            hiddenColumns={["Id"]}
                        />
                    </Modal>
                    <Modal show={detailsModalVisible} title="Menu details" onClose={()=>{ setDetailsModalVisible(false); }}> 
                        <Details
                            objectModel={objectModel}
                        />
                    </Modal>
                    <Modal show={editModalVisible} title="Edit menu" onClose={()=>{ setEditModalVisible(false); }}>
                        <UpdateForm
                            objectModel={objectModel}
                            onSubmit={onEditMenu}
                            hiddenColumns={["Id"]}
                        />
                    </Modal>
                    <ConfirmDialog 
                        message={"Are you sure you want to delete this menu?"}
                        onAbort={() => {setDeleteModalVisible(false)}}
                        show={deleteModalVisible} 
                        onConfirm={onDeleteMenu}/>
                    <ApjList 
                        editAction={onOpenEditModal}
                        deleteAction={onOpenDeleteModal}
                        addAction={() => {setAddModalVisible(true)}} 
                        detailsAction={onOpenDetailsModal}
                        multipleDeleteAction={onDeleteMultipleMenu}
                        objectModel={objectModel}
                        activeMultipleSelection={false}
                        data={menuList} />
                    <TablePagination
                        component="div"
                        count={totalCount}
                        page={currentPage}
                        onPageChange={onChangePage}
                        rowsPerPage={rowsPerPage}
                        onRowsPerPageChange={onChangeRowsPerPage}
                    />
                </TitleBorder>
            </Page>
        </Wrapper>
    );
}

const Wrapper = styled.div`
    .insert-form {
        margin: 30px 0;
    }
    .menu-dynamic {
        color: var(--white);
    }
`;

export const getServerSideProps: GetServerSideProps = async (ctx) => {    
    const service = new MenuService();
    const menus = await service.getPaginatedMenu(1, 10);
    const hierarchicalMenus = await service.getHierarchicalMenu();
    return {
        props: {
            menus,
            hierarchicalMenus
        }
    }
}

export default MenuPage;