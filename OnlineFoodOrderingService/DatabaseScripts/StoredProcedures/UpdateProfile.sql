IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[Usp_UpdateProfile]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].Usp_UpdateProfile
END
GO
CREATE PROCEDURE Usp_UpdateProfile
@Userid			 NUMERIC(5)	=0,
@UserName		 VARCHAR(50)   ,
@Email           VARCHAR(50)   ,
@PhoneNo		 VARCHAR(20)   ,
@PrimaryAddress  VARCHAR(200)  
As
BEGIN
	Update Users
	SET UserEmailAddress=@Email,
		UserPhoneNumber=@PhoneNo,
		PrimaryAddress=@PrimaryAddress
		where Userid=@Userid
END
