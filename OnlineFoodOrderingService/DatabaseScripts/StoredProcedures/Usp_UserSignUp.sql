IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_UserSignUp]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_UserSignUp
END
GO
--EXEC Usp_UserSignUp 1,'Shamsad','Shamsad','Kndivali',1
CREATE PROCEDURE Usp_UserSignUp
@Userid			NUMERIC(5)	=0,
@UserName		VARCHAR(50)	  ,
@UserPwd		VARCHAR(50)   ,
@PrimaryAddress VARCHAR(200)  ,
@IsAdmin		BIT	        =0
As
BEGIN

Declare 
@ErrMsg AS VARCHAR(200),
@Status AS BIT

SELECT @Status=0,@ErrMsg=''

BEGIN TRAN
BEGIN TRY
	INSERT INTO Users(
		UserName,
		UserPwd,
		PrimaryAddress,
		IsAdmin
	)
	Values
	(
		@UserName,
		@UserPwd,
		@PrimaryAddress,
		@IsAdmin
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


