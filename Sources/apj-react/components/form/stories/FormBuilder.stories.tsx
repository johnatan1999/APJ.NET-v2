import { text, withKnobs } from "@storybook/addon-knobs";
import FormBuilder, { FormBuilderProps } from "../src/FormBuilder";


const Props: FormBuilderProps = {
    objectModel: {id: 1, name: "A", desc: "Desc", age: 10, active: true, list: '1', date: new Date()},
    hiddenColumns: ['Id', 'desc'],
    withDefaultValue: true,
    columnsLabel: [
        {current: 'date', new: 'Date', type: 'date'},
        {current: 'name', new: 'Name'},
        {current: 'age', new: 'Age', type: 'number'},
        {current: 'active', new: 'Active', type: 'boolean'},
        {current: 'list', new: 'List', type: 'list', data: [
            {value: '1', desc: 'A'},
            {value: '2', desc: 'B'},
            {value: '3', desc: 'C'},
        ]}
    ]
};

export default {
    component: FormBuilder,
    title: 'Form',
    decorators: [withKnobs],
    parameters: {
        notes: ` 
            <h2>Form Builder</h2>
        `,
        knobs: {
            escapeHTML: false
        }
    }
};
  
export const FormBuilderComponent = () => {
    return (
        <FormBuilder {...Props} />
    )
}