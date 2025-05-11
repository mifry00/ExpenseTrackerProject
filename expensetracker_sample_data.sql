
-- Insert Users
INSERT INTO users (id, email, password_hash, is_admin, created_at) VALUES (10, 'admin@et.com', 'admin123', true, '2025-04-29 16:06:58.851898');
INSERT INTO users (id, email, password_hash, is_admin, created_at) VALUES (37, 'eva@et.com', 'pass123', false, '2025-05-07 11:14:47.523056');
INSERT INTO users (id, email, password_hash, is_admin, created_at) VALUES (38, 'mille@et.com', 'Password123', false, '2025-05-07 11:15:07.903443');
INSERT INTO users (id, email, password_hash, is_admin, created_at) VALUES (39, 'victoria@et.com', 'hello123', false, '2025-05-07 11:15:23.13712');

-- Insert Expenses
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (12, 38, 97.00, 'Travel', '2025-05-06 00:00:00', true, '2025-05-07 11:16:54.536445', 'Train tickets København H - Malmö');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (13, 38, 1000.00, 'Food', '2025-05-06 00:00:00', true, '2025-05-07 11:17:26.730756', 'Restaurant with customers');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (14, 38, 97.00, 'Travel', '2025-07-06 00:00:00', true, '2025-05-07 11:17:45.303685', 'Train tickets Malmö - København H');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (16, 38, 500.00, 'Utilities', '2025-05-07 00:00:00', false, '2025-05-07 20:39:28.589909', 'Office supplies');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (19, 37, 900.00, 'Utilities ', '2025-05-01 00:00:00', false, '2025-05-09 18:59:49.871514', 'Internet bill - May');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (22, 39, 250.00, 'Travel', '2025-05-09 00:00:00', false, '2025-05-09 19:08:52.995744', 'Taxi to escape room');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (23, 39, 1500.00, 'Team building', '2025-05-09 00:00:00', false, '2025-05-09 19:09:45.995039', 'Escape room for team building ');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (24, 39, 2500.00, 'Food', '2025-05-09 00:00:00', false, '2025-05-09 19:10:28.286315', 'Restaurant after team day');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (21, 39, 1000.00, 'Food', '2025-05-09 00:00:00', true, '2025-05-09 19:06:28.967329', 'Sandwiches and drinks for team day');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (20, 39, 3000.00, 'Training', '2025-05-01 00:00:00', true, '2025-05-09 19:05:52.07044', 'Online course - Project management seminar');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (17, 37, 1500.00, 'Software', '2025-05-09 00:00:00', true, '2025-05-09 18:58:17.548763', 'Alteryx subscription ');
INSERT INTO expenses (id, user_id, amount, category, expense_date, is_approved, created_at, description) VALUES (18, 37, 2000.00, 'Marketing', '2025-05-09 00:00:00', true, '2025-05-09 18:58:51.969519', 'Posters');
