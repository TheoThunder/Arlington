--Admins Permissions
INSERT INTO roles_permissions (role_id, permission_id) VALUES
	(1, 1),
	(1, 2),
	(1, 3),
	(1, 5),
	(1, 6),
	(1, 7),
	(1, 8);

--Manager Permissions
INSERT INTO roles_permissions (role_id, permission_id) VALUES
	(4, 1),
	(4, 2),
	(4, 3),
	(4, 5),
	(4, 6);

--SA Permissions
INSERT INTO roles_permissions (role_id, permission_id) VALUES
	(3, 1),
	(3, 2),
	(3, 3),
	(3, 4),
	(3, 6),
	(3, 9);

--AA Permissions
INSERT INTO roles_permissions (role_id, permission_id) VALUES
	(2, 1),
	(2, 2),
	(2, 3),
	(2, 4),
	(2, 9);