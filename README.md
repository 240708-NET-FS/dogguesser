# DogGuesser Game 🐾

Welcome to **DogGuesser**, the ultimate game where you put your dog breed knowledge to the test! Compete against the clock, identify various dog breeds, and climb your way to the top of the leaderboard. Get ready for some paw-some fun!

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Environment Variables](#environment-variables)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)

## Features

- Guess the dog breed from a variety of images.
- Compete on the leaderboard.
- User authentication and role-based access control.
- Dynamic score management and history tracking.

## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/240708-NET-FS/dogguesser.git
cd dogguesser
```

### 2. Set Up the Frontend

Navigate to the frontend application directory and install the dependencies:

```bash
cd dogguesser-frontend/App
npm install
```

### 3. Set Up the Backend

Navigate to the backend application directory and install the dependencies:

```bash
cd dogguesser_backend
dotnet run
```

## Running the Application

### 1. Running the Frontend

Navigate to the \`dogguesser-frontend/App\` folder and start the frontend server:

```bash
cd dogguesser-frontend/App
npm start
```

This will start the frontend on \`http://localhost:5501\`

### 2. Running the Backend

Navigate to the \`dogguesser-backend\` folder and start the backend server:

```bash
cd dogguesser-backend
dotnet run
```

This will start the backend API on \`http://localhost:5153\`.

## Environment Variables

To configure the connection between the frontend and backend, you need to create a \`.env\` file in the \`dogguesser-frontend/App\` directory. The \`.env\` file should contain the following:

```
API_URL=http://localhost:5153/api
```

This environment variable tells the frontend where to send API requests.

### Creating the \`.env\` File

1. Navigate to the \`dogguesser-frontend/App\` directory.
2. Create a file named \`.env\`.
3. Add the following content:

```plaintext
API_URL=http://localhost:5153/api
```

## Technologies Used

- **Frontend**: HTML, CSS, JavaScript, Parcel, Fetch API
- **Backend**: ASP.NET Core, C#, Entity Framework Core
- **Database**: SQL Server (or other supported databases)
- **Authentication**: JWT (JSON Web Tokens)
- **Testing**: Jest, fetch-mock

## Contributing

If you ask to contribute to this project we will be very confused.
