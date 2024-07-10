import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './pages/HomePage';
import RegisterPage from './pages/RegisterPage';
import LoginPage from './pages/LoginPage';
import EventPage from './pages/EventPage';
import NavBar from './NavBar';
import './App.css';
import { AuthProvider } from './AuthContext';

function App() {
  return (
    <AuthProvider>
    <Router>
      <NavBar/>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/events" element={<EventPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Routes>
    </Router>
    </AuthProvider>
  );
}

export default App;
