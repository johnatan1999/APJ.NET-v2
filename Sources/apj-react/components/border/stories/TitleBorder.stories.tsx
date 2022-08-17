import { text, withKnobs } from "@storybook/addon-knobs";
import TitleBorder, { TitleBorderProps } from "../src/TitleBorder";

const Props: TitleBorderProps = {
    title: "Title example"
};

export default {
    component: TitleBorder,
    title: 'Title Border',
    decorators: [withKnobs],
    parameters: {
        notes: ` 
            <h2>Titled Border</h2>
        `,
        knobs: {
            escapeHTML: false
        }
    }
};
  
export const TitleBorderComponent = () => {
    return (
        <TitleBorder {...Props} />
    )
}