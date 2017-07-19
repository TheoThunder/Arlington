-- Table: lead

-- DROP TABLE lead;

CREATE TABLE lead
(
  leadid serial NOT NULL,
  companyname text,
  contact1title text,
  contact1firstname text,
  contact2title text,
  contact2firstname text,
  primaryphonenumber text,
  additionalphonenumber text,
  numbertocall integer,
  faxnumber text,
  primaryemailaddress text,
  additionalemailaddress text,
  websitelink text,
  streetaddress1 text,
  streetaddress2 text,
  city text,
  state text,
  zipcode text,
  zonenumber integer,
  status text,
  assignedsauserid integer,
  callbackdate date,
  ignoreddate date,
  assignedaauserid integer,
  contact1lastname text,
  contact2lastname text,
  suppressed boolean,
  CONSTRAINT lead_pkey PRIMARY KEY (leadid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE lead OWNER TO postgres;
