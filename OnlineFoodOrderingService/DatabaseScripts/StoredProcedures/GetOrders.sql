--EXEC Usp_GetOrders 1,'2017-06-07','2017-06-17','N'
ALTER PROCEDURE Usp_GetOrders
@Userid			NUMERIC(10)	=0,
@FromDate		DateTime=Null,
@ToDate			DateTime=Null,
@Status			CHAR='' --'' means all orders P means past orders N means waitng for delivery

As
BEGIN	
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
 if(@Userid =0)
	BEGIN
		SELECT o.OrderId,
			   o.OrderDate,
			   U.Userid,
			   U.UserName,
			   o.OrderStatus,
			   o.DeliveryAddress,
			   o.CityCode,
			   Gt.GrandTotal
		FROM Orders o
		INNER JOIN Users U
		ON o.Userid=U.Userid
		INNER JOIN (
			SELECT SUM (OLI. Price*OLI.Quantity) AS GrandTotal,OLI.OrderId AS OrderId
			FROM Orders o
			inner join OrderLineItem OLI
			on o.OrderId=OLI.OrderId
			group by OLI.OrderId
		) Gt 
		ON Gt.OrderId =o.OrderId
		WHERE (ISNULL(@FromDate,'')='' OR  ISNULL(@ToDate,'')='' OR 
			  CAST(o.OrderDate AS DATE) BETWEEN @FromDate AND @ToDate)
		ORDER BY 1 DESC
	END
ELSE
	IF(@Status='')
	BEGIN
		SELECT o.OrderId,
			   o.OrderDate,
			   U.Userid,
			   U.UserName,
			   o.OrderStatus,
			   o.DeliveryAddress,
			   o.CityCode,
			   Gt.GrandTotal
		FROM Orders o
		INNER JOIN Users U
		ON o.Userid=U.Userid
		INNER JOIN (
			SELECT SUM (OLI. Price*OLI.Quantity) AS GrandTotal,OLI.OrderId AS OrderId
			FROM Orders o
			inner join OrderLineItem OLI
			on o.OrderId=OLI.OrderId
			group by OLI.OrderId
		) Gt 
		ON Gt.OrderId =o.OrderId
		WHERE U.Userid=@Userid
		ORDER BY 1 DESC
	END
	ELSE IF(@Status='P')
	BEGIN
		SELECT o.OrderId,
			   o.OrderDate,
			   U.Userid,
			   U.UserName,
			   o.OrderStatus,
			   o.DeliveryAddress,
			   o.CityCode,
			   Gt.GrandTotal
		FROM Orders o
		INNER JOIN Users U
		ON o.Userid=U.Userid
		INNER JOIN (
			SELECT SUM (OLI. Price*OLI.Quantity) AS GrandTotal,OLI.OrderId AS OrderId
			FROM Orders o
			inner join OrderLineItem OLI
			on o.OrderId=OLI.OrderId
			group by OLI.OrderId
		) Gt 
		ON Gt.OrderId =o.OrderId
		WHERE U.Userid=@Userid
		AND o.OrderStatus IN ('D','C')
		ORDER BY 1 DESC
	END
	ELSE IF(@Status='N')
	BEGIN
		SELECT o.OrderId,
			   o.OrderDate,
			   U.Userid,
			   U.UserName,
			   o.OrderStatus,
			   o.DeliveryAddress,
			   o.CityCode,
			   Gt.GrandTotal
		FROM Orders o
		INNER JOIN Users U
		ON o.Userid=U.Userid
		INNER JOIN (
			SELECT SUM (OLI. Price*OLI.Quantity) AS GrandTotal,OLI.OrderId AS OrderId
			FROM Orders o
			inner join OrderLineItem OLI
			on o.OrderId=OLI.OrderId
			group by OLI.OrderId
		) Gt 
		ON Gt.OrderId =o.OrderId
		WHERE U.Userid=@Userid
		AND o.OrderStatus IN ('P','O')
		ORDER BY 1 DESC
	END
END

