-- Table: zones

-- DROP TABLE zones;

CREATE TABLE zones
(
  zone_id serial NOT NULL,
  zone_number integer,
  CONSTRAINT zones_pkey PRIMARY KEY (zone_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE zones OWNER TO postgres;

-- Table: zipcodes

-- DROP TABLE zipcodes;

CREATE TABLE zipcodes
(
  zipcode_id serial NOT NULL,
  zipcode integer,
  zone_id integer,
  CONSTRAINT zipcodes_pkey PRIMARY KEY (zipcode_id),
  CONSTRAINT zipcodes_zone_id_fkey FOREIGN KEY (zone_id)
      REFERENCES zones (zone_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE
)
WITH (
  OIDS=FALSE
);
ALTER TABLE zipcodes OWNER TO postgres;
