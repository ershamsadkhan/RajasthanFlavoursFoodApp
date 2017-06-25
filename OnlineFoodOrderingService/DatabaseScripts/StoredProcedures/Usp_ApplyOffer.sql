--Usp_ApplyOffer 23,'C200'
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[Usp_ApplyOffer]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE Usp_ApplyOffer
END
GO
CREATE PROCEDURE Usp_ApplyOffer
@OrderNo	         NUMERIC(5)	=0 ,
@OfferCode		     VARCHAR(10) =''  
As
BEGIN

	INSERT INTO OfferHistory 
	VALUES(
	      @OrderNo,
		  (SELECT OfferId FROM Offer WHERE OfferCode=@OfferCode),
		  GETDATE()
		  )
END