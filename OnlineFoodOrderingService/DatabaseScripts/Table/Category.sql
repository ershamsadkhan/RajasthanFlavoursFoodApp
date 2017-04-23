IF NOT EXISTS(SELECT 1 FROM information_schema.tables WHERE table_name = 'Category')
BEGIN
	CREATE TABLE Category(Categoryid NUMERIC(8) PRIMARY KEY NOT NULL IDENTITY,CategoryHeader varchar(100), CategoryDescription varchar(200))
END

IF NOT EXISTS(SELECT 1 FROM information_schema.tables WHERE table_name = 'Items')
BEGIN
	CREATE TABLE Items(Itemid NUMERIC(8) PRIMARY KEY NOT NULL IDENTITY,Categoryid NUMERIC(8) NOT NULL FOREIGN KEY REFERENCES Category(Categoryid),ItemHeader varchar(100), ItemDescription varchar(200),QuaterPrice NUMERIC(5) NOT NULL,HalfPrice NUMERIC(5) NOT NULL,FullPrice NUMERIC(5) NOT NULL,ImageUrl VARCHAR(200))
END