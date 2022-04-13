
create table product (
    pId int primary key identity,
    pName varchar(20) not null,
    pCategory varchar(20) not null ,
    pPrice int not null,
    pQty int

)
drop table customer  insert into customer values ('kiddo' , 'Kid', 'kid@gmail.com', 15)
select * from customer
create table customer(
    cNo int PRIMARY key IDENTITY,
    cId varchar(20) UNIQUE,
    cName varchar(20),
    cEmail varchar(40),
    -- cDob datetime, 
    cAge int,
)

create table orders (
    orderId int primary key identity,
    cNo int  ,
    pId int ,
    orderQty int not null,
    orderDate datetime,

)

select * from orders
select * from invoice
create table invoice (
    orderId int FOREIGN key REFERENCES orders(orderId),
    totalAmount int
)



-- order table : orderate  ..... put procedure eg. create procedure order_date(@cid)
-- order table : order id ( auto made procedure .. . ? )

insert into product values ('Sprite 6 pack','beverage',10,200)
insert into product values ('Coca Cola 6 pack','beverage',10,200)
insert into product values ('Fanta 12 pack','beverage',20,200)
insert into product values ('Lays_ Frito Lay','snack',3,200)
insert into product values ('Tostito_ Frito Lay','snack',4,200)
insert into product values ('Doritos_ Frito Lay','snack',5,200)
insert into product values ('Cheetos_ Frito Lay','snack',6,200)


select * from customer
select * from product
select * from orders

select * from orders where orderDate between '2022-03-04' and '2022-04-06'

-- STOCK LEFT QTY AFTER TRIGGER
create trigger trg_after_sale_insert on orders after INSERT
as 
begin 
    declare @soldproductid int;
    declare @qtysold int;
    set @soldproductid = (select i.pId from inserted i)
    set @qtysold = (select i.orderQty from inserted i)

    update product set pQty = pQty - @qtysold where  pid = @soldproductid
end


-- prodcudure < even with this , without after trigger qty won't be changed >-----



create PROCEDURE procedure_newOrder (@cNo int, @productId int , @orderQty int)
as 
begin 
    set @cNo = (select cNo from customer where cNo = @cNo)
    declare @orderdate datetime = (select GETDATE())

  insert into orders values (@cNo, @productId, @orderQty , @orderdate)

end

exec procedure_newOrder 1,1,5


------------------------------------
-- procedure updating product -----

CREATE PROCEDURE procedure_updateProduct (@productId int, @productQty int)
as 
begin 
    set @productId = (select pId from product where pId = @productId)
    set @productQty = (@productQty + (select pQty from product where pId =@productId))
    update product set pQty = @productQty where pid = @productId 
end

exec procedure_updateProduct 8, 20

--------------------------------

select orderQty * pId from orders where orderId = 1  -- working. using this format... 

------ View bill amount by putting orderID 
select orderQty * pPrice as billAmount_by_OrderID from product join orders on product.pId = orders.pId   where orderId = 1 -- works!!!! -

select * from customer
select * from product 
select * from orders


select * from product join orders on product.pId = orders.Pid   --- Join Table format

------ View bill amount by putting cNo 
select isNull(sum(orderQty * pPrice),0) as billAmount_by_CNo from product join orders on product.pId = orders.Pid where cNo = 2  --- workds !!! 


--------after trigger after cancel order -- product qty change 

create trigger trg_after_cancel on orders after delete
as 
begin 
    declare @cancelproductid int;
    declare @qtycancel int;
    set @cancelproductid = (select d.pId from deleted d)
    set @qtycancel = (select d.orderQty from deleted d)

    update product set pQty = pQty + @qtycancel where  pid = @cancelproductid
end

--------------  
-- before trigger on order if the customer is underaged  // 
create TRIGGER check_customer_age_before_insert on orders for INSERT
as 
begin 
if (((select cAge from customer) < 21)  and ((select count(Pcategory)from product where pCategory = 'adult juice') >= 1))
begin 
    rollback ;
   -- print ('You cannot buy this until you reach 21')
    END
    END
--------------- 
truncate table product

SELECT DATEDIFF(year, getdate(), (select cDob from customer  where cNo = 1)) -- works !!

drop TRIGGER check_customer_age_before_insert
select cDob from customer where cNo=1 -- works 
select getdate()

select * from orders
 select Year(getdate()) - (select Year(20000)) -- works 

----


SELECT DATEDIFF(year, getdate(), '2011/08/25')


