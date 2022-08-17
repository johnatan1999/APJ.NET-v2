import Config from '../config/config.json';
import LoginService from './authentification/login.service';
import converter from 'json-2-csv';

export const getBaseUrl = () => process.env.API_URL || process.env.BASE_URL || Config.BASE_URL;

export default abstract class BasicService {

    static async fetchData(uri: string, params: any=null)  {
        try {
            let paramsString = "";
            if(params) {
                const keys = Object.keys(params)
                const searchParams = new URLSearchParams();
                for(const key of keys)
                    searchParams.append(key, params[key])
                    paramsString = searchParams.toString();
            }
            const https = require("https");
            const options: any = {
                agent: new https.Agent({
                    rejectUnauthorized: false
                })
            };
            console.log("BO$>", getBaseUrl() + uri + (params ? "?" + paramsString : ""))
            const res = await fetch(getBaseUrl() + uri + (params ? "?" + paramsString : ""), options)
            return await res.json()
        } catch(error) {
            console.error("error >", error)
        }
    }

    static async postData(uri: string, params: any) {
        return this.sendData(uri, params, 'POST');
    }

    static async putData(uri: string, params: any) {
        return this.sendData(uri, params, 'PUT');
    }
    
    static async deleteData(uri: string, params={}) {
        return this.sendData(uri, params, 'DELETE');
    }
    

    static async sendData(uri: string, params: any, method='POST') {
        console.log("BO$>", getBaseUrl() + uri);
        const user = LoginService.getUserInfosFromLS();
        if(!user || !user.token) this.redirect("/login");
        const response = await fetch(getBaseUrl() + uri, {
            method: method,
            body: JSON.stringify(params),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
                "Authorization": "Bearer "+user.token
            }
        })
        
        return response.json();
        // .then(response => response.json())
        // .then(json => console.log(json));
    }

    static async sendDataWithoutToken(uri: string, params: any, method='POST') {
        console.log("BO$>", getBaseUrl() + uri);
        const response = await fetch(getBaseUrl() + uri, {
            method: method,
            body: JSON.stringify(params),
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
        
        return response.json();
    }

    static redirect(link: string) {
        document.location.href = link;
    }
    
    async exportCSVFile(data: any[], fileName: string) {
        if(data.length>0) {
            const keys = Object.keys(data[0]);
            const header: any = {};
            keys.forEach((key: any)=> {
                header[key] = key;
            });
            data.unshift(header);
        }
    
            
        converter.json2csv(data, (err, csv) => {
            if(err) throw err;
            var exportedFilenmae = fileName + '.csv' || 'export.csv';
            var blob = new Blob([csv as BlobPart], { type: 'text/csv;charset=utf-8;' });
            if (navigator.msSaveBlob) { // IE 10+
                navigator.msSaveBlob(blob, exportedFilenmae);
            } else {
                var link = document.createElement("a");
                if (link.download !== undefined) { // feature detection
                    // Browsers that support HTML5 download attribute
                    var url = URL.createObjectURL(blob);
                    link.setAttribute("href", url);
                    link.setAttribute("download", exportedFilenmae);
                    link.style.visibility = 'hidden';
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                }
            }
        });    
    }
}