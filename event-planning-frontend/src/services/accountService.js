import api from "../apiConfig";

const BASE_URL = "/Account";

const accountService = {
  register: async (email, password, fullName, role, additionalFields) => {
    try {
      const response = await api.post(`${BASE_URL}/register`, {
        email,
        password,
        fullName,
        role,
        additionalFields,
      });
      return response.data;
    } catch (error) {
      console.error("Error during registration:", error);
      throw error;
    }
  },

  authenticate: async (email, password) => {
    try {
      const response = await api.post(`${BASE_URL}/authenticate`, {
        email,
        password,
      });
      return response.data;
    } catch (error) {
      console.error("Error during authentication:", error);
      throw error;
    }
  },

  getProfile: async (id) => {
    try {
      const response = await api.get(`${BASE_URL}/${id}`);
      return response.data;
    } catch (error) {
      console.error("Error fetching profile:", error);
      throw error;
    }
  },
};

export default accountService;
