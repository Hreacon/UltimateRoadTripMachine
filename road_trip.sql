USE [road_trip]
GO
/****** Object:  Table [dbo].[destination]    Script Date: 3/7/2016 4:43:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[destination](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[roadtrip_id] [int] NULL,
	[stop] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[roadtrip]    Script Date: 3/7/2016 4:43:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[roadtrip](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [varchar](5000) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[roadtrip] ON 

INSERT [dbo].[roadtrip] ([id], [name], [description]) VALUES (1, N'excellent adventure', N'excellent adventure to faraway places')
SET IDENTITY_INSERT [dbo].[roadtrip] OFF
