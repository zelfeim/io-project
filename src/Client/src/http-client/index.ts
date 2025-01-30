import axios, { type AxiosInstance } from 'axios';

const httpClient: AxiosInstance = axios.create({
    baseURL: 'http://localhost:5000',
    withCredentials: true,
});

export default httpClient;
