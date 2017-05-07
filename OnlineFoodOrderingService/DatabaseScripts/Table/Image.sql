USE [OnlineFoodOrdering]
GO

/****** Object:  Table [dbo].[Images]    Script Date: 5/7/2017 11:21:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images](
	[Id] [int] NOT NULL,
	[ImageName] [nvarchar](150) NULL,
	[ImageUrl] [nvarchar](250) NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO