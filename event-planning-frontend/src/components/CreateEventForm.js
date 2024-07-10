import React, { useState } from 'react';
import eventService from '../services/eventService';
import '../css/CreateEventForm.css'; 

const CreateEventForm = ({ onClose, onEventCreated }) => {
  const [name, setName] = useState('');
  const [theme, setTheme] = useState('');
  const [location, setLocation] = useState('');
  const [date, setDate] = useState('');
  const [maxParticipants, setMaxParticipants] = useState('');
  const [additionalFields, setAdditionalFields] = useState([]);

  const handleCreateEvent = async (event) => {
    event.preventDefault();
    try {
      const eventDto = {
        name,
        theme,
        location,
        date,
        maxParticipants,
        additionalFields
      };

      const createdEvent = await eventService.createEvent(eventDto);
      onEventCreated(createdEvent); 
      onClose();  
    } catch (error) {
      console.error('Error creating event:', error);
    }
  };

  const handleAdditionalFieldChange = (index, field) => {
    const updatedFields = [...additionalFields];
    updatedFields[index] = field;
    setAdditionalFields(updatedFields);
  };

  const handleAddField = () => {
    setAdditionalFields([...additionalFields, { fieldName: '', fieldValue: '' }]);
  };

  return (
    <div className="create-event-form">
      <h2>Create New Event</h2>
      <form onSubmit={handleCreateEvent}>
        <label>Name:
          <input type="text" value={name} onChange={(e) => setName(e.target.value)} required />
        </label>
        <label>Theme:
          <input type="text" value={theme} onChange={(e) => setTheme(e.target.value)} required />
        </label>
        <label>Location:
          <input type="text" value={location} onChange={(e) => setLocation(e.target.value)} required />
        </label>
        <label>Date:
          <input type="date" value={date} onChange={(e) => setDate(e.target.value)} required />
        </label>
        <label>Max Participants:
          <input type="number" value={maxParticipants} onChange={(e) => setMaxParticipants(e.target.value)} required />
        </label>

        <div>
          <h3>Additional Fields:</h3>
          {additionalFields.map((field, index) => (
            <div key={index}>
              <input
                type="text"
                placeholder="Field Name"
                value={field.fieldName}
                onChange={(e) => handleAdditionalFieldChange(index, { ...field, fieldName: e.target.value })}
              />
              <input
                type="text"
                placeholder="Field Value"
                value={field.fieldValue}
                onChange={(e) => handleAdditionalFieldChange(index, { ...field, fieldValue: e.target.value })}
              />
            </div>
          ))}
          <button type="button" onClick={handleAddField}>Add Field</button>
        </div>

        <button type="submit">Create Event</button>
        <button type="button" onClick={onClose}>Cancel</button>
      </form>
    </div>
  );
};

export default CreateEventForm;
