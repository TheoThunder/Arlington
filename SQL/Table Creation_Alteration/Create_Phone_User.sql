

CREATE TABLE phone_user	
(
  phoneuserid serial NOT NULL,
  extension integer,
  firstname text,
  middlename text,
  lastname text,
  crmuserid integer,
  accountid integer,
  voicemailid integer,
  priority text,
  date_created timestamp without time zone,
  extension_server_uuid integer
)
WITH (
  OIDS=FALSE
);
ALTER TABLE phone_user OWNER TO postgres;
