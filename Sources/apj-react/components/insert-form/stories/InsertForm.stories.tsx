import { text, withKnobs } from "@storybook/addon-knobs";
import InsertForm, { InsertFormProps } from "../src/InsertForm";


const Props: InsertFormProps = {
    objectModel: {id: 1, name: "A", desc: "Desc", age: 10, active: true, list: '1', date: new Date()},
    hiddenColumns: ['id', 'desc'],
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
    ],
    onSubmit: () => {}
};

export default {
    component: InsertForm,
    title: 'Form',
    decorators: [withKnobs],
    parameters: {
        notes: ` 
            <h2>Insert Form</h2>
        `,
        knobs: {
            escapeHTML: false
        }
    }
};
  
export const InsertFormComponent = () => {
    return (
        <InsertForm {...Props} />
    )
}