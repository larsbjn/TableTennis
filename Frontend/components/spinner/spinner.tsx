import React from "react";
import styles from './spinner.module.scss';

const Spinner: React.FC = () => {
    return (
        <span className={styles.loader}></span>
    )
}

export default Spinner;