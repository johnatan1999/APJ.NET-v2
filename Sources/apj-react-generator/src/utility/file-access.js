const fs = require('fs');
const path = require('path');

const TEMPLATE_FOLDER = "ressources/templates/"

const getTemplate = (templateName) => {  
    const template = readFile(getTemplateUrl(templateName));
    return template;
}

const writeFile = (path, data) => {
    console.log("Write file at", path)
    if(fs.existsSync(path)) throw new Error(`${path} already exist.`);
    fs.writeFileSync(path, data, 'utf-8');
}

const readFile = (path) => {
    console.log("Read file at", path);
    return fs.readFileSync(path, 'utf-8');
}

const getTemplateUrl = (templateName) => {
    return __basedir + TEMPLATE_FOLDER + templateName + '.txt';
}

// const writePage = (pageName, data) => {
//     if(!fs.existsSync(process.cwd() + '/pages')) 
//         throw new Error(`There is no pages directory on the current folder (${process.cwd() + '/pages'})`);
//     const pagePath = getPagePath(pageName);
//     if(!fs.existsSync(pagePath)) {
//         fs.mkdirSync(pagePath);
//     }
//     writeFile(pagePath + '/index.tsx', data);
// }

const writePage = (fileName, pageName, data) => {
    if(!fs.existsSync(process.cwd() + '/pages')) 
        throw new Error(`There is no pages directory on the current folder (${process.cwd() + '/pages'})`);
    const pagePath = getPagePath(pageName);
    if(!fs.existsSync(pagePath)) {
        fs.mkdirSync(pagePath);
    }
    writeFile(pagePath + `/${fileName}.tsx`, data);
}

const writeService = (serviceName, data) => {
    if(!fs.existsSync(process.cwd() + '/services')) 
        throw new Error(`There is no services directory on the current folder (${process.cwd() + '/services'})`);
    const servicePath = getServicePath(serviceName);
    if(!fs.existsSync(servicePath)) {
        fs.mkdirSync(servicePath);
    }
    writeFile(`${servicePath}/${serviceName.toLowerCase()}.service.ts`, data);
}

const addConfig = (key, conf) => {
    let confData = readFile(getConfigFilePath());
    confData = JSON.parse(confData);
    confData[key] = conf;
    writeInConfigFile(JSON.stringify(confData, null, 2));
}

const writeInConfigFile = (data) => {
    const configPath = getConfigFilePath();
    writeFile(configPath, data);
}

const getConfigFilePath = () => {
    return process.cwd() + '/config/config.json';
}

const getPagePath = (pageName) => {
    return process.cwd() + '/pages/'+pageName.toLowerCase();
}

const getServicePath = (pageName) => {
    return process.cwd() + '/services/'+pageName.toLowerCase();
}

module.exports = {
    getTemplate,
    writeFile,
    getPagePath,
    writePage,
    writeService,
    addConfig
}