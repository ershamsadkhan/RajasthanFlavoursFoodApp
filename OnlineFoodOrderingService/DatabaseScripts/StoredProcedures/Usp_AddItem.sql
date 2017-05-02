IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_AddItem]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_AddItem
END
GO
--EXEC Usp_AddItem 
CREATE PROCEDURE Usp_AddItem
@Categoryid				NUMERIC(5),
@ItemHeader				VARCHAR(100),
@ItemDescription		VARCHAR(200),
@QuaterPrice			NUMERIC(5),
@HalfPrice				NUMERIC(5),
@FullPrice				NUMERIC(5),
@ImageUrl				VARCHAR(200)
As
BEGIN

Declare 
@ErrMsg AS VARCHAR(200),
@Status AS BIT

SELECT @Status=0,@ErrMsg=''

BEGIN TRAN
BEGIN TRY
	INSERT INTO Items(
		Categoryid,
		ItemHeader,
		ItemDescription,
		QuaterPrice,
		HalfPrice,
		FullPrice,
		ImageUrl
	)
	Values
	(
		@Categoryid,
		@ItemHeader,
		@ItemDescription,
		@QuaterPrice,
		@HalfPrice,
		@FullPrice,
		@ImageUrl
	)

	SELECT @Status=1,@ErrMsg='SuccessFull'
	SELECT @Status AS Status,@ErrMsg AS ErrMsg
	COMMIT TRAN
END TRY
BEGIN CATCH
	SELECT @Status=0,@ErrMsg=ERROR_MESSAGE()
	SELECT @Status AS Status,@ErrMsg AS ErrMsg
	ROLLBACK TRAN
END CATCH

END