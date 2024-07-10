import api from "../apiConfig";

const BASE_URL = '/Registration';

const registrationService = {
    register: async (eventId) => {
        try {
            const response = await api.post(BASE_URL, eventId);
            return response.data;
        } catch (error) {
            console.error('Error registering:', error);
            throw error;
        }
    },

    confirmRegistration: async (token) => {
        try {
            const response = await api.get(`${BASE_URL}/confirm/${token}`);
            return response.data;
        } catch (error) {
            console.error('Error confirming registration:', error);
            throw error;
        }
    },

    getRegistrationsByEventId: async (eventId) => {
        try {
            const response = await api.get(`${BASE_URL}/event/${eventId}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching registrations by event id:', error);
            throw error;
        }
    },

    getRegistrationsByAccountId: async (accountId) => {
        try {
            const response = await api.get(`${BASE_URL}/account/${accountId}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching registrations by account id:', error);
            throw error;
        }
    },

    cancelRegistration: async (id) => {
        try {
            await api.delete(`${BASE_URL}/${id}`);
        } catch (error) {
            console.error('Error canceling registration:', error);
            throw error;
        }
    }
};

export default registrationService;