import { text, withKnobs } from "@storybook/addon-knobs";
import SearchForm, { SearchFormProps } from "../src/SearchForm";


const Props: SearchFormProps = {
    objectModel: [{"type":"String","label":"Id","name":"Id","value":null},{"type":"String","label":"Url","name":"Url","value":null},{"type":"String","label":"Label","name":"Label","value":null},{"type":"number","label":"Rank","name":"Rank","value":null},{"type":"String","label":"IdParent","name":"IdParent","value":null}],
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
    onSubmit: async () => {
        const res = await fetch('https://gambist-backend.herokuapp.com/user/all')
        res.json()
        .then((data) => {
            console.log(data);
        })
    }
};

export default {
    component: SearchForm,
    title: 'Search Form',
    decorators: [withKnobs],
    parameters: {
        notes: ` 
            <h2>Search Form</h2>
        `,
        knobs: {
            escapeHTML: false
        }
    }
};
  
export const SearchFormComponent = () => {
    return (
        <SearchForm {...Props} />
    )
}