import Config from '../../config/config.json';
import BasicService from '../basic.service';

export default class MenuService extends BasicService {

    async getAllMenu() {
        return await BasicService.fetchData(`${Config.Menu.FindAll}`);
    }
    
    async getPaginatedMenu(page=1, limit=5) {
        return await BasicService.fetchData(`${Config.Menu.FindAll}/${page}/${limit}`);
    }
    
    async searchPaginatedMenu(criteria: any, page=1, limit=10) {
        const params: any = {};
        const keys = Object.keys(criteria);
        for(const key of keys) {
            if(criteria[key]) params[key] = criteria[key];
        }     
        return await BasicService.fetchData(`${Config.Menu.FindAll}/${page}/${limit}`, params);
    }

    async getHierarchicalMenu() {
        return await BasicService.fetchData(`${Config.Menu.FindAll}?hierarchical=true`)
    }

    async addMenu(menu: any) {
        return await BasicService.postData(Config.Menu.Add, menu);
    }

    async editMenu(menu: any) {
        return await BasicService.putData(Config.Menu.Edit+"/"+menu.Id, menu);
    }

    async deleteMenu(id: string) {
        return await BasicService.deleteData(Config.Menu.Delete+"/"+id);
    }

    async deleteMultipleMenus(ids: string []) {
        return await BasicService.deleteData(Config.Menu.Delete, {data: ids});
    }

}