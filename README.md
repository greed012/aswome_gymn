<h2>Gymn Website - ASP.NET MVC Project</h2>

<h3>Introduction</h3>
Aswome Gymn is a website built using C# ASP.NET mvc framework, for the college web development project. The project was a very good practice for my data base skills since all the components had to be related to eachother in one or multiple ways. I have attached the ERD as well as the relations between each element and user manual at the end.

<h3>Features</h3>

1. User Registration and Login
 Users, both members and fitness center employees, can securely create accounts using simple registration. Passwords are stored in hashes.

2. Member Profile Management
Members have control over their profiles, allowing them to update login credentials and contact information.

4. Membership Management
Staff members have access to managing memberships, including creation, modification, and deletion. Different membership types (monthly, quarterly, annual) are available for members to view and manage.

5. Session Management for Gym Classes
The system includes session management features for gym classes. Staff can create classes, set schedules, and establish class limits. Members can register for classes, view schedules, and track their attendance.

6. Trainer Management
In compliance with gym requirements, a trainer management component is integrated. Staff can administer trainer profiles and schedules, providing detailed information about the training staff.

7. User Roles and Authorization
To ensure secure access based on responsibilities, user roles and authorization have been implemented. Staff members have broader access to manage memberships, classes, and trainers, while members have limited access to their profiles and class schedules. This strict authorization system enhances system security and data integrity.


<h3>Acknowledgments</h3>
Special thanks to the ASP.NET MVC community,microsoft asp.net documentation and resources for providing valuable insights during the development of this project.

<br>
<H3>ERD</H3>
![erd](https://github.com/greed012/aswome_gymn/assets/93044288/168b8d34-986d-447a-bed8-1a5cedacc98e)
<h3>Entities:</h3>
• Users - Stores information about users like id, email, password, role etc.

• Classes - Stores information about classes like id, name, description etc.

• Memberships - Stores information about membership plans like id, name, price etc.

• Trainers - Stores information about trainers like id, name, social media links etc.

• Training_Sessions - Stores information about training sessions like id, class, trainer, time etc.

• Enrollments - Stores enrollment information like user, class, session etc.

• Attendance - Stores attendance information like user, session, class, attendance etc.

• Membership_Enrollments - Stores membership enrollments like user, membership etc.

<h3>Relationships:</h3>
<h4>One-to-Many:</h4>
• Classes can have multiple Training_Sessions (one class to many sessions)

• Trainers can have multiple Training_Sessions (one trainer to many sessions)

• Users can have multiple Enrollments (one user to many enrollments)

• Classes can have multiple Enrollments (one class to many enrollments)

• Training_Sessions can have multiple Enrollments (one session to many enrollments)

• Users can have multiple Attendance records (one user to many attendance)

• Training_Sessions can have multiple Attendance records (one session to many attendance)

• Classes can have multiple Attendance records (one class to many attendance)

• Users can have multiple Membership_Enrollments (one user to many membership enrollments)

• Memberships can have multiple Membership_Enrollments (one membership to many enrollments)

<h4>Many-to-One:</h4>
• Training_Sessions have one Class (many sessions to one class)

• Training_Sessions have one Trainer (many sessions to one trainer)

• Enrollments have one User (many enrollments to one user)

• Enrollments have one Class (many enrollments to one class)

• Enrollments have one Training_Session (many enrollments to one session)

• Attendance have one User (many attendance to one user)

• Attendance have one Training_Session (many attendance to one session)

• Attendance have one Class (many attendance to one class)

• Membership_Enrollments have one User (many enrollments to one user)

• Membership_Enrollments have one Membership (many enrollments to one membership)

<h3>User Manual</h3>
[user_manual.pdf](https://github.com/greed012/aswome_gymn/files/13720506/user_manual.pdf)
