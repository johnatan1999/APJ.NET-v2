const fileAccess = require('../utility/file-access');
const path = require('path');

const generatePage = (args) => {
    console.log("generate-page", args);
    if(args.length < 2) throw new Error("Please specify the model name");
    if(args.length > 1) {
        const object = args[0];
        const modelName = args[1];
        switch(object){
            case 'list':
                generateList(modelName);
                break;
            case 'detail':
                generateDetails(modelName);
                break;
            case 'insert':
                generateAdd(modelName);
                break;
            case 'edit':
                generateEdit(modelName);
                break;
            case 'all':
                generateList(modelName);
                generateAdd(modelName);
                generateEdit(modelName);
                generateDetails(modelName);
                generateService(modelName);
                updateConfigFile(modelName);
                break;
            case 'config':
                updateConfigFile(modelName)
                break;
            default: break;
        }
    }
}

const generateList = (modelName) => {
    let template = fileAccess.getTemplate('list');
    template = template.replace(new RegExp("{{Model_Name}}", "g"), modelName);
    template = template.replace(new RegExp("{{Variable_Name}}", "g"), modelName.toLowerCase());
    fileAccess.writePage("index", modelName, template);
}

const generateAdd = (modelName) => {
    let template = fileAccess.getTemplate('add');
    template = template.replace(new RegExp("{{Model_Name}}", "g"), modelName);
    template = template.replace(new RegExp("{{Variable_Name}}", "g"), modelName.toLowerCase());
    fileAccess.writePage("add", modelName, template);
}


const generateEdit = (modelName) => {
    let template = fileAccess.getTemplate('edit');
    template = template.replace(new RegExp("{{Model_Name}}", "g"), modelName);
    template = template.replace(new RegExp("{{Variable_Name}}", "g"), modelName.toLowerCase());
    fileAccess.writePage("edit", modelName, template);
}

const generateDetails = (modelName) => {
    let template = fileAccess.getTemplate('details');
    template = template.replace(new RegExp("{{Model_Name}}", "g"), modelName);
    template = template.replace(new RegExp("{{Variable_Name}}", "g"), modelName.toLowerCase());
    fileAccess.writePage("details", modelName, template);
}


const generateService = (modelName) => {
    let template = fileAccess.getTemplate('service');
    template = template.replace(new RegExp("{{Model_Name}}", "g"), modelName);
    template = template.replace(new RegExp("{{Variable_Name}}", "g"), modelName.toLowerCase());
    fileAccess.writeService(modelName, template);
}

const updateConfigFile = (modelName) => {
    const conf  = {
        "FindById": "api/" + modelName.toLowerCase(),
        "FindAll": "api/" + modelName.toLowerCase(),
        "Add": "api/" + modelName.toLowerCase(),
        "Edit": "api/" + modelName.toLowerCase(),
        "Delete": "api/" + modelName.toLowerCase(),
        "Model": "api/"+modelName.toLowerCase()+"/model",
        "Import": "api/"+modelName.toLowerCase()+"/import"
    }
    fileAccess.addConfig(modelName, conf);
}

module.exports = {
    generatePage,
}