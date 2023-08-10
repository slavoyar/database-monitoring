// import { FC } from 'react';


function Querty() {
    return (
        <div>
            <p>Ыт ыз нот квэрты</p>
        </div>
    );
}

export default function SearchPanel() {
    let q=1;
    return(
        <div>
            <Querty></Querty>
            <button type="button">
                Отсортировать по названию
            </button>
            <p>{q}</p>
            <button type="button">
                Отсортировать по адресу
            </button>
        </div>
    );
}
