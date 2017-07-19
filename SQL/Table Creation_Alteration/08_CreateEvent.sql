-- Table: event

-- DROP TABLE event;

CREATE TABLE event
(
  id character varying(30),
  "name" character varying(50),
  eventstart timestamp without time zone,
  eventend timestamp without time zone,
  resource character varying(50)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE event OWNER TO postgres;
