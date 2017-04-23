IF NOT EXISTS(SELECT 1 FROM information_schema.tables WHERE table_name = 'Users')
BEGIN
	CREATE TABLE Users(Userid NUMERIC(8) PRIMARY KEY NOT NULL IDENTITY,UserName varchar(50), UserPwd varchar(50), PrimaryAddress varchar(200),IsAdmin bit )
END