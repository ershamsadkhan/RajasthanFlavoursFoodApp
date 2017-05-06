IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_UpdateOrderStatus]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_UpdateOrderStatus
END
GO
--EXEC Usp_UpdateOrderStatus 3,C   --P:Placed C:Cancelled D:Delivered
CREATE PROCEDURE Usp_UpdateOrderStatus
@OrderNo			NUMERIC(5),
@OrderStatus		VARCHAR(2)
As
BEGIN

Declare 
@ErrMsg AS VARCHAR(200),
@Status AS BIT

SELECT @Status=0,@ErrMsg=''

BEGIN TRAN
BEGIN TRY

	DECLARE @OldOrderStatus AS VARCHAR(2);
	SELECT @OldOrderStatus=OrderStatus FROM Orders WHERE OrderId=@OrderNo

	IF(ISNULL(@OldOrderStatus,'P')='D')
	BEGIN
		SELECT @Status=0,@ErrMsg='Order is already delivered.'
	END
	ELSE IF(ISNULL(@OldOrderStatus,'P')='C')
	BEGIN
		SELECT @Status=0,@ErrMsg='Order is already cancelled.'
	END
	ELSE
	BEGIN
		UPDATE Orders SET OrderStatus=@OrderStatus 
		WHERE OrderId=@OrderNo

		SELECT @Status=1,@ErrMsg='SuccessFully updated status'
	END
	
	SELECT @Status AS Status,@ErrMsg AS ErrMsg
	COMMIT TRAN
END TRY
BEGIN CATCH
	SELECT @Status=0,@ErrMsg=ERROR_MESSAGE()
	SELECT @Status AS Status,@ErrMsg AS ErrMsg
	ROLLBACK TRAN
END CATCH

END