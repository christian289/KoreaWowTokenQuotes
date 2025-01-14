'use client';

import { useEffect, useState } from 'react';
import styles from './TokenData.module.css';

export default function TokenData() {
  const [data, setData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('http://localhost:5298/api/token-data', {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          }
        });
        const result = await response.json();
        setData(result);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>Token Data</h1>
      {data ? (
        <div className={styles.dataContainer}>
          <pre className={styles.data}>{JSON.stringify(data, null, 2)}</pre>
        </div>
      ) : (
        <p className={styles.loading}>Loading...</p>
      )}
    </div>
  );
}
