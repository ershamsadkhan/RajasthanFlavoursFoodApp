
--Usp_GetAppliedOffers 23
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[Usp_GetAppliedOffers]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE Usp_GetAppliedOffers
END
GO
CREATE PROCEDURE Usp_GetAppliedOffers
@OrderNo		NUMERIC(5) =0

AS
BEGIN
	   Select ohf.*
	   from 
	   offer ohf INNER JOIN OfferHistory oh
	   ON ohf.OfferId=oh.OfferId
	   INNER JOIN Orders orders
	   on oh.OrderId= orders.OrderId 
	   where oh.OrderId=@OrderNo
END

 