import React from 'react';

export interface IProps {
    id: string;
    label: string;
    selectedValue: number | string;
    options: { value: number | string, text: string }[];
    onChange(value: string);
    className?: string;
}

const Select: React.FC<IProps> = ({ id, label, selectedValue, options, className, onChange }) => {
    return <>
        <label className="mb-1" htmlFor={id}>{label}</label>
        <select value={selectedValue}
            onChange={({ target }) => onChange(target.value)} name={id} id={id} className={className}>
            {options.length > 0 ? (<>
                <option disabled value={""}>-- Please select an option --</option>
                {options.map((option, i) => <option key={i} value={option.value}>{option.text}</option>)}
            </>
            ) : (
                    <option disabled value="">-- No available options --</option>
                )}
        </select>
    </>
}

export default Select;