import axios from "axios";
import config from "../appsettings.json";

export default class GamesService {
    static getPaged = async (page, pageSize) => {
        return await axios.get(`${config.apiBaseUrl}/games?pageSize=${pageSize}&page=${page}`);
    }

    static get = async id => {
        return await axios.get(`${config.apiBaseUrl}/games/${id}`);
    }

    static post = async data => {
        return await axios.post(`${config.apiBaseUrl}/games`, data);
    }

    static put = async (id, data) => {
        return await axios.put(`${config.apiBaseUrl}/games/${id}`, data);
    }

    static remove = async id => {
        return await axios.delete(`${config.apiBaseUrl}/games/${id}`);
    }    
}

