-- Table: card

-- DROP TABLE card;

CREATE TABLE card
(
  cardid serial NOT NULL,
  cardtype text,
  "comment" text,
  createdon date,
  lastupdated date,
  leftvm boolean,
  numbercalled integer,
  talkedtodm boolean,
  talkedtooffm boolean,
  talkedtoother boolean,
  talkedtoperson boolean,
  creatorid integer,
  parentleadid integer,
  assignedaaid integer,
  appointmentsheetid integer,
  CONSTRAINT card_pkey PRIMARY KEY (cardid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE card OWNER TO postgres;
