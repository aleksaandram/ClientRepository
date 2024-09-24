if not exists (select 'a' from sys.sysobjects where name = 'Clients' and xtype = 'U')
	create table Clients (
		ClientID int Primary key, --id na klient
		FullName nvarchar(200), --ime prezime na klient
		BirthDate date         --datum na raganje
	)
go

if not exists (select 'a' from sys.sysobjects where name = 'AdressBook' and xtype = 'U')
	create table AdressBook (
	    AdressID int primary key, --id vo adresar
		ClientID int foreign key references Clients(ClientID), --id na klient
		AdressType int, --tip na adresa
		FullAddress varchar(200)   --celosna adresa
	)
go