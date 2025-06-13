# AI-Driven_Learning_Platform
A miniature learning platform that allows users to choose what they want to learn (by category and subcategory), send instructions to AI to receive lessons created, and view their learning history. The platform includes a REST API, database, AI integration, and a basic web dashboard UI.


## Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone https://github.com/RutShira/AI-Driven_Learning_Platform.git
   cd AI-Driven_Learning_Platform
   ```

2. **Install dependencies for each part:**

   ### Backend (C#)
   ```bash
   cd backend
   dotnet restore
   ```

   ### Frontend (React/TypeScript/JavaScript)
   ```bash
   cd ../frontend
   npm install
   ```

3. **Copy the environment variables example file and edit as needed:**
   ```bash
   cp .env.example .env
   ```

4. **Start the database (if required):**
   - Using Docker Compose:
     ```bash
     docker-compose up -d
     ```

---

## Technologies Used

- **Backend:** C# (.NET)
- **Frontend:** React, TypeScript, JavaScript
- **Database:** (e.g., PostgreSQL/MySQL/MongoDB)
- **Docker & Docker Compose**
- **REST API**
- **AI Integration** (e.g., OpenAI API)
- **dotenv** for environment variable management

---

## Assumptions Made

- Users must be authenticated to save and view learning history.
- All sensitive credentials (e.g., API keys, database URIs) are provided via environment variables and not hardcoded.
- The AI service (e.g., OpenAI) is available and the API key is valid.
- The local environment uses Docker Compose for database setup (if a database is needed).

---

## How to Run Locally

### Run the Backend:
```bash
cd backend
dotnet run
```
Default: http://localhost:5000

### Run the Frontend:
```bash
cd frontend
npm start
```
Default: http://localhost:3000

### Database:
- Ensure the database is running (with Docker Compose: `docker-compose up`).

### Environment Variables:
- Make sure all environment variables are set correctly, using `.env` based on the provided `.env.example`.

---

## Sample .env Example

```
# Backend
DB_HOST=localhost
DB_PORT=5432
DB_USER=youruser
DB_PASS=yourpassword
JWT_SECRET=your_jwt_secret
OPENAI_API_KEY=your_openai_key

# Frontend
REACT_APP_API_URL=http://localhost:5000
```

---

Happy learning!
