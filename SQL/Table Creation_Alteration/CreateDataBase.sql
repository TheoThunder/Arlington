-- Table: aaps_mbsreport

-- DROP TABLE aaps_mbsreport;

CREATE TABLE aaps_mbsreport
(
  mbsreport_id serial NOT NULL,
  "month" text,
  "year" integer,
  calls integer,
  appointments integer,
  goodapts integer,
  closes integer,
  accounts integer,
  aauserid integer,
  sauserid integer,
  CONSTRAINT mbsreport_pkey PRIMARY KEY (mbsreport_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE aaps_mbsreport OWNER TO postgres;

-- Table: account

-- DROP TABLE account;

CREATE TABLE account
(
  accountid serial NOT NULL,
  merchantid text,
  accountname text,
  aacreator integer,
  assignedsalesrep integer,
  salesrepnumber text,
  officenumber text,
  status text,
  accountapprovaldate timestamp without time zone,
  annualfee boolean,
  estimatedmonthlyvolume text,
  ht text,
  hmv text,
  platform text,
  vendor text,
  vip boolean,
  mbp boolean,
  freesupplies boolean,
  pcirefund boolean,
  mailingstreet text,
  mailingcity text,
  mailingstate text,
  mailingzipcode text,
  dbastreet text,
  dbacity text,
  dbastate text,
  dbazipcode text,
  primaryemail text,
  secondaryemail text,
  website text,
  credit boolean,
  debit boolean,
  arb boolean,
  cim boolean,
  ip boolean,
  giftcardprocessor text,
  secur_chex text,
  software text,
  ecommerce text,
  primaryterminal integer,
  primaryterminalowner text,
  primaryterminalquantity integer,
  secondaryterminal integer,
  secondaryterminalowner text,
  secondaryterminalquantity integer,
  checkequipment integer,
  checkequipmentowner text,
  checkequipmentquantity integer,
  primarypinpad integer,
  primarypinpadowner text,
  primarypinpadquantity integer,
  secondarypinpad integer,
  secondarypinpadowner text,
  secondarypinpadquantity integer,
  printer integer,
  priterowner text,
  description text,
  uploadfiles integer,
  parentlead integer,
  primaryphone text,
  secondaryphone text,
  faxnumber text,
  CONSTRAINT accountid_pkey PRIMARY KEY (accountid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE account OWNER TO postgres;

-- Table: appointmentsheet

-- DROP TABLE appointmentsheet;

CREATE TABLE appointmentsheet
(
  appointmentid serial NOT NULL,
  addingservices boolean,
  appointmentlocation text,
  assignedsalesagent integer,
  city text,
  "comment" text,
  createdat timestamp without time zone,
  currentlyacceptedcards boolean,
  currentprocessor text,
  dateofappointment timestamp without time zone,
  howmanylocations integer,
  internet boolean,
  lastupdated timestamp without time zone,
  moto boolean,
  multilocation boolean,
  newequipment boolean,
  newsetup boolean,
  price boolean,
  score text,
  singlelocation boolean,
  state text,
  street text,
  swipe boolean,
  unhappy boolean,
  volume text,
  zipcode integer,
  creator integer,
  parentlead integer,
  "location" text,
  appointmentdatefrom timestamp without time zone,
  appointmentdateto timestamp without time zone,
  reschedule boolean,
  creatorname text,
  singleloccheck boolean,
  event_reference text,
  CONSTRAINT appointmentid PRIMARY KEY (appointmentid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE appointmentsheet OWNER TO postgres;


-- Table: card

-- DROP TABLE card;

CREATE TABLE card
(
  cardid serial NOT NULL,
  cardtype text,
  "comment" text,
  createdon timestamp without time zone,
  lastupdated timestamp without time zone,
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
  reassigned boolean,
  creatorname text,
  cardcallbackdate timestamp without time zone,
  CONSTRAINT card_pkey PRIMARY KEY (cardid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE card OWNER TO postgres;


-- Table: equipment

-- DROP TABLE equipment;

CREATE TABLE equipment
(
  equipmentid serial NOT NULL,
  equipmentname text,
  equipmenttype text,
  active boolean,
  CONSTRAINT pkey_equipmentid PRIMARY KEY (equipmentid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE equipment OWNER TO postgres;


-- Table: event

-- DROP TABLE event;

CREATE TABLE event
(
  eventid serial NOT NULL,
  title text,
  "type" text,
  description text,
  appointment boolean,
  personal boolean,
  city text,
  state text,
  zip integer,
  "zone" integer,
  map text,
  creatorid integer,
  assigneduserid integer,
  starttime text,
  endtime text,
  street text,
  parent_user_id integer,
  parent_appointment_id integer,
  appointment_reference text
)
WITH (
  OIDS=FALSE
);
ALTER TABLE event OWNER TO postgres;



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
  ignored boolean,
  dateimported timestamp without time zone,
  reassigned boolean,
  primaryphonechecked boolean,
  CONSTRAINT lead_pkey PRIMARY KEY (leadid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE lead OWNER TO postgres;


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


-- Table: roles_permissions

-- DROP TABLE roles_permissions;

CREATE TABLE roles_permissions
(
  role_id integer NOT NULL,
  permission_id integer NOT NULL,
  CONSTRAINT roles_permissions_pkey PRIMARY KEY (role_id, permission_id),
  CONSTRAINT roles_permissions_permission_id_fkey FOREIGN KEY (permission_id)
      REFERENCES permissions (permission_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT roles_permissions_role_id_fkey FOREIGN KEY (role_id)
      REFERENCES roles (role_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE roles_permissions OWNER TO postgres;


-- Table: threshold

-- DROP TABLE threshold;

CREATE TABLE threshold
(
  thresholdid serial NOT NULL,
  uppercalendar integer,
  lowercalendar integer,
  we_upperdashboard integer,
  we_lowerdashboard integer,
  nc_upperdashboard integer,
  nc_lowerdashboard integer,
  CONSTRAINT pkey_thresholdid PRIMARY KEY (thresholdid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE threshold OWNER TO postgres;


-- Table: ticket

-- DROP TABLE ticket;

CREATE TABLE ticket
(
  ticketid serial NOT NULL,
  ticket_number integer,
  customer_name text,
  subject text,
  tstatus text,
  creator integer,
  current_owner integer,
  "zone" integer,
  priority text,
  date_opened timestamp without time zone,
  account_name text,
  last_updated timestamp without time zone,
  ticket_type text,
  reason text,
  ticket_origin text,
  received_from text,
  tdescription text,
  effective_date timestamp without time zone,
  comments text,
  "action" text,
  taccountid integer,
  date_closed timestamp without time zone,
  callback_number text,
  tickethistoryid text,
  closedby integer
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ticket OWNER TO postgres;


-- Table: tickethistory

-- DROP TABLE tickethistory;

CREATE TABLE tickethistory
(
  historyid serial NOT NULL,
  historydate timestamp without time zone,
  userworked integer,
  haction text,
  "comment" text,
  hticketid text
)
WITH (
  OIDS=FALSE
);
ALTER TABLE tickethistory OWNER TO postgres;


-- Table: time_slots

-- DROP TABLE time_slots;

CREATE TABLE time_slots
(
  timeslot_id serial NOT NULL,
  num_available_sa integer,
  color text,
  start_time text,
  end_time text,
  title integer,
  all_day boolean,
  parent_user_id integer,
  "zone" integer,
  CONSTRAINT timeslot_pkey PRIMARY KEY (timeslot_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE time_slots OWNER TO postgres;


-- Table: trinity_logs

-- DROP TABLE trinity_logs;

CREATE TABLE trinity_logs
(
  logid serial NOT NULL,
  date timestamp without time zone,
  thread text,
  "level" text,
  logger text,
  message text,
  exception text
)
WITH (
  OIDS=FALSE
);
ALTER TABLE trinity_logs OWNER TO postgres;


-- Table: uploadedfile

-- DROP TABLE uploadedfile;

CREATE TABLE uploadedfile
(
  uploadedfileid serial NOT NULL,
  filename text,
  filetype text,
  filepath text,
  accountid integer,
  leadid integer,
  CONSTRAINT uploadedfile_pkey PRIMARY KEY (uploadedfileid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE uploadedfile OWNER TO postgres;


-- Table: users

-- DROP TABLE users;

CREATE TABLE users
(
  user_id serial NOT NULL,
  username text,
  "password" character varying,
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
  email1 text,
  email2 text,
  isactive boolean,
  team integer,
  CONSTRAINT users_pkey PRIMARY KEY (user_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE users OWNER TO postgres;


-- Table: userzones

-- DROP TABLE userzones;

CREATE TABLE userzones
(
  user_id integer,
  zone_id integer,
  userzoneid serial NOT NULL
)
WITH (
  OIDS=FALSE
);
ALTER TABLE userzones OWNER TO postgres;


-- Table: zipcodes

-- DROP TABLE zipcodes;

CREATE TABLE zipcodes
(
  zipcode_id serial NOT NULL,
  zipcode integer,
  zoneid integer,
  CONSTRAINT zipcode_pkey PRIMARY KEY (zipcode_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE zipcodes OWNER TO postgres;


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
