CREATE DATABASE IF NOT EXISTS Yoga;
USE Yoga;

DROP TABLE IF EXISTS pose_mapping;
DROP TABLE IF EXISTS categories;
DROP TABLE IF EXISTS poses;
DROP TABLE IF EXISTS difficulty;

-- Create difficulty table
CREATE TABLE difficulty (
                            difficulty_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            difficulty_level TEXT NOT NULL
);

-- Create categories table
CREATE TABLE categories (
                            category_id INT AUTO_INCREMENT PRIMARY KEY,
                            category_name TEXT NOT NULL,
                            category_description TEXT NOT NULL
);

--Create poses table
CREATE TABLE poses (
                       pose_id INT NOT NULL AUTO_INCREMENT,
                       english_name TEXT NOT NULL,
                       sanskrit_name_adapted TEXT,
                       sanskrit_name TEXT,
                       translation_name TEXT,
                       pose_description TEXT,
                       pose_benefits TEXT,
                       difficulty_id INT,
                       url_svg TEXT,
                       url_png TEXT,
                       url_svg_alt TEXT,
                       PRIMARY KEY (pose_id),
                       FOREIGN KEY (difficulty_id) REFERENCES difficulty(difficulty_id)
);

-- Create pose mapping table
CREATE TABLE pose_mapping (
                              mapping_id INT AUTO_INCREMENT PRIMARY KEY,
                              pose_id INT NOT NULL,
                              category_id INT,
                              FOREIGN KEY (pose_id) REFERENCES poses(pose_id),
                              FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

-- Insert difficulty table data
INSERT INTO difficulty (difficulty_level) VALUES
                                                  ('Beginner'),
                                                  ('Intermediate'),
                                                  ('Expert');

-- Insert category table data
INSERT INTO categories (`category_name`, `category_description`) VALUES
('Core Yoga', 'Engage your abdominal muscles with core yoga poses that build a strong and stable center.'),
('Seated Yoga', 'Yoga practice with seated poses that help you find better alignment, increase your flexibility, and relieve lower back pain and discomfort. Tone the belly, massage your internal organs, and relieve lower back pain in these seated yoga poses.'),
('Strengthening Yoga', 'Work and tone your entire body with strengthening yoga poses.'),
('Chest Opening Yoga', 'Open your heart and shoulders in chest opening yoga poses.'),
('Backbend Yoga', 'Discover the powerful effects of yoga backbends with step-by-step instructions, sequences, and expert advice to keep your practice pain-free.'),
('Forward Bend Yoga', 'Learn how to work stiff muscles safely, promote lower-body flexibility, and find correct alignment with forward bend yoga poses.'),
('Hip Opening Yoga', 'Loosen tight hips, improve your range of motion and circulation, alleviate back pain and more in these hip-opening yoga poses.'),
('Standing Yoga', 'Develop strength and stability in your standing poses, and feel the benefits throughout your practice. Build strength and set the foundation for a safe yoga practice.'),
('Restorative Yoga', 'Restorative yoga focuses on winding down after a long day and relaxing your mind. At its core, this style focuses on body relaxation. You spend more time in fewer postures throughout the class. Many of the poses are modified to be easier and more relaxing. Restorative yoga also helps to cleanse and free your mind.'),
('Arm Balance Yoga', 'Move past fear, build better balance, and strengthen your body with arm balance yoga poses like Crane Pose, Plank Pose, Firefly Pose amd more.'),
('Balancing Yoga', 'Build a strong foundation for your asana practice with these balancing yoga poses. Get step-by-step instructions and reap the benefits.'),
('Inversion Yoga', 'Master inversions—overcome fear and discover how to defy gravity with these step-by-step instructions. Learn how to prepare for and stay safe in inversion yoga poses.');


-- Insert pose mapping data
INSERT INTO pose_mapping (pose_id, category_id) VALUES
                                                    (1, 1),
                                                    (1, 2),
                                                    (1, 3),
                                                    (2, 1),
                                                    (2, 2),
                                                    (2, 3),
                                                    (3, 4),
                                                    (3, 5),
                                                    (5, 2),
                                                    (5, 6),
                                                    (5, 7),
                                                    (6, 4),
                                                    (6, 5),
                                                    (7, 1),
                                                    (8, 4),
                                                    (8, 5),
                                                    (9, 1),
                                                    (9, 3),
                                                    (9, 8),
                                                    (10, 6),
                                                    (10, 7),
                                                    (10, 9),
                                                    (11, 9),
                                                    (12, 8),
                                                    (13, 1),
                                                    (13, 10),
                                                    (14, 1),
                                                    (14, 3),
                                                    (14, 8),
                                                    (15, 3),
                                                    (15, 6),
                                                    (15, 8),
                                                    (16, 8),
                                                    (16, 7),
                                                    (16, 11),
                                                    (17, 8),
                                                    (17, 7),
                                                    (17, 11),
                                                    (18, 3),
                                                    (18, 8),
                                                    (19, 3),
                                                    (19, 12),
                                                    (20, 6),
                                                    (21, 8),
                                                    (21, 11),
                                                    (22, 3),
                                                    (22, 11),
                                                    (22, 8),
                                                    (23, 8),
                                                    (24, 5),
                                                    (25, 5),
                                                    (25, 7),
                                                    (26, 1),
                                                    (26, 3),
                                                    (26, 10),
                                                    (27, 12),
                                                    (28, 6),
                                                    (28, 8),
                                                    (28, 3),
                                                    (29, 8),
                                                    (30, 2),
                                                    (30, 6),
                                                    (31, 2),
                                                    (32, 2),
                                                    (32, 7),
                                                    (33, 11),
                                                    (33, 12),
                                                    (34, 1),
                                                    (34, 10),
                                                    (34, 11),
                                                    (35, 4),
                                                    (35, 5),
                                                    (36, 2),
                                                    (37, 8),
                                                    (38, 6),
                                                    (39, 8),
                                                    (39, 11),
                                                    (40, 2),
                                                    (40, 6),
                                                    (40, 7),
                                                    (41, 8),
                                                    (41, 11),
                                                    (42, 3),
                                                    (42, 8),
                                                    (43, 4),
                                                    (43, 5),
                                                    (44, 3),
                                                    (44, 8),
                                                    (45, 3),
                                                    (45, 8),
                                                    (46, 3),
                                                    (46, 8),
                                                    (46, 11),
                                                    (47, 3),
                                                    (47, 4),
                                                    (47, 5),
                                                    (48, 4),
                                                    (48, 10);