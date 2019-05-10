use EveProjectTest
GO
Create Table PlayerLocation

(
	CurrentSystem	nvarchar NOT NULL, --link to ESI  SysID
	SystemName		nvarchar NOT NULL, 
	StarType		nvarchar NOT NULL, 
	SystemBonus     nvarchar NOT NULL, --link to ESI SysBonus
	--Link to ESI SystemSignatures table
	

)

insert into PlayerLocation
VALUES
('TESTKEY', 'TESTNAME', 'TESTSTAR', 'TESTBONUS');


GO
create table SystemReports
(
	ReportID    nvarchar not null, --PK
	SystemID	nvarchar not null, --FK Link to ESI SysID
	Notes		NText Not null, 
	timeGenerated	Timestamp not null, 
	timeExpires datetime not null, 
	--Link to ESI SysID
	PRIMARY KEY (ReportID)
	
)

insert into SystemReports
VALUES
('TESTreportID', 'TESTSystemID', 'TestNotes blah blah bad dude kill on sight blah blah', 
 CURRENT_TIMESTAMP, '2020-5-5');

GO
create table CharacterData
(
	CharacterID		nvarchar not null, --PK
	ReportID			nvarchar Not null, --FK link to CharacterReporths
	-- link ESI CharEmploymentHistory   
	-- link ESI  CharWalletTransactions 
	PRIMARY KEY (CharacterID)
)
INSERT INTO CharacterData
VALUES ('TESTcharID', 'TESTReportID')


GO
create table CharEmploymentHistory
(
	CharacterID		nvarchar not null, 
	EmploymentHistory Ntext not null, --link to table
	PRIMARY KEY (CharacterID)
)

INSERT INTO CharEmploymentHistory
VALUES ('TESTcharID', 'TESTlinkToDatabasethatkeepcrasshingmypc')

GO
create table CharWalletTransactions
(
	CharacterID  nvarchar not null, 
	WalletTransactions Ntext not null,  -- link to table
	PRIMARY KEY (CharacterID)
)
INSERT INTO CharWalletTransactions
VALUES ('TESTcharID', 'TESTlinkToDatabasethatkeepcrasshingmypc')


GO


create table SystemSignatures
(
	SigID nvarchar not null, --PK  --link to ESI SignatureType
	Scanned bit not null,
	SigType ntext not null, 
	
	PRIMARY KEY (SigID)
)
INSERT INTO SystemSignatures
VALUES ('TestSigID', 1, 'TESTtypeGasSite')

create table CharacterReports
(
	ReportID         nvarchar not null, --PK
	ReportBody       ntext not null, 
	timeGenerated	 Timestamp not null, 
	timeExpires		 datetime null, 

	PRIMARY KEY (ReportID)
)

Insert Into CharacterReports
values ('TESTreportID', 'Here is the text, its a bad dude really bad, get em', CURRENT_TIMESTAMP, '2020-5-5'

