CREATE DATABASE road_trip_test
GO
USE [road_trip_test]
GO
/****** Object:  Table [dbo].[destinations]    Script Date: 3/8/2016 3:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[destinations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[roadtrip_id] [int] NULL,
	[stop] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[images]    Script Date: 3/8/2016 3:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[images](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[link] [varchar](2000) NULL,
	[search_terms_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[roadtrips]    Script Date: 3/8/2016 3:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[roadtrips](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [varchar](5000) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[search_terms]    Script Date: 3/8/2016 3:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[search_terms](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[term] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[destinations] ON 

INSERT [dbo].[destinations] ([id], [name], [roadtrip_id], [stop]) VALUES (1, N'multnomah falls', 1, 1)
INSERT [dbo].[destinations] ([id], [name], [roadtrip_id], [stop]) VALUES (2, N'paradise falls', 1, 2)
INSERT [dbo].[destinations] ([id], [name], [roadtrip_id], [stop]) VALUES (3, N'burgerville', 1, 3)
INSERT [dbo].[destinations] ([id], [name], [roadtrip_id], [stop]) VALUES (4, N'aquarium', 1, 4)
SET IDENTITY_INSERT [dbo].[destinations] OFF
SET IDENTITY_INSERT [dbo].[roadtrips] ON 

INSERT [dbo].[roadtrips] ([id], [name], [description]) VALUES (1, N'excellent adventure', N'excellent adventure to faraway places')
SET IDENTITY_INSERT [dbo].[roadtrips] OFF
SET IDENTITY_INSERT [dbo].[search_terms] ON 

INSERT [dbo].[search_terms] ([id], [term]) VALUES (1, N'portland')
INSERT [dbo].[search_terms] ([id], [term]) VALUES (2, N'poptarts')
INSERT [dbo].[search_terms] ([id], [term]) VALUES (3, N'pizza')
SET IDENTITY_INSERT [dbo].[search_terms] OFF
