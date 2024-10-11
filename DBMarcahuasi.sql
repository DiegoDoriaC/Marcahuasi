create database DBMarcahuasi

use DBMarcahuasi
go

create table Administrador
(
	IdAdministrado int primary key identity not null,
	Nombre varchar(25) not null,
	NumeroTelefono varchar(12) not null,
	Contraceña varchar(15) not null ,
	FechaModificacion datetime default getdate()
)
go

alter table administrador alter column NumeroTelefono varchar(12)

create table Departamentos
(
	IdDepartamento int primary key identity(1000, 1) not null,
	NombreDepartamento varchar(20)
)
go

create table Nacionalidades
(
	IdNacionalidad int identity(1000,1) primary key not null, 
	Pais varchar(30) not null,
	CodigoISO varchar(3) not null,
	UrlImagen varchar(550)not null,
)
go

create table Turistas
(
	IdTurista int identity primary key not null, 
	Nombres varchar(50) not null,
	Apellidos varchar(50) not null,
	NumeroDocumento varchar(9)not null,
	IdNacionalidad int references Nacionalidades,
	IdDepartamento int references Departamentos
)
go

create table TarifaPagos
(
	IdTarifa int identity(1000,1) primary key not null, 
	NombreTarifa varchar(25) not null,
	MontoTarifa decimal (10,2) not null,
)
go

create table Ingresos
(
	IdIngreso int identity primary key not null, 
	IdTurista int references Turistas,
	IdTarifa int references TarifaPagos,
	FechaIngreso datetime default getdate(),
	Modificado bit not null,
	FechaModificacion datetime default null,
	Observacion varchar (300),
	UserRegister varchar (180)not null,
)
go



	-------------------- Registros Insertados --------------------

insert into Nacionalidades Values 
	('Alemania', 'DEU', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Alemania.png'),
	('Argentina', 'ARG', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Argentina.png'),
	('Bolivia', 'BOL', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Bolivia.png'),
	('Brasil', 'BRA', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Brasil.png'),
	('Canadá', 'CAN', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Canada.png'),
	('Chile', 'CHL', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Chile.png'),
	('China', 'CHN', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/China.png'),
	('Colombia', 'COL', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Colombia.png'),
	('Corea del Sur', 'KOR', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Corea.png'),
	('Croacia', 'HRV', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Croacia.png'),
	('Ecuador', 'ECU', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Ecuador.png'),
	('España', 'ESP', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/España.png'),
	('Estados Unidos', 'USA', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Estados Unidos.png'),
	('Francia', 'FRA', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Francia.png'),
	('Italia', 'ITA', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Italia.png'),
	('Japón', 'JPN', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Japon.png'),
	('México', 'MEX', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Mexico.png'),
	('Países Bajos', 'NLD', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Paises bajos.png'),
	('Paraguay', 'PRY', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Paraguay.png'),
	('Perú', 'PER', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Peru.png'),
	('Reino Unido', 'GBR', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Reino Unido.png'),
	('Uruguay', 'URY', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Uruguay.png'),
	('Venezuela', 'VEN', 'https://marcahuasiblobstorage.blob.core.windows.net/banderas/Venezuela.png')
go

insert into Departamentos values 
('Amazonas'),
('Áncash'),
('Apurímac'),
('Arequipa'),
('Ayacucho'),
('Cajamarca'),
('Callao'),
('Cusco'),
('Huancavelica'),
('Huánuco'),
('Ica'),
('Junín'),
('La Libertad'),
('Lambayeque'),
('Lima'),
('Loreto'),
('Madre de Dios'),
('Moquegua'),
('Pasco'),
('Piura'),
('Puno'),
('San Martín'),
('Tacna'),
('Tumbes'),
('Ucayali')
go

insert into TarifaPagos values 
	('Nacional', 10),
	('Extranjero', 20)
go



	-------------------- Creacion de tipos --------------------

create type TypeTurista as table 
(
	Nombres varchar(50) not null,
	Apellidos varchar(50) not null,
	NumeroDocumento varchar(9)not null,
	IdNacionalidad int,
	IdDepartamento int
)
go


	-------------------- Procedimiento almacenado para Registrar Ingreso --------------------

create or alter procedure sp_registrarIngreso
	@p_tablaTurista TypeTurista readonly,
	@p_IdTarifa int
as
begin
	declare @IdTurista int
	insert into Turistas(Nombres, Apellidos, NumeroDocumento, IdNacionalidad, IdDepartamento)
	select Nombres, Apellidos, NumeroDocumento, IdNacionalidad, IdDepartamento from @p_tablaTurista
	set @IdTurista = @@IDENTITY

	insert into Ingresos (IdTurista, IdTarifa, Modificado, FechaModificacion, Observacion, UserRegister)
	values (@IdTurista, @p_IdTarifa, 0, null, null, HOST_NAME())
end
go

	-------------------- Procedimiento almacenado para Modificar Ingreso --------------------

create or alter procedure sp_actualizarIngreso
	@p_IdIngreso int,
	@p_IdTarifa int,
	@p_Observacion varchar (300)
as
begin
	update Ingresos set
	IdTarifa = @p_IdTarifa,
	Modificado = 1,
	FechaModificacion = GETDATE(),
	Observacion = @p_Observacion,
	UserRegister = HOST_NAME() where [IdIngreso] = @p_IdIngreso
end
go;


	-------------------- Registros de Prueba --------------------

	declare @TablaTuristaTest TypeTurista
	insert into @TablaTuristaTest([Nombres], [Apellidos], [NumeroDocumento], [IdNacionalidad], [IdDepartamento]) values
		('Diego', 'Doria', '72557870', 1019, 1014)
	exec sp_registrarIngreso @p_tablaTurista = @TablaTuristaTest,@p_IdTarifa = 1001



	exec sp_actualizarIngreso @p_IdIngreso = 1, @p_IdTarifa = 1000, @p_Observacion = 'Ocurrio una equivocación al registrar la tarifa, por error se le puso como extranjero cuando es turista nacional'
	


	select * from Ingresos
	select * from Nacionalidades
	select * from Departamentos
	select * from TarifaPagos
	select * from Turistas
	select * from Administrador

	update Administrador set
	Nombre = 'Diego Doria Crisostomo',
	NumeroTelefono = '928109364',
	Contraceña = '8b6aaf6d8999c00a62ab9027911120e1865d638cb9a6472a8a10833d3a615b56'


