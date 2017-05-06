IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_DeleteOrder]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_DeleteOrder
END
GO
--EXEC Usp_DeleteOrder 2
CREATE PROCEDURE Usp_DeleteOrder
@OrderNo			NUMERIC(5)
As
BEGIN

Declare 
@ErrMsg AS VARCHAR(200),
@Status AS BIT

SELECT @Status=0,@ErrMsg=''

BEGIN TRAN
BEGIN TRY
	DELETE FROM OrderLineItem 
	WHERE OrderId=@OrderNo

	DELETE FROM Orders
	WHERE OrderId=@OrderNo

	SELECT @Status=1,@ErrMsg='SuccessFully deleted'
	SELECT @Status AS Status,@ErrMsg AS ErrMsg
	COMMIT TRAN
END TRY
BEGIN CATCH
	SELECT @Status=0,@ErrMsg=ERROR_MESSAGE()
	SELECT @Status AS Status,@ErrMsg AS ErrMsg
	ROLLBACK TRAN
END CATCH

END