
--CREATE TABLES
	--Table Users
create table dtUsers (
UserID int not null identity(1,1),
FirstName varchar(40) not null,
LastName varchar(40) not null,
CellPhone varchar(20) not null,
Genre char(1) not null, 
Email varchar(40) not null, 
UserName varchar(10) not null,
Password varchar(max) not null,
constraint PK_dtUsers primary key(UserID),
constraint CK_dtUsers check(Genre in ('F','M')),
constraint IDX_dtUsers unique (UserName)
)
go

	--Table Categories
create table dtCategories(
CategoryID int not null identity(1,1),
CategoryName varchar(30) not null, 
Description varchar(150), 
constraint PK_dtCategories primary key(CategoryID),
constraint IDX_dtCategories unique(CategoryName)
)
go


	--Table Products
create table dtProducts(
ProductID int not null identity(1,1),
CategoryID int not null,
ProductName varchar(30) not null,
Description varchar(150),
Existence int not null,
Price decimal(16,2),
Creation_Date datetime,
Expire_Date datetime,
constraint PK_dtProducts primary key(ProductID),
constraint FK_dtProducts foreign key(CategoryID) references dtCategories(CategoryID)
)
go

--User Table Operations
create procedure CreateUser(@FirstName as varchar(40), @LastName as varchar(40), 
@CellPhone as varchar(20), @Genre as char(1),@Email as varchar(40), @UserName as varchar(10), @Password as varchar(max))
as
begin 
	insert into dtUsers values(@FirstName, @LastName, @CellPhone, @Genre, @Email, @UserName, @Password)
end
go

create procedure UpdateUser(@UserID as int, @FirstName as varchar(40), @LastName as varchar(40), 
@CellPhone as varchar(20), @Genre as char(1),@Email as varchar(40), @UserName as varchar(10), @Password as varchar(max))
as
begin
	update dtUsers set FirstName=@FirstName, LastName=@LastName, CellPhone=@CellPhone,
	Genre=@Genre, Email=@Email, UserName=@UserName, Password=@Password where UserID=@UserID
end
go

create procedure GetUserByUserNameAndPassword(@UserName as varchar(10), @Password as varchar(max))
as
begin
	select*from dtUsers where UserName=@UserName and Password=@Password
end
go

create procedure GetUserById(@UserID as int)
as
begin
	select*from dtUsers where UserID=@UserID
end
go

--Category Table Operations
create procedure CreateCategory(@CategoryName as varchar, @Description as varchar)
as
begin	
	insert into dtCategories values(@CategoryName, @Description)
end
go

create procedure UpdateCategory(@CategoryID as int, @CategoryName as varchar, @Description as varchar)
as
begin
	update dtCategories set CategoryName=@CategoryName, Description=@Description where CategoryID=@CategoryID
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
	select*from dtCategories where CategoryID=@CategoryID
end
go

create procedure GetCategories
as
begin
	select*from dtCategories
end
go

--Products Table Operations
create procedure CreateProduct(@CategoryID as int, @ProductName as varchar(30), @Description as varchar(150), 
@Existence as int, @Price as decimal(16,2), @Creation_Date as datetime, @Expire_Date as datetime)
as 
begin
	insert into dtProducts values(@CategoryID, @ProductName, @Description, @Existence, @Price, @Creation_Date, @Expire_Date)
end
go

create procedure UpdateProduct(@ProductID as int, @CategoryID as int, @ProductName as varchar(30), @Description as varchar(150), 
@Existence as int, @Price as decimal, @Creation_Date as datetime, @Expire_Date as datetime)
as 
begin
	update dtProducts set CategoryID=@CategoryID, ProductName= @ProductName, Description = @Description,
	Existence = @Existence, Price=@Price, Creation_Date=@Creation_Date, Expire_Date = @Expire_Date 
	where ProductID=@ProductID
end
go

create procedure DeleteProduct(@ProductID as int)
as
begin
	delete from dtProducts where ProductID= @ProductID
end
go

create procedure GetProductById(@ProductID as int)
as 
begin
	select p.ProductID, p.CategoryID, p.ProductName, c.CategoryName, p.Description, p.Existence, p.Price,
	p.Creation_Date, p.Expire_Date from dtProducts p inner join dtCategories c on p.CategoryID = c.CategoryID
	where p.ProductID = @ProductID
end
go

create procedure GetProducts
as
begin
	select p.ProductID, p.CategoryID, p.ProductName, c.CategoryName, p.Description, p.Existence, p.Price,
	p.Creation_Date, p.Expire_Date from dtProducts p inner join dtCategories c on p.CategoryID = c.CategoryID
end
go

create procedure ValidateIfUserExists(@UserName as varchar)
as
begin
	select UserName from dtUsers where UserName=@UserName
end
go

create procedure ValidateIfCategoryNameExists(@CategoryName as varchar)
as
begin
	select CategoryName from dtCategories where CategoryName=@CategoryName
end
go
