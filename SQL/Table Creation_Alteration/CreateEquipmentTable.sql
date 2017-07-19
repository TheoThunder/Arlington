-- Table: equipment

-- DROP TABLE equipment;

CREATE TABLE equipment
(
  equipmentid serial NOT NULL,
  equipmentname text,
  equipmenttype text,
  active boolean
  
  
)
WITH (
  OIDS=FALSE
);
ALTER TABLE equipment OWNER TO postgres;
