import Config from '../../config/config.json';
import BasicService from '../basic.service';

export default class RoleService extends BasicService {

    async getAllRole() {
        return await BasicService.fetchData(`${Config.Role.FindAll}`);
    }
    
    async getPaginatedRole(page=1, limit=5) {
        return await BasicService.fetchData(`${Config.Role.FindAll}/${page}/${limit}`);
    }
    
    async searchPaginatedRole(criteria: any, page=1, limit=10) {
        const params: any = {};
        const keys = Object.keys(criteria);
        for(const key of keys) {
            if(criteria[key]) params[key] = criteria[key];
        }     
        return await BasicService.fetchData(`${Config.Role.FindAll}/${page}/${limit}`, params);
    }

    async addRole(role: any) {
        return await BasicService.postData(Config.Role.Add, role);
    }

    async editRole(role: any) {
        return await BasicService.putData(Config.Role.Edit+"/"+role.Id, role);
    }

    async deleteRole(id: string) {
        return await BasicService.deleteData(Config.Role.Delete+"/"+id);
    }

    async deleteMultipleRoles(ids: string []) {
        return await BasicService.deleteData(Config.Role.Delete, {data: ids});
    }

}