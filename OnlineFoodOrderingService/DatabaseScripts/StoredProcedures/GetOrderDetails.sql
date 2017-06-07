--EXEC Usp_GetOrderDetails 4
ALTER PROCEDURE Usp_GetOrderDetails
@Orderid  NUMERIC(10)	=0
As
BEGIN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	SELECT o.OrderId,
		   o.OrderDate,
		   OLI.OrderLineItemId,
		   OLI. Quantity,
		   OLI. Price,
		   OLI.PriceType,
		   it.ItemHeader,
		   it.ImageUrl
	FROM Orders o
	INNER JOIN OrderLineItem OLI
	on o.OrderId=OLI.OrderId
	INNER JOIN Items it
	on OLI.Itemid=it.Itemid
	WHERE o.OrderId=@Orderid

END
