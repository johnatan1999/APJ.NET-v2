import ApjList, { ApjListProps } from "../src/ApjTable";
import { text, withKnobs } from "@storybook/addon-knobs";


const Props: ApjListProps = {
    data: [
        {id: 1, name: "A", desc: "Desc"},
        {id: 2, name: "B", desc: ""},
        {id: 3, name: "C", desc: ""},
    ],
    hiddenColumns: ['desc'],
    columnsLabel: [
        {current: 'name', new: 'Name'},
        {current: 'id', new: 'Replaced column value'}
    ]
};

export default {
    component: ApjList,
    title: 'Apj List',
    decorators: [withKnobs],
    parameters: {
        notes: ` 
            <h2>Certification Component</h2><ul>
                <li>Functional brief is here: https://7n414q.axshare.com/#id=fm69bp&p=certifications___endorsements__2_&g=12</li>
                <li>Technical Approach is here: Go to Microsoft teams, browse file below: General > Sprint 5 > Creative phase delivrables > 4.Specifications > Technical briefs > Product endorsements [TO] > TB - Product endorsement.docx</li>
                <li>The Certitication component will take the following props:
                <ol>
                    <li><b>Title text: </b> string, the title of the module</li>
                    <li><b>Description text: </b> array of product, the product that will be display. each of them should contain : </li>
                    <li><b>Carousel: </b> array of endorsement content,the list of endorsement that will be display,each should contains : 
                    <ol>
                        <li><b>Icon: </b> Icon, information for the image that will be display</li>
                        <li><b>Title: </b> string, title of the endorsment</li>
                        <li><b>Caption: </b> string, caption of the endorsement</li>
                        <li><b>url: </b> string, url to redirect on click on endorsement </li></ol>
                    </li><li><b>ClassName: </b> string, the classname for the component (Optionnal)</li><li><b>Pure: </b> boolean, set it to true to display component on pure version (Optionnal)</li>
                </ol>
                </li>
            </ul>
        `,
        knobs: {
            escapeHTML: false
        }
    }
};
  
export const ApjListComponent = () => {
    return (
        <ApjList {...Props} />
    )
}