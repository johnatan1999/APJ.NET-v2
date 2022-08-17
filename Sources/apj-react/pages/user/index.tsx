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
import { Menu, Users as User } from '../../model/apj.model';
import ApiResponse, { ApiListResponse } from '../../model/response/ApiResponse';
import UserService from '../../services/user/user.service';
import MenuService from '../../services/menu/menu.service';
import { Snackbar } from '@material-ui/core';
import MuiAlert from '@material-ui/lab/Alert';

interface UserPageProps {
    users: ApiListResponse<User>;
    hierarchicalMenus: ApiResponse<Menu[]>;
    model: ApiResponse<{
        objectModel: any;
        formModel: any;
    }>
}

const UserPage = (props: UserPageProps) => {
    const {
        users,
        hierarchicalMenus,
        model
    } = props;

    const [addModalVisible, setAddModalVisible] = useState<boolean>(false);
    const [editModalVisible, setEditModalVisible] = useState<boolean>(false);
    const [detailsModalVisible, setDetailsModalVisible] = useState<boolean>(false);
    const [deleteModalVisible, setDeleteModalVisible] = useState<boolean>(false);
    const [userList, setUserList] = useState<User[]>([]);

    const [currentPage, setCurrentPage] = useState<number>(1);
    const [rowsPerPage, setRowsPerPage] = useState<number>(5);
    const [dataLoading, setDataLoading] = useState<boolean>(false);
    const [totalCount, setTotalCount] = useState<number>(10);
    const [selectedUser, setSelectedUser] = useState<User>();
    const [objectModel, setObjectModel] = useState<any>();
    const [formModel, setFormModel] = useState<any>();
    const [openSnackbar, setOpenSnackbar] = useState<boolean>(false);
    const [snackBarMessage, setSnackbarMessage] = useState<any>();
    const [snackbarSeverity, setSnackbarSeverity] = useState<"success" | "error">("success");

    const showSuccessMessage = (message: string) => {
        setOpenSnackbar(true);
        setSnackbarMessage(message);
        setSnackbarSeverity("success");
    }

    const showErrorMessage = (message: string) => {
        setOpenSnackbar(true);
        setSnackbarMessage(message);
        setSnackbarSeverity("success");
    }

    useEffect(() => {
        setUserList(users.content.data);
        setRowsPerPage(users.content.limit);
        setTotalCount(users.content.totalCount);
        setCurrentPage(users.content.page-1);
        setObjectModel(users.objectModel);
        setFormModel(model.content.formModel.fields);
    }, [users, model]);

    const searchUser = async (e: any, user: any) => {
        const service = new UserService();
        await service.searchPaginatedUser(user)
        .then((res: ApiListResponse<User>) => {
            setUserList(res.content.data);
            setTotalCount(res.content.totalCount);
            setCurrentPage(res.content.page);
        })
        .catch((err:any)=>{
            console.log(err);
            showErrorMessage(err.message);
        });
    }

    const onAddUser = async (e: any, user: any) => {
        const service = new UserService();
        await service.addUser(user)
        .then((res: ApiResponse<User>) => {
            if(res.success) {
                setUserList([
                    res.content,
                    ...userList
                ]);
                setTotalCount(totalCount+1);
                showSuccessMessage(res.message);
            } else showErrorMessage(res.message);
            setAddModalVisible(false);
        })
        .catch((err: Error) => {
            console.log(err);
            showErrorMessage(err.message);
            setAddModalVisible(false);
        });
    }

    const onEditUser = async (e: any, user: any) => {
        const service = new UserService();
        await service.editUser(user)
        .then((res: ApiResponse<User>) => {
            if(res.success) {
                setUserList(userList.map((m: User) => {
                    return m.id == res.content.id ? res.content : m;
                }));
                setEditModalVisible(false);
                showSuccessMessage(res.message);
            } else showErrorMessage(res.message);
        })
        .catch((err: Error) => {
            console.log(err);
            showErrorMessage(err.message);
            setEditModalVisible(false);
        });
    }

    const onDeleteUser = async (e: any) => {
        if(selectedUser != null) {
            const service = new UserService();
            await service.deleteUser(selectedUser.id)
            .then((res: ApiResponse<User>) => {
                if(res.success) {
                    setUserList(userList.filter((m: User) => {
                        return m.id != selectedUser.id;
                    }));
                    setTotalCount(totalCount-1);
                    setDeleteModalVisible(false);
                    showSuccessMessage(res.message);
                } else showErrorMessage(res.message);
            })
            .catch((err: Error) => {
                console.log(err);
                showErrorMessage(err.message);
                setDeleteModalVisible(false);
            });
        }
    }

    const onDeleteMultipleUser = async (e: any, usersId=[]) => {
        const service = new UserService();
        await service.deleteMultipleUsers(usersId)
        .then((res: ApiResponse<User>) => {
            if(res.success) {
                console.log(res.message);
                showSuccessMessage(res.message);
            } else {
                showErrorMessage(res.message);
            }
        })
        .catch((err) => {
            console.log(err);
            showErrorMessage(err.message)
        });
    } 

    const exportData = async (users: any[]) => {
        await new UserService().exportCSVFile(users, 'users');
    }

    const onChangePage = async (e: any, page: number) => {
        setDataLoading(true);
        const service = new UserService();
        const result: ApiListResponse<User> = await service.getPaginatedUser(page+1, rowsPerPage);
        if(result) {
            console.log(result);
            setUserList([
                ...result.content.data,
            ])
            setCurrentPage(page);
        }
        setDataLoading(false);
    }

    const onChangeRowsPerPage = (e: any) => {
        setRowsPerPage(e.target.value)
    }

    const onOpenEditModal = (user: any, e: any) => {
        setSelectedUser(user);
        changeObjectModelValue(user);
        setEditModalVisible(true);
    }

    const onOpenDeleteModal = (user: any, e: any) => {
        setSelectedUser(user);
        setDeleteModalVisible(true);
    }

    const onOpenDetailsModal = (user: any, e: any) => {
        setSelectedUser(user);
        changeObjectModelValue(user);
        setDetailsModalVisible(true);
    }

    const changeObjectModelValue = (user: any) => {
        var _objectModel = [...objectModel];
        const keys = Object.keys(user);
        for(const key of keys) {
            for(const field of _objectModel) {
                if(field.name.toLowerCase() === key.toLowerCase()) {
                    field.value = user[key]
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
                        objectModel={formModel}
                        onSubmit={searchUser}
                        columnsLabel={[
                            {current: 'IdRole', new: 'Role'}
                        ]}
                    />
                </TitleBorder>
                <TitleBorder title="User List">
                    <Modal 
                        width="large" 
                        show={addModalVisible} 
                        title="Add user" 
                        closeOnClickOutside={true}
                        onClose={()=>{ setAddModalVisible(false); }}> 
                        <InsertForm 
                            className="insert-form"
                            objectModel={formModel}
                            onSubmit={onAddUser}
                            hiddenColumns={["Id"]}
                        />
                    </Modal>
                    <Modal show={detailsModalVisible} title="User details" onClose={()=>{ setDetailsModalVisible(false); }}> 
                        <Details
                            objectModel={objectModel}
                        />
                    </Modal>
                    <Modal show={editModalVisible} title="Edit user" onClose={()=>{ setEditModalVisible(false); }}>
                        <UpdateForm
                            objectModel={formModel}
                            onSubmit={onEditUser}
                            hiddenColumns={["Id"]}
                        />
                    </Modal>
                    <ConfirmDialog 
                        message={"Are you sure you want to delete this user?"}
                        onAbort={() => {setDeleteModalVisible(false)}}
                        show={deleteModalVisible} 
                        onConfirm={onDeleteUser}/>
                    <ApjList 
                        editAction={onOpenEditModal}
                        deleteAction={onOpenDeleteModal}
                        addAction={() => {setAddModalVisible(true)}} 
                        detailsAction={onOpenDetailsModal}
                        multipleDeleteAction={onDeleteMultipleUser}
                        objectModel={objectModel}
                        activeMultipleSelection={false}
                        exportAction={exportData}
                        columnsLabel={[
                            {current: 'IdRole', new: 'Role'}
                        ]}
                        data={userList} />
                    {rowsPerPage>0 && totalCount>10 && currentPage>0 && <TablePagination
                        component="div"
                        count={totalCount}
                        page={currentPage}
                        onPageChange={onChangePage}
                        rowsPerPage={rowsPerPage}
                        onRowsPerPageChange={onChangeRowsPerPage}
                    />}
                </TitleBorder>
                <Snackbar open={openSnackbar} autoHideDuration={6000} onClose={() => {setOpenSnackbar(false)}}>
                    <MuiAlert onClose={() => {setOpenSnackbar(false)}} severity={snackbarSeverity}>
                        {snackBarMessage}
                    </MuiAlert>
                </Snackbar>
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
    const service = new UserService();
    const users = await service.getPaginatedUser(1, 10);
    const hierarchicalMenus = await new MenuService().getHierarchicalMenu();
    const model = await service.getUserModel();
    return {
        props: {
            users,
            hierarchicalMenus,
            model
        }
    }
}

export default UserPage;