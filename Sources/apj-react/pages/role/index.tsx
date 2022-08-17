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
import { Role } from '../../model/apj.model';
import ApiResponse, { ApiListResponse } from '../../model/response/ApiResponse';
import RoleService from '../../services/role/role.service';
import MenuService from '../../services/menu/menu.service';

interface RolePageProps {
    roles: ApiListResponse<Role>;
    hierarchicalMenus: ApiResponse<Menu[]>;
}

const RolePage = (props: RolePageProps) => {
    const {
        roles,
        hierarchicalMenus
    } = props;

    const [addModalVisible, setAddModalVisible] = useState<boolean>(false);
    const [editModalVisible, setEditModalVisible] = useState<boolean>(false);
    const [detailsModalVisible, setDetailsModalVisible] = useState<boolean>(false);
    const [deleteModalVisible, setDeleteModalVisible] = useState<boolean>(false);
    const [roleList, setRoleList] = useState<Role[]>([]);

    const [currentPage, setCurrentPage] = useState<number>(1);
    const [rowsPerPage, setRowsPerPage] = useState<number>(5);
    const [dataLoading, setDataLoading] = useState<boolean>(false);
    const [totalCount, setTotalCount] = useState<number>(10);
    const [selectedRole, setSelectedRole] = useState<Role>();
    const [objectModel, setObjectModel] = useState<any>();

    useEffect(() => {
        setRoleList(roles.content.data);
        setRowsPerPage(roles.content.limit);
        setTotalCount(roles.content.totalCount);
        setCurrentPage(roles.content.page-1);
        setObjectModel(roles.objectModel);
    }, [roles]);

    const searchRole = async (e: any, role: any) => {
        const service = new RoleService();
        service.searchPaginatedRole(role)
        .then((res: ApiListResponse<Role>) => {
            setRoleList(res.content.data);
            setTotalCount(res.content.totalCount);
            setCurrentPage(res.content.page);
        })
        .catch((err:any)=>{
            console.log(err);
        });
    }

    const onAddRole = async (e: any, role: any) => {
        const service = new RoleService();
        service.addRole(role)
        .then((res: ApiResponse<Role>) => {
            if(res.success) {
                setRoleList([
                    res.content,
                    ...roleList
                ]);
                setTotalCount(totalCount+1);
            }
            setAddModalVisible(false);
        })
        .catch((err:any) => {
            console.log(err);
            setAddModalVisible(false);
        });
    }

    const onEditRole = async (e: any, role: any) => {
        const service = new RoleService();
        service.editRole(role)
        .then((res: ApiResponse<Role>) => {
            if(res.success) {
                setRoleList(roleList.map((m: Role) => {
                    return m.id == res.content.id ? res.content : m;
                }));
                setEditModalVisible(false);
            }
        })
        .catch((err:any) => {
            console.log(err);
            setEditModalVisible(false);
        });
    }

    const onDeleteRole = async (e: any) => {
        if(selectedRole != null) {
            const service = new RoleService();
            service.deleteRole(selectedRole.id)
            .then((res: ApiResponse<Role>) => {
                if(res.success) {
                    setRoleList(roleList.filter((m: Role) => {
                        return m.id != selectedRole.id;
                    }));
                    setTotalCount(totalCount-1);
                    setDeleteModalVisible(false);
                }
            })
            .catch((err:any) => {
                console.log(err);
                setDeleteModalVisible(false);
            });
        }
    }

    const onDeleteMultipleRole = async (e: any, rolesId=[]) => {
        const service = new RoleService();
        await service.deleteMultipleRoles(rolesId)
        .then((res: ApiResponse<Role>) => {
            if(res.success) {
                console.log(res.message);
            }
        })
        .catch((err) => {
            console.log(err);
        });
    } 

    const onChangePage = async (e: any, page: number) => {
        setDataLoading(true);
        const service = new RoleService();
        const result: ApiListResponse<Role> = await service.getPaginatedRole(page+1, rowsPerPage);
        if(result) {
            console.log(result);
            setRoleList([
                ...result.content.data,
            ])
            setCurrentPage(page);
        }
        setDataLoading(false);
    }

    const onChangeRowsPerPage = (e: any) => {
        setRowsPerPage(e.target.value)
    }

    const onOpenEditModal = (role: any, e: any) => {
        setSelectedRole(role);
        changeObjectModelValue(role);
        setEditModalVisible(true);
    }

    const onOpenDeleteModal = (role: any, e: any) => {
        setSelectedRole(role);
        setDeleteModalVisible(true);
    }

    const onOpenDetailsModal = (role: any, e: any) => {
        setSelectedRole(role);
        changeObjectModelValue(role);
        setDetailsModalVisible(true);
    }

    const changeObjectModelValue = (role: any) => {
        var _objectModel = [...objectModel];
        const keys = Object.keys(role);
        for(const key of keys) {
            for(const field of _objectModel) {
                if(field.name.toLowerCase() === key.toLowerCase()) {
                    field.value = role[key]
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
                        onSubmit={searchRole}
                    />
                </TitleBorder>
                <TitleBorder title="Role List">
                    <Modal 
                        width="large" 
                        show={addModalVisible} 
                        title="Add role" 
                        closeOnClickOutside={true}
                        onClose={()=>{ setAddModalVisible(false); }}> 
                        <InsertForm 
                            className="insert-form"
                            objectModel={objectModel}
                            onSubmit={onAddRole}
                            hiddenColumns={["Id"]}
                        />
                    </Modal>
                    <Modal show={detailsModalVisible} title="Role details" onClose={()=>{ setDetailsModalVisible(false); }}> 
                        <Details
                            objectModel={objectModel}
                        />
                    </Modal>
                    <Modal show={editModalVisible} title="Edit role" onClose={()=>{ setEditModalVisible(false); }}>
                        <UpdateForm
                            objectModel={objectModel}
                            onSubmit={onEditRole}
                            hiddenColumns={["Id"]}
                        />
                    </Modal>
                    <ConfirmDialog 
                        message={"Are you sure you want to delete this role?"}
                        onAbort={() => {setDeleteModalVisible(false)}}
                        show={deleteModalVisible} 
                        onConfirm={onDeleteRole}/>
                    <ApjList 
                        editAction={onOpenEditModal}
                        deleteAction={onOpenDeleteModal}
                        addAction={() => {setAddModalVisible(true)}} 
                        detailsAction={onOpenDetailsModal}
                        multipleDeleteAction={onDeleteMultipleRole}
                        objectModel={objectModel}
                        activeMultipleSelection={false}
                        data={roleList} />
                    {rowsPerPage>0 && totalCount>0 && currentPage>0 && <TablePagination
                        component="div"
                        count={totalCount}
                        page={currentPage}
                        onPageChange={onChangePage}
                        rowsPerPage={rowsPerPage}
                        onRowsPerPageChange={onChangeRowsPerPage}
                    />}
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
    const service = new RoleService();
    const roles = await service.getPaginatedRole(1, 10);
    const hierarchicalMenus = await new MenuService().getHierarchicalMenu();
    return {
        props: {
            roles,
            hierarchicalMenus
        }
    }
}

export default RolePage;