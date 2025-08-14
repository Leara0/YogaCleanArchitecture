Yoga App Database Setup

Prerequisites:
- MySql Server 8.0 or higher
- MySql Workbench (optional but recommended)

Database Setup Instructions
- Step 1: Run SQL Scripts - Execute the SQL files in the following order:
  - "CreateTablesAndBasicData.sql"
    - Creates the database schema
    - Sets up difficulty levels and yoga categories
    - Establishes all table relationships 

  - "PoseData"
    - Loads all 48 yoga poses with detailed information
    - Includes Sanskrit names, descriptions, and benefits
    - Maps poses to difficulty

- Step 2: Update Connection String - Update your 'appsettings.json' with your MySQL connection details:
  - {
    "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Yoga;Uid=yourusername;Pwd=yourpassword;"
    }
    }

Data Attribution
- The pose data used in this application is adapted from the 'Yoga API project' by Alex Cumplido.
- Original Data Source: https://github.com/alexcumplido/yoga-api
- Modifications Made:
  - Converted from original database to MySql
  - Adapted table structure for Clean Architecture implementation
  - Maintained all original Sanskrit names, descriptions and pose benefits
  - Restructured relationships for optimal MVC usage
- Special thanks to Alex Cumplido for compiling this comprehensive collection of yoga pose information

Database Schema
- Tables
  - Difficulty: Skill levels (Beginner, Intermediate, Expert)
  - Categories: 12 yoga pose categories with descriptions
  - Poses: 48 detailed yoga poses with Sanskrit names and benefits
  - Pose Mapping: Many-to-many relationship between poses and categories

Features:
- This database supports a full-features yoga pose management system with:
  - Pose browsing by difficulty and category
  - Detailed pose information including benefits
  - SVG image support