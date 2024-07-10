import api from "../apiConfig";

const BASE_URL = '/Event';

const eventService = {
    getAllEvents: async () => {
        try {
            const response = await api.get(BASE_URL);
            return response.data;
        } catch (error) {
            console.error('Error fetching events:', error);
            throw error;
        }
    },

    getEventById: async (id) => {
        try {
            const response = await api.get(`${BASE_URL}/${id}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching event by id:', error);
            throw error;
        }
    },

    createEvent: async (eventDto) => {
        try {
            const response = await api.post(BASE_URL, eventDto);
            return response.data;
        } catch (error) {
            console.error('Error creating event:', error);
            throw error;
        }
    },

    updateEvent: async (id, eventDto) => {
        try {
            const response = await api.put(`${BASE_URL}/${id}`, eventDto);
            return response.data;
        } catch (error) {
            console.error('Error updating event:', error);
            throw error;
        }
    },

    deleteEvent: async (id) => {
        try {
            await api.delete(`${BASE_URL}/${id}`);
        } catch (error) {
            console.error('Error deleting event:', error);
            throw error;
        }
    }
};

export default eventService;
