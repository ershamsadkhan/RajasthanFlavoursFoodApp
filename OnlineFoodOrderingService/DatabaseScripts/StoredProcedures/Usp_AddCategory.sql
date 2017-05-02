IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_AddCategory]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_AddCategory
END
GO
--EXEC Usp_GetItemsList 
CREATE PROCEDURE Usp_AddCategory
@CategoryHeader			VARCHAR(100),
@CategoryDescription	VARCHAR(200)
As
BEGIN

Declare 
@ErrMsg AS VARCHAR(200),
@Status AS BIT

SELECT @Status=0,@ErrMsg=''

BEGIN TRAN
BEGIN TRY
	INSERT INTO Category(
		CategoryHeader,
		CategoryDescription
	)
	Values
	(
		@CategoryHeader,
		@CategoryDescription
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