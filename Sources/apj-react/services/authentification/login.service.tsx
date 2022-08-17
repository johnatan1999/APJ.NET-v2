import Config from '../../config/config.json';
import { Users } from '../../model/apj.model';
import ApiResponse from '../../model/response/ApiResponse';
import BasicService from '../basic.service';

export default class LoginService  {

    static async doLogin(login: string, password: string, onFail=(res: ApiResponse<Users>)=>{}) {
        return await BasicService.sendDataWithoutToken(`${Config.Login}`, { login, password })
        .then((res: ApiResponse<Users>) => {
            if(res.success) {
                this.saveUserInfosToLS(res.content);
                this.redirect('/home')
            } else {
                onFail(res);
            }
        })
        .catch((err) => {
            console.log(err);
        });
    }

    static async refreshUserInfo() {
        let user = this.getUserInfosFromLS();
        if(user) {
            user = await BasicService.fetchData(Config.Login, {userid: user.id});
            this.saveUserInfosToLS(user);
        }
    }
   
    static logout() {
        localStorage.removeItem('userinfos');
        this.redirect('/home')
    }

    static getUserInfosFromLS() {
        const userinfos = localStorage.getItem('userinfos');
        try {
            if(userinfos)   
                return JSON.parse(userinfos);
        }
        catch(err) {
             return null;
        }
    }

    static isLogged() {
        return !!this.getUserInfosFromLS();
    }

    static saveUserInfosToLS(user: any) {
        localStorage.setItem('userinfos', JSON.stringify(user));
    }

    static redirect(link: string) {
        document.location.href = link;
    }
    
}