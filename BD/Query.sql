﻿--CREATE DATABASE
create database HBRTEST
go

use HBRTEST
go

--CREATE TABLES
	--Table Users
create table dtUsers (
UserID int not null identity(1,1),
FirstName varchar(40) not null,
LastName varchar(40) not null,
CellPhone varchar(12) not null,
Genre char(1) not null, 
Email nvarchar(40) not null, 
UserName nvarchar(10) not null,
Password nvarchar(max) not null,
CreationDate datetime not null,
LastModificationDate datetime not null,
Active bit not null,
constraint PK_dtUsers primary key(UserID),
constraint CK_dtUsers check(Genre in ('F','M')),
constraint IDX_dtUsers unique (UserName)
)
go

	--Table Categories
create table dtCategories(
CategoryID int not null identity(1,1),
CategoryName varchar(30) not null, 
Description nvarchar(max), 
CreationDate datetime not null,
LastModificationDate datetime not null,
Active bit not null,
constraint PK_dtCategories primary key(CategoryID),
constraint IDX_dtCategories unique(CategoryName)
)
go


	--Table Products
create table dtProducts(
ProductID int not null identity(1,1),
CategoryID int not null,
ProductName varchar(30) not null,
ProductImage nvarchar(max),
Description nvarchar(max),
Existence int not null,
Price decimal(16,2),
CreationDate datetime not null,
LastModificationDate datetime not null,
Active bit not null,
constraint PK_dtProducts primary key(ProductID),
constraint FK_dtProducts foreign key(CategoryID) references dtCategories(CategoryID)
)
go

--User Table Operations
create procedure CreateUser(@FirstName as varchar(40), @LastName as varchar(40), 
@CellPhone as varchar(20), @Genre as char(1),@Email as nvarchar(40), @UserName as nvarchar(10), @Password as nvarchar(max), 
@CreationDate as datetime, @LastModificationDate as datetime, @Active as bit)
as
begin 
	insert into dtUsers values(@FirstName, @LastName, @CellPhone, @Genre, @Email, @UserName, @Password, 
	@CreationDate, @LastModificationDate, @Active)
end
go

create procedure UpdateUser(@UserID as int, @FirstName as varchar(40), @LastName as varchar(40), 
@CellPhone as varchar(20), @Genre as char(1),@Email as nvarchar(40), @Password as nvarchar(max),  
@LastModificationDate as datetime, @Active as bit)
as
begin
	update dtUsers set FirstName=@FirstName, LastName=@LastName, CellPhone=@CellPhone,
	Genre=@Genre, Email=@Email, Password=@Password, 
	LastModificationDate=@LastModificationDate, Active=@Active where UserID=@UserID
end
go

create procedure GetUserByUserNameAndPassword(@UserName as nvarchar(10), @Password as nvarchar(max))
as
begin
	select UserId, FirstName, LastName, CellPhone, Genre, Email, UserName, Password, 
	CreationDate, LastModificationDate, Active from dtUsers where UserName=@UserName and Password=@Password
end
go

create procedure GetUserById(@UserID as int)
as
begin
	select UserId, FirstName, LastName, CellPhone, Genre, Email, UserName, Password, 
	CreationDate, LastModificationDate, Active from dtUsers where UserID=@UserID
end
go

create procedure ValidateIfUserExists(@UserName as varchar)
as
begin
	select UserName from dtUsers where UserName=@UserName
end
go

--Category Table Operations
create procedure CreateCategory(@CategoryName as varchar(30), @Description as nvarchar(max), 
@CreationDate as datetime, @LastModificationDate as datetime, @Active as nvarchar(10))
as
begin	
	insert into dtCategories values(@CategoryName, @Description, @CreationDate, @LastModificationDate, @Active)
end
go

create procedure UpdateCategory(@CategoryID as int, @CategoryName as nvarchar(30), @Description as nvarchar(max),
@LastModificationDate as datetime, @Active as bit)
as
begin
	update dtCategories set CategoryName=@CategoryName, Description=@Description, 
	LastModificationDate= @LastModificationDate, Active=@Active where CategoryID=@CategoryID
end
go

create procedure DeleteCategory(@CategoryID as int)
as
begin
	delete from dtCategories where CategoryID=@CategoryID
end
go

create procedure GetCategoryById(@CategoryID as int)
as
begin
	select CategoryID, CategoryName, Description, CreationDate, LastModificationDate, Active  
	from dtCategories where CategoryID=@CategoryID
end
go

create procedure GetCategories
as
begin
	select CategoryID, CategoryName, Description, CreationDate, LastModificationDate, Active from dtCategories
end
go

create procedure ValidateIfCategoryNameExists(@CategoryName as nvarchar(30))
as
begin
	select CategoryName from dtCategories where CategoryName=@CategoryName
end
go

--Products Table Operations
create procedure CreateProduct(@CategoryID as int, @ProductName as nvarchar(30), @ProductImage as nvarchar(max), @Description as varchar(150), 
@Existence as int, @Price as decimal(16,2), @CreationDate as datetime, @LastModificationDate as datetime, @Active bit)
as 
begin
	insert into dtProducts values(@CategoryID, @ProductName, @ProductImage, @Description, @Existence, @Price, @CreationDate, @LastModificationDate, @Active)
end
go

create procedure UpdateProduct(@ProductID as int, @CategoryID as int, @ProductName as nvarchar(30), @Description as nvarchar(150), 
@Existence as int, @Price as decimal(16,2), @LastModificationDate as datetime, @Active as bit)
as 
begin
	update dtProducts set CategoryID=@CategoryID, ProductName= @ProductName, Description = @Description,
	Existence = @Existence, Price=@Price, LastModificationDate = @LastModificationDate,
	Active = @Active
	where ProductID=@ProductID
end
go

create procedure DeleteProduct(@ProductID as int)
as
begin
	delete from dtProducts where ProductID = @ProductID
end
go

create procedure GetProductById(@ProductID as int)
as 
begin
	select p.ProductID, p.CategoryID, p.ProductName, c.CategoryName, p.ProductImage, p.Description, p.Existence, p.Price,
	p.CreationDate, p.LastModificationDate, p.Active from dtProducts p inner join dtCategories c on p.CategoryID = c.CategoryID
	where p.ProductID = @ProductID
end
go

create procedure GetProducts
as
begin
	select p.ProductID, p.CategoryID, p.ProductName, c.CategoryName, p.ProductImage, p.Description, p.Existence, p.Price,
	p.CreationDate, p.LastModificationDate, p.Active from dtProducts p inner join dtCategories c on p.CategoryID = c.CategoryID
end
go
