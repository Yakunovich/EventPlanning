import React, { useState } from "react";
import accountService from "../services/accountService";
import "../css/RegisterForm.css";

const RegisterForm = () => {
  const [email, setEmail] = useState("");
  const [role, setRole] = useState("User");
  const [password, setPassword] = useState("");
  const [fullName, setFullName] = useState("");
  const [additionalFields, setAdditionalFields] = useState([
    { fieldName: "", fieldValue: "" },
  ]);
  const [registrationSuccess, setRegistrationSuccess] = useState(false);
  const [error, setError] = useState(null);

  const handleAdditionalFieldChange = (index, field, value) => {
    const updatedFields = [...additionalFields];
    updatedFields[index] = { fieldName: field, fieldValue: value };
    setAdditionalFields(updatedFields);
  };

  const handleAddField = () => {
    setAdditionalFields([
      ...additionalFields,
      { fieldName: "", fieldValue: "" },
    ]);
  };

  const handleRemoveField = (index) => {
    const updatedFields = [...additionalFields];
    updatedFields.splice(index, 1);
    setAdditionalFields(updatedFields);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await accountService.register(
        email,
        password,
        fullName,
        role,
        additionalFields
      );
      console.log(response.data);
      setRegistrationSuccess(true);
      setError(null);
    } catch (error) {
      console.error("Error registering", error);
      setError("Registration failed. Please try again.");
    }
  };

  return (
    <div className="form-container">
      <h2>Register Form</h2>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Full Name:</label>
          <input
            type="text"
            value={fullName}
            onChange={(e) => setFullName(e.target.value)}
            required
          />
        </div>
        <div className="form-group">
          <label>Email:</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="form-group">
          <label>Password:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="form-group">
          <label>Role:</label>
          <select
            value={role}
            onChange={(e) => setRole(e.target.value)}
            required
          >
            <option value="User">User</option>
            <option value="Manager">Manager</option>
          </select>
        </div>
        <div className="additional-fields">
          {additionalFields.map((field, index) => (
            <div key={index} className="additional-field">
              <input
                type="text"
                value={field.fieldName}
                onChange={(e) =>
                  handleAdditionalFieldChange(
                    index,
                    e.target.value,
                    field.fieldValue
                  )
                }
                placeholder="Additional Field Name"
              />
              <input
                type="text"
                value={field.fieldValue}
                onChange={(e) =>
                  handleAdditionalFieldChange(
                    index,
                    field.fieldName,
                    e.target.value
                  )
                }
                placeholder="Additional Field Value"
              />
              {index > 0 && (
                <button type="button" onClick={() => handleRemoveField(index)}>
                  Remove
                </button>
              )}
            </div>
          ))}
          <button type="button" onClick={handleAddField}>
            Add Additional Field
          </button>
        </div>
        <button type="submit">Register</button>
      </form>
      {registrationSuccess && (
        <p className="success-message">Registration successful!</p>
      )}
      {error && <p className="error-message">{error}</p>}
    </div>
  );
};

export default RegisterForm;
