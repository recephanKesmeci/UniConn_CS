# UniConn - University Social Network System
# Project Overview
## UniConn is a dedicated social network platform designed for university students and societies. The system addresses the issue of fragmented communication by providing a centralized database for real-time event tracking, community management, and student engagement across multiple universities.
Key Features
	- Community Management: Create and manage university societies with specific roles and permissions.
	- Event Tracking: Browse and join campus events in real-time.
	- Engagement Tools: Participate in community polls, create discussion posts, and vote on options.
	- Academic Verification: Secure registration requiring a verified .edu institutional email address.

# Database Architecture
## The system utilizes a relational database designed to Third Normal Form (3NF) to ensure data integrity and minimize redundancy.
Core Entities
	- STUDENTS: Manages user profiles and academic status.
	- COMMUNITY: Groups representing specific interest areas.
	- EVENTS & POSTS: Stores real-time campus activities and social announcements.
	- POLL SYSTEM: Handles polls, options, and individual student votes.
	- ROLE: Defines permissions (Admin, Moderator, Member) within specific communities.

# Libraries Used
	- Database Engine: Microsoft SQL Server (MS SQL)
	- Backend & ORM: Entity Framework Core (EF Core)
	- Frontend Framework: ASP.NET Razor Pages
	- Architecture: ASP.NET MVC / Razor-based web application


Step-by-Step Instructions
1. Database Setup
	1. Open your SQL Database Management Tool (e.g., SQL Server Management Studio).
	2. Execute the table creation scripts in the following order to respect Foreign Key constraints :
		- STUDENTS and COMMUNITY
		- ROLE and EVENTS
		- POST, POLL, POLL_OPTION, VOTE
		- COMMUNITY_MEMBERSHIP and COMMUNITY_ROLE_ASSIGNMENT
	3. Run the provided index scripts (idx_student_email, etc.) to optimize search performance .
	4. Create the views (vw_active_members and vw_event_overview) for easy data reporting .
	5. Apply the trg_cleanup_roles_on_membership_delete trigger to automate role removal when a member leaves a community .
2. Configuration
	- Ensure your database connection string points to your local or hosted SQL instance.
	- Verify that your institutional email validation logic is active (the system strictly requires the .edu domain).
3. Running the Application
	1. Launch the Backend: Initialize your server-side environment from visual studio.
	2. Authentication: Register a new user using a .edu email address.
	3. Explore: Use the Student Dashboard to discover ongoing events or the Community Hub to join a society.
	4. Admin Access: Users with administrative privileges can access the Admin Dashboard to manage users, societies, and execute SQL commands
