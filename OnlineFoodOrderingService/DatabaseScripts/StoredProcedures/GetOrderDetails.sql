--EXEC Usp_GetOrderDetails
CREATE PROCEDURE Usp_GetOrderDetails
@Orderid  NUMERIC(10)	=0
As
BEGIN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SELECT o.OrderId,
		   o.OrderDate,
		   OLI.OrderLineItemId,
		   OLI. Quantity,
		   OLI. Price,
		   it.ItemHeader
	FROM Orders o
	inner join OrderLineItem OLI
	on o.OrderId=OLI.OrderId
	INNER JOIN Items it
	on OLI.Itemid=it.Itemid
	WHERE o.OrderId=@Orderid

END
