-- Table: roles

-- DROP TABLE roles;

CREATE TABLE roles
(
  role_id serial NOT NULL,
  "name" text,
  CONSTRAINT roles_pkey PRIMARY KEY (role_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE roles OWNER TO postgres;


-- Table: permissions

-- DROP TABLE permissions;

CREATE TABLE permissions
(
  permission_id serial NOT NULL,
  "name" text,
  "action" text,
  CONSTRAINT permissions_pkey PRIMARY KEY (permission_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE permissions OWNER TO postgres;
