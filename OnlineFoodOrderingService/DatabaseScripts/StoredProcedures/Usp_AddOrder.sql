IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[Usp_AddOrder]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE Usp_AddOrder
END
GO
--exec Usp_AddOrder @lineItemXml='<OrderLineItem>
--  <lineItem Price="0" PriceType="2" Quantity="3" ItemId="1" />
--  <lineItem Price="0" PriceType="3" Quantity="2" ItemId="2" />
--</OrderLineItem>',@UserId='4'
--go 
CREATE PROCEDURE Usp_AddOrder
@lineItemXml			VARCHAR(4000),
@UserId					NUMERIC(5),
@DeliveryAddress		VARCHAR(1000),
@CityCode				NUMERIC(3)
As
BEGIN

Declare 
@ErrMsg AS VARCHAR(200),
@Status AS BIT

SELECT @Status=0,@ErrMsg=''

BEGIN TRAN
BEGIN TRY

	IF OBJECT_ID('tempdb..#tempOrdersLineItem') IS NOT NULL
    DROP TABLE #tempOrdersLineItem

	DECLARE @OrderNo int
	DECLARE @GrandTotal NUMERIC(5)=0
	DECLARE @tempOrderTable table (OrderNo int)
	DECLARE @DocHandle int
	EXEC sp_xml_preparedocument @DocHandle OUTPUT, @lineItemXml

	SELECT * INTO #tempOrdersLineItem
	FROM 	
		OPENXML (@DocHandle, '/OrderLineItem/lineItem',1) 
		WITH (
		PriceType  int,
		Quantity   int,
		ItemId     int
		)

	EXEC sp_xml_removedocument @DocHandle
	
	--SELECT * from #tempOrdersLineItem

	INSERT INTO Orders
	(
	Userid,
	OrderDate,
	OrderStatus,
	DeliveryAddress,
	CityCode	
	) OUTPUT Inserted.OrderId INTO @tempOrderTable
	VALUES
	(
	@UserId,
	GETDate(),
	'P',
	@DeliveryAddress,
	@CityCode	
	)

	SELECT @OrderNo=OrderNo from @tempOrderTable

	INSERT INTO OrderLineItem
	(
		OrderId,
		Quantity,
		PriceType,
		Price,
		ItemId
	)
	SELECT  @OrderNo       AS OrderId,
			tli.Quantity   AS Quantity,
			tli.PriceType  AS PriceType,
			CASE(tli.PriceType)
				WHEN 1 THEN it.QuaterPrice
				WHEN 2 THEN it.HalfPrice
				WHEN 3 THEN it.FullPrice
			END            AS Price,
			tli.ItemId     AS ItemId
	FROM #tempOrdersLineItem tli
	INNER JOIN Items it
	ON it.Itemid=tli.ItemId

	--compute grand total
	SELECT @GrandTotal=SUM (OLI. Price*OLI.Quantity) 
			FROM Orders o
			inner join OrderLineItem OLI
			on o.OrderId=OLI.OrderId
			WHERE o.OrderId=@OrderNo
			group by OLI.OrderId

	Update Orders set GrandTotal=@GrandTotal WHERE OrderId=@OrderNo

	SELECT @Status=1,@ErrMsg='SuccessFull'
	SELECT @Status AS Status,@ErrMsg AS ErrMsg,@OrderNo AS OrderNo
	COMMIT TRAN
END TRY
BEGIN CATCH
	SELECT @Status=0,@ErrMsg=ERROR_MESSAGE()
	SELECT @Status AS Status,@ErrMsg AS ErrMsg,@OrderNo AS OrderNo
	ROLLBACK TRAN
END CATCH

END