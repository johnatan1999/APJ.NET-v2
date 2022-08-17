import Config from '../../config/config.json';
import BasicService from '../basic.service';

export default class UserService extends BasicService {

    async getAllUser() {
        return await BasicService.fetchData(`${Config.User.FindAll}`);
    }
    
    async getPaginatedUser(page=1, limit=5) {
        return await BasicService.fetchData(`${Config.User.FindAll}/${page}/${limit}`);
    }
    
    async searchPaginatedUser(criteria: any, page=1, limit=10) {
        const params: any = {};
        const keys = Object.keys(criteria);
        for(const key of keys) {
            if(criteria[key]) params[key] = criteria[key];
        }     
        return await BasicService.fetchData(`${Config.User.FindAll}/${page}/${limit}`, params);
    }

    async addUser(user: any) {
        return await BasicService.postData(Config.User.Add, user);
    }

    async editUser(user: any) {
        return await BasicService.putData(Config.User.Edit+"/"+user.Id, user);
    }

    async deleteUser(id: string) {
        return await BasicService.deleteData(Config.User.Delete+"/"+id);
    }

    async deleteMultipleUsers(ids: string []) {
        return await BasicService.deleteData(Config.User.Delete, {data: ids});
    }

    async getUserModel() {
        return await BasicService.fetchData(Config.User.Model);
    }

}