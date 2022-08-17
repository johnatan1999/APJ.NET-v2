import { text, withKnobs } from "@storybook/addon-knobs";
import MenuDynamic, { MenuDynamicProps } from "../src/MenuDynamic";


const Props: MenuDynamicProps = {
    menuData: [
        {
            label: 'A',
            level: 1,
            children: [
                {
                    label: 'A-1',
                    children: [
                        {
                            label: 'A-1-1',
                            children: [
                                {
                                    label: 'A-1-1-1'
                                }
                            ]
                        }
                    ]
                },
                {
                    label: 'A-2'
                }
            ]
        },
        {
            label: 'B',
            level: 1,
            children: [
                {
                    label: 'B-1'
                },
                {
                    label: 'B-2'
                }
            ]
        },
        {
            level: 1,
            label: 'C'
        }
    ]
};

export default {
    component: MenuDynamic,
    title: 'Menu Dynamic',
    decorators: [withKnobs],
    parameters: {
        notes: ` 
            <h2>Menu Dynamic</h2>
        `,
        knobs: {
            escapeHTML: false
        }
    }
};
  
export const MenuDynamicComponent = () => {
    return (
        <MenuDynamic {...Props} />
    )
}