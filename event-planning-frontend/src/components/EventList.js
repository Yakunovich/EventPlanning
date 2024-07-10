import React, { useEffect, useState, useContext } from "react";
import eventService from "../services/eventService";
import registrationService from "../services/registrationService";
import "../css/EventList.css";
import CreateEventForm from "./CreateEventForm";
import { AuthContext } from "../AuthContext";

const EventList = () => {
  const [events, setEvents] = useState([]);
  const [showCreateForm, setShowCreateForm] = useState(false);
  const { userRole } = useContext(AuthContext);

  const fetchEvents = async () => {
    try {
      const data = await eventService.getAllEvents();
      const eventsArray = data.$values || [];
      setEvents(eventsArray);
    } catch (error) {
      console.error("Failed to fetch events", error);
    }
  };

  useEffect(() => {
    fetchEvents();
  }, []);

  const handleCreateFormOpen = () => {
    setShowCreateForm(true);
  };

  const handleCreateFormClose = () => {
    setShowCreateForm(false);
  };

  const handleEventCreated = (newEvent) => {
    setEvents([...events, newEvent]);
  };

  const handleRegister = async (eventId) => {
    try {
      await registrationService.register({ eventId });
      alert("Registration successful!");

      fetchEvents();
    } catch (error) {
      console.error("Error registering for event", error);
      alert(error.response.data);
    }
  };

  return (
    <div className="event-list">
      {userRole === "Manager" && (
        <button onClick={handleCreateFormOpen}>Create New Event</button>
      )}
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Theme</th>
            <th>Location</th>
            <th>Date</th>
            <th>Max Participants</th>
            <th>Current Participants</th>
            <th>Additional Fields</th>
            {userRole === "User" && <th>Actions</th>}
          </tr>
        </thead>
        <tbody>
          {events.map((event) => (
            <tr key={event.id}>
              <td>{event.name}</td>
              <td>{event.theme}</td>
              <td>{event.location}</td>
              <td>{new Date(event.date).toLocaleDateString()}</td>
              <td>{event.maxParticipants}</td>
              <td>{event.currentParticipants}</td>
              <td>
                <ul>
                  {event.eventAdditionalFields.$values.map((field) => (
                    <li key={field.id}>
                      <strong>{field.fieldName}:</strong> {field.fieldValue}
                    </li>
                  ))}
                </ul>
              </td>
              <td>
                {userRole === "User" && (
                  <button onClick={() => handleRegister(event.id)}>
                    Register
                  </button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {showCreateForm && (
        <div className="create-event-overlay">
          <CreateEventForm
            onClose={handleCreateFormClose}
            onEventCreated={handleEventCreated}
          />
        </div>
      )}
    </div>
  );
};

export default EventList;
