-- Table: uploadedfile

-- DROP TABLE uploadedfile;

CREATE TABLE uploadedfile
(
  uploadedfileid serial PRIMARY KEY,
  filename text,
  filetype text,
  filepath text,
  accountID int REFERENCES account (accountid),
  leadID int REFERENCES lead (leadid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE uploadedfile OWNER TO postgres;
