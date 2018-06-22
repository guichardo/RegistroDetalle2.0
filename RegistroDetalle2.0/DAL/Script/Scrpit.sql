create database ArticulosDb
go
use ArticulosDb
go
create table Articulos
(

	ArticuloId int primary key identity(1,1),
	Descripcion varchar(30),
	Precio int,
	CantidadCotizada int,
	FechaVencimiento DateTime

);
