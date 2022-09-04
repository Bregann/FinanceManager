import { useState, useEffect } from "react";
import { apiUrl } from "../config";

export interface UseFetchGETResult {
    data: [];
    setData: (data: []) => void;
    isPending: boolean;
    error: string | null;
}

export const useFetchGET = (url: string) => {
    const [data, setData] = useState<[]>([]);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState<string | null>(null);

    let response: UseFetchGETResult = {
        data: data,
        isPending: isPending,
        setData: setData,
        error: error
    };

    useEffect( () => {
        const abortCont = new AbortController();

        fetch(apiUrl + url, { signal: abortCont.signal})
            .then(res => {
                if(!res.ok){
                    throw Error('');
                }

                return res.json();
            })
            .then(data => {
                setData(data);
                setIsPending(false);
            })
            .catch(err => {
                if(err.name === 'AbortError'){
                }
                else{
                    setError(err.message);
                    setIsPending(false);
                }
            })
    }, [url]);

    return response;
}

export const DoPost = async (url: string, data: any) => {
    const res = await fetch(apiUrl + url, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(data)
    });

    return res.json();
}

export const DoDelete = async(url: string, data: any) => {
    const res = await fetch(apiUrl + url, {
        method: 'DELETE',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(data)
    });

    return res;
}

export const DoGet = async (url: string) => {
    const res = await fetch(apiUrl + url);
    return res.json();
}