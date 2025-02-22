import dynamic from "next/dynamic";
import {Theme} from "react-select";
import {observer} from "mobx-react";

export interface Option {
    value: any;
    label: string;
}

export interface DropdownProps {
    options: Array<Option>;
    defaultValue?: Option;
    searchable?: boolean;
    onChange: (newValue: Option, actionMeta: any) => void;
}

const theme = (theme: Theme) => ({
    ...theme,
    colors: {
        ...theme.colors,
        primary: '#4a4a4a',
        primary25: '#4a4a4a',
        neutral50: 'white',
        neutral80: 'white',
        neutral0: 'rgb(43, 48, 53)',
    }
})

const DropdownRender = dynamic(() => import('react-select'), {ssr: false})

const Dropdown = observer(({...props}: DropdownProps) => {
    return (
        <DropdownRender
            theme={theme}
            options={props.options}
            defaultValue={props.defaultValue}
            isSearchable={props.searchable}
            onChange={(newValue, actionMeta) => props.onChange(newValue as Option, actionMeta)}
        />
    )
})

export default Dropdown;