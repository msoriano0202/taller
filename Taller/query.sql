declare @Customers as table
(
	PersonId int primary key identity(1,1),
	FirstName varchar(50),
	LastName varchar(50),
	Age int,
	Occupation varchar(100),
	MartialStatus varchar(100)
)

insert into @Customers(FirstName, LastName, Age, Occupation, MartialStatus)
values ('Miguel', 'Soriano', 23, 'Student', 'Single'),
	   ('Emanuel', 'Soriano', 19, 'Student', 'Single')

declare @Orders as table
(
	OrderId int primary key identity(1,1),
	PersonId int,
	DateCreated datetime,
	MethodofPurchase varchar(50)
)

insert into @Orders(PersonId, DateCreated, MethodofPurchase)
values (1, GETDATE(), 'DEBIT'),
	   (2, GETDATE(), 'CREDIT')

declare @OrderDetails as table
(
	OrderDetailId int primary key identity(1,1), 
	OrderId int, 
	ProductNumber varchar(10), 
	ProductId varchar(10), 
	ProductOrigin varchar(20)
)

insert into @OrderDetails(OrderId, ProductNumber, ProductId, ProductOrigin)
values (1, '1112222333', '1112222333', 'USA'),
	   (2, '0000000000', '0000000000', 'ASIA')


declare @Product_Id as varchar(10) = '1112222333'

select	concat(cust.FirstName, ' ', cust.LastName) as 'Full Name',
		cust.Age,
		ord.OrderId,ord.DateCreated,ord.MethodofPurchase as 'Purchase Method',
		det.ProductNumber, det.ProductOrigin
from	@Customers cust
inner join @Orders ord on cust.PersonId = ord.PersonId
inner join @OrderDetails det on ord.OrderId = det.OrderId
where	det.ProductId = @Product_Id

