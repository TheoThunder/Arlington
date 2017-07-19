-- Table: users

-- DROP TABLE users;

CREATE TABLE users
(
  user_id serial NOT NULL,
  username text,
  "password" text,
  first_name text,
  middle_name text,
  last_name text,
  address1 text,
  address2 text,
  city text,
  state text,
  zipcode text,
  assigned_role_id integer,
  office_number integer,
  sales_rep_number integer,
  calendar_color text,
  phone1 text,
  phone2 text,
  faxnumber text,
  wage real,
  CONSTRAINT users_pkey PRIMARY KEY (user_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE users OWNER TO postgres;
