# BriefingTime

## Project Overview
BriefingTime is a full-stack application designed for the management and distribution of professional briefings. The system utilizes a containerized architecture to ensure consistency across development and production environments. It features a high-performance .NET backend and a reactive Angular frontend.

---

## Technical Stack

### Backend
* **Framework:** **.NET 8+** (Web API)
* **Database:** **PostgreSQL**
* **ORM:** **Entity Framework Core**
* **Identity:** **Microsoft ASP.NET Core Identity**
* **Security:** **JWT (JSON Web Tokens)** stored in **Secure HttpOnly Cookies**

### Frontend
* **Framework:** **Angular 18+**
* **State Management:** **Angular Signals**
* **Communication:** **HttpClient** with **Functional Interceptors**
* **Type Safety:** **TypeScript**

### Infrastructure
* **Containerization:** **Docker** and **Docker Compose**
* **Database Management:** **DBeaver**
* **API Testing:** **Postman**

---

## Getting Started

### Prerequisites
* **Docker Desktop**
* **Node.js** (for local frontend development)
* **.NET SDK**

### Installation

**Clone the repository:**
   ```bash
   git clone [https://github.com/your-username/briefing-time.git](https://github.com/your-username/briefing-time.git)
   cd briefing-time
   ```

**Start the Frontend:**
   ```bash
   docker-compose up --build
   ```
    
**Start the Frontend:**
   ```bash
   npm install
   npm start
   ```

### Authentication and Security
**The project implements a hardened authentication flow:**
* **Storage:** Authentication tokens are stored in HttpOnly, Secure, and SameSite=Strict cookies to mitigate XSS and CSRF risks.
* **Refresh Strategy:** A Refresh Token rotation mechanism is used to maintain user sessions without persisting sensitive credentials.
* **Frontend Integration:** Angular interceptors are configured with `withCredentials: true` to allow automatic cookie transmission between the client and the Dockerized API.

### Database Configuration
**The PostgreSQL instance runs within a Docker container. To manage the data:**
* **Host:** localhost
* **Port:** 5432
* **Database Name:** BriefingTimeDb
* **Tool:** Use DBeaver or similar for schema visualization and manual queries.

---

## License
This project is licensed under the **GPL-3.0 License**. See the `LICENSE` file for details.
