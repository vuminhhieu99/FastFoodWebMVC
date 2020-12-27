USE [master]
GO
/****** Object:  Database [SnackShopDB]    Script Date: 2020-07-18 4:24:45 PM ******/
CREATE DATABASE [SnackShopDB]
 
GO
ALTER DATABASE [SnackShopDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SnackShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SnackShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SnackShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SnackShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SnackShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SnackShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SnackShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SnackShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SnackShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SnackShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SnackShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SnackShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SnackShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SnackShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SnackShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SnackShopDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SnackShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SnackShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SnackShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SnackShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SnackShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SnackShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SnackShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SnackShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SnackShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [SnackShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SnackShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SnackShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SnackShopDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SnackShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SnackShopDB', N'ON'
GO
USE [SnackShopDB]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bill](
	[id_bill] [int] IDENTITY(1,1) NOT NULL,
	[subtotal] [decimal](12, 0) NULL CONSTRAINT [DF_Bill_subtotal]  DEFAULT ((0)),
	[total] [decimal](12, 0) NULL CONSTRAINT [DF_Bill_total]  DEFAULT ((0)),
	[creatDate] [datetime] NULL,
	[id_customer] [int] NULL,
	[discountCode] [varchar](20) NULL,
	[discount] [decimal](12, 0) NULL CONSTRAINT [DF_Bill_discount]  DEFAULT ((0)),
	[address] [nvarchar](500) NULL,
	[phone] [char](10) NULL,
	[id_status] [int] NULL CONSTRAINT [DF_Bill_status]  DEFAULT ((1)),
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[id_bill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BillDetail]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetail](
	[id_bill] [int] NOT NULL,
	[id_product] [int] NOT NULL,
	[price] [decimal](10, 0) NULL CONSTRAINT [DF_BillDetail_price]  DEFAULT ((0)),
	[amount] [int] NULL CONSTRAINT [DF_BillDetail_amount]  DEFAULT ((0)),
	[intoMoney] [decimal](12, 0) NULL CONSTRAINT [DF_BillDetail_intoMoney]  DEFAULT ((0)),
	[discriptionProductDetail] [nvarchar](max) NULL,
 CONSTRAINT [PK_BillDetail] PRIMARY KEY CLUSTERED 
(
	[id_bill] ASC,
	[id_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BillStatus]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillStatus](
	[id_status] [int] IDENTITY(1,1) NOT NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_BillStatus] PRIMARY KEY CLUSTERED 
(
	[id_status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cart]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cart](
	[id_cart] [int] IDENTITY(1,1) NOT NULL,
	[subtotal] [decimal](12, 0) NULL,
	[total] [decimal](12, 0) NULL,
	[id_discountCode] [varchar](20) NULL,
	[id_customer] [int] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[id_cart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[id_customer] [int] NOT NULL,
	[id_product] [int] NOT NULL,
	[price] [decimal](10, 0) NULL,
	[amount] [int] NULL,
	[intoMoney] [decimal](12, 0) NULL,
	[discriptionProductDetail] [nvarchar](max) NULL,
 CONSTRAINT [PK_CartDetail_1] PRIMARY KEY CLUSTERED 
(
	[id_customer] ASC,
	[id_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[id_category] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[photo] [varchar](100) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[id_category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Credential]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Credential](
	[id_userGroup] [varchar](20) NOT NULL,
	[id_role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Credential] PRIMARY KEY CLUSTERED 
(
	[id_userGroup] ASC,
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[id_customer] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[phone] [char](10) NULL,
	[address] [nvarchar](200) NULL,
	[userName] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[subtotalCart] [decimal](12, 0) NULL CONSTRAINT [DF_Customer_subtotalCart]  DEFAULT ((0)),
	[totalCart] [decimal](12, 0) NULL CONSTRAINT [DF_Customer_total]  DEFAULT ((0)),
	[avatar] [varchar](10) NULL,
	[id_discountCode] [varchar](20) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id_customer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DiscountCode]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiscountCode](
	[id_discountCode] [varchar](20) NOT NULL,
	[discount] [decimal](12, 0) NULL CONSTRAINT [DF_DiscountCode_discount]  DEFAULT ((0)),
 CONSTRAINT [PK_DiscountCode] PRIMARY KEY CLUSTERED 
(
	[id_discountCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[id_product] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[description] [nvarchar](max) NULL,
	[information] [nvarchar](max) NULL,
	[review] [int] NOT NULL CONSTRAINT [DF_Product_review]  DEFAULT ((0)),
	[availability] [bit] NOT NULL CONSTRAINT [DF_Product_availability]  DEFAULT ((1)),
	[price] [decimal](10, 0) NULL CONSTRAINT [DF_Product_price]  DEFAULT ((0)),
	[salePercent] [int] NULL CONSTRAINT [DF_Product_salePercent]  DEFAULT ((0)),
	[salePrice] [decimal](10, 0) NULL CONSTRAINT [DF_Product_salePrice]  DEFAULT ((0)),
	[rate] [float] NULL,
	[mainPhoto] [varchar](100) NULL,
	[photo1] [varchar](100) NULL,
	[photo2] [varchar](100) NULL,
	[photo3] [varchar](100) NULL,
	[photo4] [varchar](100) NULL,
	[updated] [date] NULL,
	[id_category] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductDetail]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDetail](
	[id_productDetail] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[amount] [int] NULL CONSTRAINT [DF_ProductDetail_amount]  DEFAULT ((0)),
	[availability] [bit] NOT NULL CONSTRAINT [DF_ProductDetail_availability]  DEFAULT ((1)),
	[extraPrice] [decimal](10, 0) NULL CONSTRAINT [DF_ProductDetail_extraPrice]  DEFAULT ((0)),
	[id_product] [int] NULL,
 CONSTRAINT [PK_ProductDetail] PRIMARY KEY CLUSTERED 
(
	[id_productDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[id_role] [varchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
	[email] [varchar](50) NULL,
	[status] [bit] NULL,
	[createDate] [datetime] NULL,
	[id_userGroup] [varchar](20) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 2020-07-18 4:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserGroup](
	[id_userGroup] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[id_userGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (36, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:10:38.683' AS DateTime), 2, N'10', CAST(50000 AS Decimal(12, 0)), N'HN', N'1111      ', 1)
INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (37, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:10:51.750' AS DateTime), 2, N'20', CAST(50000 AS Decimal(12, 0)), N'HD', N'2222      ', 1)
INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (38, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:11:11.200' AS DateTime), 2, N'30', CAST(50000 AS Decimal(12, 0)), N'HY', N'3333      ', 1)
INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (39, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:11:12.027' AS DateTime), 2, N'30', CAST(50000 AS Decimal(12, 0)), N'HY', N'3333      ', 1)
INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (40, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:11:18.780' AS DateTime), 2, N'40', CAST(50000 AS Decimal(12, 0)), N'HY', N'3333      ', 1)
INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (41, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:11:22.830' AS DateTime), 2, N'40', CAST(50000 AS Decimal(12, 0)), N'HY', N'5555      ', 1)
INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (42, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:11:49.897' AS DateTime), 1, N'40', CAST(50000 AS Decimal(12, 0)), N'HY', N'5555      ', 1)
INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (43, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:12:32.647' AS DateTime), 1, N'40', CAST(50000 AS Decimal(12, 0)), N'HY', N'5555      ', 2)
INSERT [dbo].[Bill] ([id_bill], [subtotal], [total], [creatDate], [id_customer], [discountCode], [discount], [address], [phone], [id_status]) VALUES (44, CAST(265000 AS Decimal(12, 0)), CAST(1235000 AS Decimal(12, 0)), CAST(N'2020-07-03 09:12:42.280' AS DateTime), 2, N'40', CAST(50000 AS Decimal(12, 0)), N'HY', N'5555      ', 3)
SET IDENTITY_INSERT [dbo].[Bill] OFF
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 4, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 20, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 21, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 22, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 23, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 24, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 25, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 26, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (39, 27, CAST(250 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (41, 4, CAST(2000 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (42, 4, CAST(2000 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
INSERT [dbo].[BillDetail] ([id_bill], [id_product], [price], [amount], [intoMoney], [discriptionProductDetail]) VALUES (44, 4, CAST(2000 AS Decimal(10, 0)), 2, CAST(400 AS Decimal(12, 0)), N'none')
SET IDENTITY_INSERT [dbo].[BillStatus] ON 

INSERT [dbo].[BillStatus] ([id_status], [status]) VALUES (1, N'Chờ xác nhận')
INSERT [dbo].[BillStatus] ([id_status], [status]) VALUES (2, N'Chờ lấy hàng')
INSERT [dbo].[BillStatus] ([id_status], [status]) VALUES (3, N'Đang giao')
INSERT [dbo].[BillStatus] ([id_status], [status]) VALUES (4, N'Đã giao')
INSERT [dbo].[BillStatus] ([id_status], [status]) VALUES (5, N'Đã hủy')
INSERT [dbo].[BillStatus] ([id_status], [status]) VALUES (6, N'Trả hàng')
SET IDENTITY_INSERT [dbo].[BillStatus] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([id_category], [name], [photo]) VALUES (1, N'Combo 1 người', N'c6.jpg')
INSERT [dbo].[Category] ([id_category], [name], [photo]) VALUES (2, N'Combo nhóm', N'c15.jpg')
INSERT [dbo].[Category] ([id_category], [name], [photo]) VALUES (3, N'Sản phẩm ưu đãi', N'sp18.png')
INSERT [dbo].[Category] ([id_category], [name], [photo]) VALUES (4, N'Gà rán - quay', N's4.jpg')
INSERT [dbo].[Category] ([id_category], [name], [photo]) VALUES (5, N'Cơm - Burger', N's19.jpg')
INSERT [dbo].[Category] ([id_category], [name], [photo]) VALUES (6, N'Thức ăn nhẹ', N's35.jpg')
INSERT [dbo].[Category] ([id_category], [name], [photo]) VALUES (7, N'Tráng miệng', N's52.png')
INSERT [dbo].[Category] ([id_category], [name], [photo]) VALUES (8, N'Đồ uống', N's53.png')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (1, N'Nguyễn Đức Hưng', N'0328887832', N'230 Nguyễn Văn Giáp', N'ndhung', N'0000', CAST(0 AS Decimal(12, 0)), CAST(0 AS Decimal(12, 0)), N'kh1.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (2, N'Vũ Minh Hiếu', N'0322220125', N'117 Trần Cung', N'vmhieu', N'0000', NULL, NULL, N'kh2.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (3, N'Nguyễn Hữu Tiến', N'0352220122', N'231 Trần Cung', N'nhtien', N'0000', NULL, NULL, N'kh3.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (4, N'Phùng Văn Trường', N'0201125038', N'23 Nguyễn Khánh Toàn', N'pvtruong', N'0000', NULL, NULL, N'kh4.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (5, N'Cao Văn Huy', N'0320015246', N'17 Trần Khánh Dư', N'cvhuy', N'0000', NULL, NULL, N'kh5.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (6, N'Tạ Hữu Sơn', N'0034425501', N'21 Nguyễn Trãi', N'thson', N'0000', NULL, NULL, N'kh6.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (7, N'Lã Minh Đức', N'0750015896', N'110 Kim Mã', N'lmduc', N'0000', NULL, NULL, N'kh7.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (8, N'Lâm Đức Hoàng', N'0322220125', N'187 Hồ Tùng Mậu', N'ldhoang', N'0000', NULL, NULL, N'kh8.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (9, N'Phạm Văn Dáng', N'0358884512', N'1 Nguyễn Cơ Thạch', N'pvdang', N'0000', CAST(0 AS Decimal(12, 0)), CAST(0 AS Decimal(12, 0)), N'kh9.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (10, N'Trần Đức Dũng', N'0520012310', N'224 Láng', N'tddung', N'0000', NULL, NULL, N'kh10.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (11, N'Vũ Thị Hải Yến', N'0374657937', N'62 Văn Hội', N'vthaiyen', N'0000', NULL, NULL, N'kh11.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (12, N'Nguyễn Thị Thuỳ Linh', N'0322201110', N'100 Cổ Nhuế', N'ntthuylinh', N'0000', NULL, NULL, N'kh12.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (13, N'Lê Thị Ngọc', N'0360012547', N'231 Phùng Chí Kiên', N'ltngoc', N'0000', NULL, NULL, N'kh13.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (14, N'Bùi Thị Thu Hương', N'0122201120', N'201 Hoàng Công Chất', N'bthuhuong', N'0000', NULL, NULL, N'kh14.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (15, N'Đỗ Thị Nguyệt Mai', N'0250034521', N'117 Khương Đình', N'ndnguyetmai', N'0000', NULL, NULL, N'kh15.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (16, N'Bùi Thị Hạnh', N'0324875942', N'323 Hoàng Hoa Thám', N'bthanh', N'0000', NULL, NULL, N'kh16.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (17, N'Hà Ngọc Linh', N'0245687510', N'12 Lê Quang Đạo', N'hnlinh', N'0000', NULL, NULL, N'kh17.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (18, N'Nguyễn Thị Loan', N'0324865120', N'180 Khuất Duy Tiến', N'ntloan', N'0000', NULL, NULL, N'kh18.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (19, N'Ngô Thị Huyền', N'0231457854', N'10 Nguyễn Chí Công', N'nthuyen', N'0000', NULL, NULL, N'kh19.png', NULL)
INSERT [dbo].[Customer] ([id_customer], [name], [phone], [address], [userName], [password], [subtotalCart], [totalCart], [avatar], [id_discountCode]) VALUES (20, N'Lê Thị Hương Trang', N'012453201 ', N'222 Lạc Long Quân', N'lthuongtrang', N'0000', NULL, NULL, N'kh20.png', NULL)
SET IDENTITY_INSERT [dbo].[Customer] OFF
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'0012547865', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'0120110026', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'0120223401', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'0124526400', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'01247895641', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'0125001254', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'01254789602', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'10256478950', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'1201445202', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'1204875001', CAST(20000 AS Decimal(12, 0)))
INSERT [dbo].[DiscountCode] ([id_discountCode], [discount]) VALUES (N'1254620012', CAST(20000 AS Decimal(12, 0)))
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (1, N'Combo gà rán A', N'2 miếng gà giòn + 1 pepsi lon', N'', 17, 0, CAST(79000 AS Decimal(10, 0)), 20, CAST(63200 AS Decimal(10, 0)), 5, N'c1.jpg', N'c1.jpg', N'c1.jpg', N'c1.jpg', N'c1.jpg', CAST(N'2020-07-02' AS Date), 1)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (2, N'Combo gà rán B', N'3 miếng hot wings + 1 khoai tây chiên (lớn) + 1 pepsi lon', N'', 5, 1, CAST(79000 AS Decimal(10, 0)), 20, CAST(63200 AS Decimal(10, 0)), 5, N'c2.jpg', N'c2.jpg', N'c2.jpg', N'c2.jpg', N'c2.jpg', CAST(N'2020-06-17' AS Date), 1)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (3, N'Combo gà rán C', N'1 miếng gà giòn cay + 1 burger tôm + 1 pepsi lon', N'', 1, 1, CAST(85000 AS Decimal(10, 0)), 20, CAST(68000 AS Decimal(10, 0)), 5, N'c3.jpg', N'c3.jpg', N'c3.jpg', N'c3.jpg', N'c3.jpg', CAST(N'2020-06-17' AS Date), 1)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (4, N'Combo gà rán D', N'1 miếng gà giòn + 1 burger gà quay + 1 pepsi lon', N'', 1, 1, CAST(89000 AS Decimal(10, 0)), 20, CAST(89000 AS Decimal(10, 0)), 5, N'c4.jpg', N'c4.jpg', N'c4.jpg', N'c4.jpg', N'c4.jpg', CAST(N'2020-06-17' AS Date), 1)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (5, N'Combo cơm A', N'1 cơm gà giòn + 1 súp gà + 1 pepsi lon', N'', 1, 1, CAST(69000 AS Decimal(10, 0)), 20, CAST(55200 AS Decimal(10, 0)), 5, N'c5.jpg', N'c5.jpg', N'c5.jpg', N'c5.jpg', N'c5.jpg', CAST(N'2020-06-17' AS Date), 1)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (6, N'Combo cơm B', N'1 cơm gà giòn + 1 miếng gà giòn + 1 pepsi lon', N'', 0, 1, CAST(89000 AS Decimal(10, 0)), 20, CAST(71200 AS Decimal(10, 0)), 5, N'c6.jpg', N'c6.jpg', N'c6.jpg', N'c6.jpg', N'c6.jpg', CAST(N'2020-06-17' AS Date), 1)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (7, N'Combo cơm C', N'1 cơm gà giòn + 1 burger gà quay + 1 pepsi lon', N'', 2, 1, CAST(95000 AS Decimal(10, 0)), 20, CAST(76000 AS Decimal(10, 0)), 5, N'c7.jpg', N'c7.jpg', N'c7.jpg', N'c7.jpg', N'c7.jpg', CAST(N'2020-06-17' AS Date), 1)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (8, N'Combo nhóm A', N'2 miếng gà giòn + 1 burger tôm + 2 pepsi lon', N'', 2, 1, CAST(129000 AS Decimal(10, 0)), 20, CAST(103200 AS Decimal(10, 0)), 5, N'c8.jpg', N'c8.jpg', N'c8.jpg', N'c8.jpg', N'c8.jpg', CAST(N'2020-06-17' AS Date), 2)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (9, N'Combo nhóm B', N'3 miếng gà giòn + 1 khoai tây chiên (lớn) +  2 pepsi lon', N'', 0, 1, CAST(149000 AS Decimal(10, 0)), 20, CAST(119200 AS Decimal(10, 0)), 5, N'c9.jpg', N'c9.jpg', N'c9.jpg', N'c9.jpg', N'c9.jpg', CAST(N'2020-06-17' AS Date), 2)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (10, N'Combo nhóm C', N'4 miếng gà giòn + 1 khoai tây chiên (lớn) + 2 pepsi lon', N'', 4, 1, CAST(185000 AS Decimal(10, 0)), 20, CAST(148000 AS Decimal(10, 0)), 5, N'c10.jpg', N'c10.jpg', N'c10.jpg', N'c10.jpg', N'c10.jpg', CAST(N'2020-06-17' AS Date), 2)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (11, N'Combo nhóm D', N'2 miếng gà giòn + 1 miếng gà quay + 1 khoai tây chiên (lớn) + 2 pepsi lon', N'', 0, 1, CAST(185000 AS Decimal(10, 0)), 20, CAST(148000 AS Decimal(10, 0)), 5, N'c11.jpg', N'c11.jpg', N'c11.jpg', N'c11.jpg', N'c11.jpg', CAST(N'2020-06-17' AS Date), 2)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (12, N'Combo nhóm E', N'3 miếng gà giòn + 1 burger gà quay + 1 khoai tây chiên (lớn) + 2 pepsi lon', N'', 0, 1, CAST(199000 AS Decimal(10, 0)), 20, CAST(159200 AS Decimal(10, 0)), 5, N'c12.jpg', N'c12.jpg', N'c12.jpg', N'c12.jpg', N'c12.jpg', CAST(N'2020-06-17' AS Date), 2)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (13, N'Combo nhóm F', N'3 miếng gà giòn + 1 popcorn (lớn) + 1 khoai tây chiên (lớn) + 2 pepsi lon', N'', 0, 1, CAST(205000 AS Decimal(10, 0)), 20, CAST(164000 AS Decimal(10, 0)), 5, N'c13.jpg', N'c13.jpg', N'c13.jpg', N'c13.jpg', N'c13.jpg', CAST(N'2020-06-17' AS Date), 2)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (14, N'Combo gia đình A', N'8 miếng gà giòn + 2 khoai tây chiên (lớn) + 4 pepsi lon', N'', 0, 1, CAST(359000 AS Decimal(10, 0)), 20, CAST(287200 AS Decimal(10, 0)), 5, N'c14.jpg', N'c14.jpg', N'c14.jpg', N'c14.jpg', N'c14.jpg', CAST(N'2020-06-17' AS Date), 2)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (15, N'Combo gia đình B', N'5 miếng gà giòn + 2 burger gà quay + 2 khoai tây chiên (lớn) + 3 pepsi lon', N'', 0, 1, CAST(359000 AS Decimal(10, 0)), 20, CAST(287200 AS Decimal(10, 0)), 5, N'c15.jpg', N'c15.jpg', N'c15.jpg', N'c15.jpg', N'c15.jpg', CAST(N'2020-06-17' AS Date), 2)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (16, N'Combo Gà hoàng kim HDA', N'1 miếng gà hoàng kim + 1 khoai tây chiên (vừa) + 1 pepsi lon', N'', 0, 1, CAST(63000 AS Decimal(10, 0)), 0, CAST(63000 AS Decimal(10, 0)), 5, N'sp1.png', N'sp1.png', N'sp1.png', N'sp1.png', N'sp1.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (17, N'Combo Gà hoàng kim HDB', N'2 miếng gà hoàng kim + 1 khoai tây chiên (vừa) + 1 pepsi lon', N'', 0, 1, CAST(94000 AS Decimal(10, 0)), 0, CAST(94000 AS Decimal(10, 0)), 5, N'sp2.png', N'sp2.png', N'sp2.png', N'sp2.png', N'sp2.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (18, N'Combo Gà hoàng kim HDC', N'4 miếng gà hoàng kim + 1 popcorn (vừa) + 2 pepsi lon', N'', 0, 1, CAST(189000 AS Decimal(10, 0)), 0, CAST(189000 AS Decimal(10, 0)), 5, N'sp3.png', N'sp3.png', N'sp3.png', N'sp3.png', N'sp3.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (19, N'Gà hoàng kim (1 miếng)', N'1 miếng gà hoàng kim', N'', 0, 1, CAST(39000 AS Decimal(10, 0)), 0, CAST(39000 AS Decimal(10, 0)), 5, N'sp4.png', N'sp4.png', N'sp4.png', N'sp4.png', N'sp4.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (20, N'Gà hoàng kim (2 miếng)', N'2 miếng gà hoàng kim', N'', 0, 1, CAST(72000 AS Decimal(10, 0)), 0, CAST(72000 AS Decimal(10, 0)), 5, N'sp5.png', N'sp5.png', N'sp5.png', N'sp5.png', N'sp5.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (21, N'Gà hoàng kim (3 miếng)', N'3 miếng gà hoàng kim', N'', 0, 1, CAST(108000 AS Decimal(10, 0)), 0, CAST(108000 AS Decimal(10, 0)), 5, N'sp6.png', N'sp6.png', N'sp6.png', N'sp6.png', N'sp6.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (22, N'Gà hoàng kim (6 miếng)', N'6 miếng gà hoàng kim', N'', 0, 1, CAST(211000 AS Decimal(10, 0)), 0, CAST(211000 AS Decimal(10, 0)), 5, N'sp7.png', N'sp7.png', N'sp7.png', N'sp7.png', N'sp7.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (23, N'Gà hoàng kim (9 miếng)', N'9 miếng gà hoàng kim', N'', 1, 1, CAST(312000 AS Decimal(10, 0)), 0, CAST(312000 AS Decimal(10, 0)), 5, N'sp8.png', N'sp8.png', N'sp8.png', N'sp8.png', N'sp8.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (24, N'Thanh bí phô-mai (2 thanh)', N'2 thanh bí phô-mai', N'', 0, 1, CAST(26000 AS Decimal(10, 0)), 0, CAST(26000 AS Decimal(10, 0)), 5, N'sp9.png', N'sp9.png', N'sp9.png', N'sp9.png', N'sp9.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (25, N'Thanh bí phô-mai (3 thanh)', N'3 thanh bí phô-mai', N'', 0, 1, CAST(32000 AS Decimal(10, 0)), 0, CAST(32000 AS Decimal(10, 0)), 5, N'sp10.png', N'sp10.png', N'sp10.png', N'sp10.png', N'sp10.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (26, N'Thanh bí phô-mai (5 thanh)', N'5 thanh bí phô-mai', N'', 0, 1, CAST(52000 AS Decimal(10, 0)), 0, CAST(52000 AS Decimal(10, 0)), 5, N'sp11.png', N'sp11.png', N'sp11.png', N'sp11.png', N'sp11.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (27, N'Combo thanh bí phô-mai HDA', N'2 miếng gà giòn + 2 thanh bí phô-mai + 1 pepsi lon', N'', 0, 1, CAST(94000 AS Decimal(10, 0)), 0, CAST(94000 AS Decimal(10, 0)), 5, N'sp12.png', N'sp12.png', N'sp12.png', N'sp12.png', N'sp12.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (28, N'Combo thanh bí phô-mai HDB', N'1 burger gà quay + 2 thanh bí phô-mai + 1 pepsi lon', N'', 0, 1, CAST(74000 AS Decimal(10, 0)), 0, CAST(74000 AS Decimal(10, 0)), 5, N'sp13.png', N'sp13.png', N'sp13.png', N'sp13.png', N'sp13.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (29, N'Combo thanh bí phô-mai HDC', N'4 miếng gà giòn + 4 thanh bí phô-mai + 2 pepsi lon', N'', 0, 1, CAST(189000 AS Decimal(10, 0)), 0, CAST(189000 AS Decimal(10, 0)), 5, N'sp14.png', N'sp14.png', N'sp14.png', N'sp14.png', N'sp14.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (30, N'Gà bít-tết với cơm', N'1 phần gà bít-tết với cơm', N'', 0, 1, CAST(39000 AS Decimal(10, 0)), 0, CAST(39000 AS Decimal(10, 0)), 5, N'sp15.png', N'sp15.png', N'sp15.png', N'sp15.png', N'sp15.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (31, N'Gà bít-tết với khoai tây chiên', N'1 phần gà bít-tết với khoai tây chiên', N'', 0, 1, CAST(39000 AS Decimal(10, 0)), 0, CAST(39000 AS Decimal(10, 0)), 5, N'sp16.png', N'sp16.png', N'sp16.png', N'sp16.png', N'sp16.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (32, N'Combo gà bít-tết HDA', N'1 phần gà bít-tết với khoai tây chiên + 1 miếng gà giòn + 1 pepsi lon', N'', 0, 1, CAST(81000 AS Decimal(10, 0)), 0, CAST(81000 AS Decimal(10, 0)), 5, N'sp17.png', N'sp17.png', N'sp17.png', N'sp17.png', N'sp17.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (33, N'Combo gà bít-tết HDB', N'1 phần gà bít-tết với cơm + 1 miếng gà giòn + 1 pepsi lon', N'', 0, 1, CAST(81000 AS Decimal(10, 0)), 0, CAST(81000 AS Decimal(10, 0)), 5, N'sp18.png', N'sp18.png', N'sp18.png', N'sp18.png', N'sp18.png', CAST(N'2020-06-17' AS Date), 3)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (34, N'Gà rán (1 miếng)', N'1 miếng gà giòn', N'', 0, 1, CAST(36000 AS Decimal(10, 0)), 0, CAST(36000 AS Decimal(10, 0)), 5, N's1.jpg', N's1.jpg', N's1.jpg', N's1.jpg', N's1.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (35, N'Gà rán (2 miếng)', N'2 miếng gà giòn', N'', 0, 1, CAST(68000 AS Decimal(10, 0)), 0, CAST(68000 AS Decimal(10, 0)), 5, N's2.jpg', N's2.jpg', N's2.jpg', N's2.jpg', N's2.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (36, N'Gà rán (3 miếng)', N'3 miếng gà giòn', N'', 0, 1, CAST(99000 AS Decimal(10, 0)), 0, CAST(99000 AS Decimal(10, 0)), 5, N's3.jpg', N's3.jpg', N's3.jpg', N's3.jpg', N's3.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (37, N'Gà rán (6 miếng)', N'6 miếng gà giòn', N'', 0, 1, CAST(195000 AS Decimal(10, 0)), 0, CAST(195000 AS Decimal(10, 0)), 5, N's4.jpg', N's4.jpg', N's4.jpg', N's4.jpg', N's4.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (38, N'Gà rán (9 miếng)', N'9 miếng gà giòn', N'', 0, 1, CAST(289000 AS Decimal(10, 0)), 0, CAST(289000 AS Decimal(10, 0)), 5, N's5.jpg', N's5.jpg', N's5.jpg', N's5.jpg', N's5.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (39, N'Gà rán (12 miếng)', N'12 miếng gà giòn', N'', 0, 1, CAST(379000 AS Decimal(10, 0)), 0, CAST(379000 AS Decimal(10, 0)), 5, N's6.jpg', N's6.jpg', N's6.jpg', N's6.jpg', N's6.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (40, N'Gà quay (1 miếng)', N'1 miếng gà quay', N'', 0, 1, CAST(68000 AS Decimal(10, 0)), 0, CAST(68000 AS Decimal(10, 0)), 5, N's7.jpg', N's7.jpg', N's7.jpg', N's7.jpg', N's7.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (41, N'Phần hot wings (3 miếng)', N'3 miếng hot wings', N'', 0, 1, CAST(49000 AS Decimal(10, 0)), 0, CAST(49000 AS Decimal(10, 0)), 5, N's8.jpg', N's8.jpg', N's8.jpg', N's8.jpg', N's8.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (42, N'Phần hot wings (5 miếng)', N'5 miếng hot wings', N'', 0, 1, CAST(71000 AS Decimal(10, 0)), 0, CAST(71000 AS Decimal(10, 0)), 5, N's9.jpg', N's9.jpg', N's9.jpg', N's9.jpg', N's9.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (43, N'Combo Tekami 1', N'1 phần tekami', N'', 0, 1, CAST(34000 AS Decimal(10, 0)), 0, CAST(34000 AS Decimal(10, 0)), 5, N's10.png', N's10.png', N's10.png', N's10.png', N's10.png', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (44, N'Combo Tekami A', N'2 phần tekami', N'', 0, 1, CAST(61000 AS Decimal(10, 0)), 0, CAST(61000 AS Decimal(10, 0)), 5, N's11.png', N's11.png', N's11.png', N's11.png', N's11.png', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (45, N'Combo Tekami B', N'1 phần tekami + 1 miếng gà giòn + 1 pepsi lon', N'', 0, 1, CAST(83000 AS Decimal(10, 0)), 0, CAST(83000 AS Decimal(10, 0)), 5, N's12.png', N's12.png', N's12.png', N's12.png', N's12.png', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (46, N'Combo Tekami C', N'2 phần tekami + 1 khoai tây chiên (vừa) + 1 pepsi lon', N'', 0, 1, CAST(86000 AS Decimal(10, 0)), 0, CAST(86000 AS Decimal(10, 0)), 5, N's13.png', N's13.png', N's13.png', N's13.png', N's13.png', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (47, N'Combo Tekami D', N'3 phần tekami + 3 miếng gà giòn + 2 pepsi lon', N'', 0, 1, CAST(199000 AS Decimal(10, 0)), 0, CAST(199000 AS Decimal(10, 0)), 5, N's14.png', N's14.png', N's14.png', N's14.png', N's14.png', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (48, N'Cơm gà truyền thống', N'1 phần cơm gà truyền thống', N'', 1, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's15.jpg', N's15.jpg', N's15.jpg', N's15.jpg', N's15.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (49, N'Cơm gà giòn cay', N'1 phần cơm gà giòn cay', N'', 0, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's16.jpg', N's16.jpg', N's16.jpg', N's16.jpg', N's16.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (50, N'Cơm gà giòn không cay', N'1 phần cơm gà giòn không cay', N'', 0, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's17.jpg', N's17.jpg', N's17.jpg', N's17.jpg', N's17.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (51, N'Cơm phi lê gà quay tiêu', N'1 phần cơm phi lê gà quay tiêu', N'', 0, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's18.jpg', N's18.jpg', N's18.jpg', N's18.jpg', N's18.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (52, N'Cơm phi lê gà quay flava', N'1 phần cơm phi lê gà quay flava', N'', 0, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's19.jpg', N's19.jpg', N's19.jpg', N's19.jpg', N's19.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (53, N'Cơm gà xào sốt Nhật', N'1 phần cơm gà xào sốt Nhật', N'', 0, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's20.jpg', N's20.jpg', N's20.jpg', N's20.jpg', N's20.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (54, N'Cơm phi lê gà giòn', N'1 phần cơm phi lê gà giòn', N'', 0, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's21.jpg', N's21.jpg', N's21.jpg', N's21.jpg', N's21.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (55, N'Cơm gà xiên que', N'1 phần cơm gà xiên que', N'', 0, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's22.jpg', N's22.jpg', N's22.jpg', N's22.jpg', N's22.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (56, N'Burger tôm', N'1 burger tôm', N'', 0, 1, CAST(42000 AS Decimal(10, 0)), 0, CAST(42000 AS Decimal(10, 0)), 5, N's23.jpg', N's23.jpg', N's23.jpg', N's23.jpg', N's23.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (57, N'Burger gà quay flava', N'1 burger gà quay flava', N'', 0, 1, CAST(47000 AS Decimal(10, 0)), 0, CAST(47000 AS Decimal(10, 0)), 5, N's24.jpg', N's24.jpg', N's24.jpg', N's24.jpg', N's24.jpg', CAST(N'2020-06-17' AS Date), 4)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (58, N'Burger zinger', N'1 burgre zinger', N'', 0, 1, CAST(51000 AS Decimal(10, 0)), 0, CAST(51000 AS Decimal(10, 0)), 5, N's25.jpg', N's25.jpg', N's25.jpg', N's25.jpg', N's25.jpg', CAST(N'2020-06-17' AS Date), 5)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (59, N'Popcorn (vừa)', N'1 phần popconrn (vừa)', N'', 0, 1, CAST(37000 AS Decimal(10, 0)), 0, CAST(37000 AS Decimal(10, 0)), 5, N's26.jpg', N's26.jpg', N's26.jpg', N's26.jpg', N's26.jpg', CAST(N'2020-06-17' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (60, N'Popcorn (lớn)', N'1 phần popcorn (lớn)', N'', 0, 1, CAST(57000 AS Decimal(10, 0)), 0, CAST(57000 AS Decimal(10, 0)), 5, N's27.jpg', N's27.jpg', N's27.jpg', N's27.jpg', N's27.jpg', CAST(N'2020-06-17' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (61, N'Phô mai viên (4 viên)', N'4 viên phô mai', N'', 0, 1, CAST(29000 AS Decimal(10, 0)), 0, CAST(29000 AS Decimal(10, 0)), 5, N's28.jpg', N's28.jpg', N's28.jpg', N's28.jpg', N's28.jpg', CAST(N'2020-06-17' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (62, N'Phô mai viên (6 viên)', N'6 viên phô mai', N'', 0, 1, CAST(39000 AS Decimal(10, 0)), 0, CAST(39000 AS Decimal(10, 0)), 5, N's29.jpg', N's29.jpg', N's29.jpg', N's29.jpg', N's29.jpg', CAST(N'2020-06-17' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (63, N'Mashies nhân Gravy (3 viên)', N'3 viên mashies nhân gravy', N'', 0, 1, CAST(19000 AS Decimal(10, 0)), 0, CAST(19000 AS Decimal(10, 0)), 5, N's30.jpg', N's30.jpg', N's30.jpg', N's30.jpg', N's30.jpg', CAST(N'2020-06-17' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (64, N'Mashies nhân Gravy (5 viên)', N'5 viên mashies nhân gravy', N'', 0, 1, CAST(29000 AS Decimal(10, 0)), 0, CAST(29000 AS Decimal(10, 0)), 5, N's31.jpg', N's31.jpg', N's31.jpg', N's31.jpg', N's31.jpg', CAST(N'2020-06-17' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (65, N'Mashies nhân rau củ (3 viên)', N'3 viên mahies nhân rau củ', N'', 0, 1, CAST(25000 AS Decimal(10, 0)), 0, CAST(25000 AS Decimal(10, 0)), 5, N's32.jpg', N's32.jpg', N's32.jpg', N's32.jpg', N's32.jpg', CAST(N'2020-06-17' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (66, N'Mashies nhân rau củ (5 viên)', N'5 viên mashies nhân rau củ', N'', 0, 1, CAST(35000 AS Decimal(10, 0)), 0, CAST(35000 AS Decimal(10, 0)), 5, N's33.jpg', N's33.jpg', N's33.jpg', N's33.jpg', N's33.jpg', CAST(N'2020-06-17' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (67, N'Cá thanh (3 thanh)', N'3 thanh cá', N'', 0, 1, CAST(41000 AS Decimal(10, 0)), 0, CAST(41000 AS Decimal(10, 0)), 5, N's34.jpg', N's34.jpg', N's34.jpg', N's34.jpg', N's34.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (68, N'Xà Lách KFC', N'1 xuất xà lách KFC', N'', 0, 1, CAST(20000 AS Decimal(10, 0)), 0, CAST(20000 AS Decimal(10, 0)), 5, N's36.jpg', N's36.jpg', N's36.jpg', N's36.jpg', N's36.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (69, N'Gà xiên que (2 thanh)', N'2 thanh gà xiên que', N'', 2, 1, CAST(31000 AS Decimal(10, 0)), 0, CAST(31000 AS Decimal(10, 0)), 5, N's37.jpg', N's37.jpg', N's37.jpg', N's37.jpg', N's37.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (70, N'Khoai tây chiên (vừa)', N'1 xuất khoai tây chiên (vừa)', N'', 0, 1, CAST(14000 AS Decimal(10, 0)), 0, CAST(14000 AS Decimal(10, 0)), 5, N's38.jpg', N's38.jpg', N's38.jpg', N's38.jpg', N's38.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (71, N'Khoai tây chiên (lớn)', N'1 xuất khoai tây chiên (lớn)', N'', 0, 1, CAST(27000 AS Decimal(10, 0)), 0, CAST(27000 AS Decimal(10, 0)), 5, N's39.jpg', N's39.jpg', N's39.jpg', N's39.jpg', N's39.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (72, N'Khoai tây chiên (đại)', N'1 xuất khoai tây chiên (đại)', N'', 0, 1, CAST(37000 AS Decimal(10, 0)), 0, CAST(37000 AS Decimal(10, 0)), 5, N's40.jpg', N's40.jpg', N's40.jpg', N's40.jpg', N's40.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (73, N'Bắp cải trộn (vừa)', N'1 xuất bắp cải trộn (vừa)', N'', 0, 1, CAST(12000 AS Decimal(10, 0)), 0, CAST(12000 AS Decimal(10, 0)), 5, N's41.jpg', N's41.jpg', N's41.jpg', N's41.jpg', N's41.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (74, N'Bắp cải trộn (lớn)', N'1 xuất bắp cải trộn (lớn)', N'', 0, 1, CAST(22000 AS Decimal(10, 0)), 0, CAST(22000 AS Decimal(10, 0)), 5, N's41.jpg', N's41.jpg', N's41.jpg', N's41.jpg', N's41.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (75, N'Bắp cải trộn (đại)', N'1 xuất bắp cải trộn (đại)', N'', 0, 1, CAST(32000 AS Decimal(10, 0)), 0, CAST(32000 AS Decimal(10, 0)), 5, N's43.jpg', N's43.jpg', N's43.jpg', N's43.jpg', N's43.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (76, N'Khoai tây nghiền (vừa)', N'1 xuất khoai tây nghiền (vừa)', N'', 0, 1, CAST(12000 AS Decimal(10, 0)), 0, CAST(12000 AS Decimal(10, 0)), 5, N's44.jpg', N's44.jpg', N's44.jpg', N's44.jpg', N's44.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (77, N'Khoai tây nghiền (lớn)', N'1 xuất khoai tây nghiền (lớn)', N'', 0, 1, CAST(22000 AS Decimal(10, 0)), 0, CAST(22000 AS Decimal(10, 0)), 5, N's45.jpg', N's45.jpg', N's45.jpg', N's45.jpg', N's45.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (78, N'Khoai tây nghiền (đại)', N'1 xuất khoai tây nghiền (đại)', N'', 0, 1, CAST(32000 AS Decimal(10, 0)), 0, CAST(32000 AS Decimal(10, 0)), 5, N's46.jpg', N's46.jpg', N's46.jpg', N's46.jpg', N's46.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (79, N'Cơm trắng', N'1 xuất cơm trắng', N'', 0, 1, CAST(10000 AS Decimal(10, 0)), 0, CAST(10000 AS Decimal(10, 0)), 5, N's47.jpg', N's47.jpg', N's47.jpg', N's47.jpg', N's47.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (80, N'Súp gà', N'1 xuất súp gà', N'', 0, 1, CAST(12000 AS Decimal(10, 0)), 0, CAST(12000 AS Decimal(10, 0)), 5, N's48.jpg', N's48.jpg', N's48.jpg', N's48.jpg', N's48.jpg', CAST(N'2020-06-18' AS Date), 6)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (81, N'Mochi trà xanh (1 cái)', N'1 bánh mochi trà xanh', N'', 0, 1, CAST(17000 AS Decimal(10, 0)), 0, CAST(17000 AS Decimal(10, 0)), 5, N's49.png', N's49.png', N's49.png', N's49.png', N's49.png', CAST(N'2020-06-18' AS Date), 7)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (82, N'Mochi trà xanh (3 cái)', N'3 bánh mochi trà xanh', N'', 0, 1, CAST(42000 AS Decimal(10, 0)), 0, CAST(42000 AS Decimal(10, 0)), 5, N's50.png', N's50.png', N's50.png', N's50.png', N's50.png', CAST(N'2020-06-18' AS Date), 7)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (83, N'Mochi socola (1 cái)', N'1 bánh mochi socola', N'', 0, 1, CAST(17000 AS Decimal(10, 0)), 0, CAST(17000 AS Decimal(10, 0)), 5, N's51.png', N's51.png', N's51.png', N's51.png', N's51.png', CAST(N'2020-06-18' AS Date), 7)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (84, N'Mochi socola (3 cái)', N'3 bánh mochi socola', N'', 0, 1, CAST(42000 AS Decimal(10, 0)), 0, CAST(42000 AS Decimal(10, 0)), 5, N's52.png', N's52.png', N's52.png', N's52.png', N's52.png', CAST(N'2020-06-18' AS Date), 7)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (85, N'Pepsi lon', N'1 lon pepsi', N'', 0, 1, CAST(17000 AS Decimal(10, 0)), 0, CAST(17000 AS Decimal(10, 0)), 5, N's53.png', N's53.png', N's53.png', N's53.png', N's53.png', CAST(N'2020-06-18' AS Date), 8)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (86, N'7Up lon', N'1 lon 7Up', N'', 0, 1, CAST(17000 AS Decimal(10, 0)), 0, CAST(17000 AS Decimal(10, 0)), 5, N's54.png', N's54.png', N's54.png', N's54.png', N's54.png', CAST(N'2020-06-18' AS Date), 8)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (87, N'Pepsi Diet lon', N'1 lon pepsi diet', N'', 0, 1, CAST(17000 AS Decimal(10, 0)), 0, CAST(17000 AS Decimal(10, 0)), 5, N's55.jpg', N's55.jpg', N's55.jpg', N's55.jpg', N's55.jpg', CAST(N'2020-06-18' AS Date), 8)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (88, N'Sữa Milo', N'1 hộp sữa Milo', N'', 0, 1, CAST(19000 AS Decimal(10, 0)), 0, CAST(19000 AS Decimal(10, 0)), 5, N's56.jpg', N's56.jpg', N's56.jpg', N's56.jpg', N's56.jpg', CAST(N'2020-06-18' AS Date), 8)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (89, N'Aquafina', N'1 chai nước lọc Aquafina', N'', 0, 1, CAST(15000 AS Decimal(10, 0)), 0, CAST(15000 AS Decimal(10, 0)), 5, N's57.jpg', N's57.jpg', N's57.jpg', N's57.jpg', N's57.jpg', CAST(N'2020-06-18' AS Date), 8)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (90, N'Twister lon', N'1 lon twister', N'', 0, 1, CAST(17000 AS Decimal(10, 0)), 0, CAST(17000 AS Decimal(10, 0)), 5, N's58.png', N's58.png', N's58.png', N's58.png', N's58.png', CAST(N'2020-06-18' AS Date), 8)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (91, N'Trà đào', N'1 cốc trà đào', N'', 0, 1, CAST(24000 AS Decimal(10, 0)), 0, CAST(24000 AS Decimal(10, 0)), 5, N's59.jpg', N's59.jpg', N's59.jpg', N's59.jpg', N's59.jpg', CAST(N'2020-06-18' AS Date), 8)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (92, N'Bánh trứng (1 cái)', N'1 bánh trứng', N'', 0, 1, CAST(17000 AS Decimal(10, 0)), 0, CAST(17000 AS Decimal(10, 0)), 5, N's60.jpg', N's60.jpg', N's60.jpg', N's60.jpg', N's60.jpg', CAST(N'2020-06-18' AS Date), 7)
INSERT [dbo].[Product] ([id_product], [name], [description], [information], [review], [availability], [price], [salePercent], [salePrice], [rate], [mainPhoto], [photo1], [photo2], [photo3], [photo4], [updated], [id_category]) VALUES (93, N'Bánh trứng (4 cái)', N'4 bánh trứng', N'', 0, 1, CAST(50000 AS Decimal(10, 0)), 0, CAST(50000 AS Decimal(10, 0)), 5, N's61.jpg', N's61.jpg', N's61.jpg', N's61.jpg', N's61.jpg', CAST(N'2020-06-18' AS Date), 7)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[ProductDetail] ON 

INSERT [dbo].[ProductDetail] ([id_productDetail], [name], [amount], [availability], [extraPrice], [id_product]) VALUES (1, N'miếng hot wiings', 3, 1, CAST(20000 AS Decimal(10, 0)), 2)
INSERT [dbo].[ProductDetail] ([id_productDetail], [name], [amount], [availability], [extraPrice], [id_product]) VALUES (2, N'khoai tây chiên (lớn)', 1, 1, CAST(40000 AS Decimal(10, 0)), 2)
INSERT [dbo].[ProductDetail] ([id_productDetail], [name], [amount], [availability], [extraPrice], [id_product]) VALUES (3, N'pepsi lon', 1, 1, CAST(15000 AS Decimal(10, 0)), 2)
INSERT [dbo].[ProductDetail] ([id_productDetail], [name], [amount], [availability], [extraPrice], [id_product]) VALUES (4, N'pepsi lon', 1, 0, CAST(15000 AS Decimal(10, 0)), 2)
INSERT [dbo].[ProductDetail] ([id_productDetail], [name], [amount], [availability], [extraPrice], [id_product]) VALUES (5, N'coca lon', 1, 0, CAST(15000 AS Decimal(10, 0)), 2)
INSERT [dbo].[ProductDetail] ([id_productDetail], [name], [amount], [availability], [extraPrice], [id_product]) VALUES (6, N'7up lon', 1, 1, CAST(15000 AS Decimal(10, 0)), 1)
INSERT [dbo].[ProductDetail] ([id_productDetail], [name], [amount], [availability], [extraPrice], [id_product]) VALUES (7, N'coca lon', 1, 1, CAST(15000 AS Decimal(10, 0)), 1)
INSERT [dbo].[ProductDetail] ([id_productDetail], [name], [amount], [availability], [extraPrice], [id_product]) VALUES (8, N'pepsi lon', 1, 1, CAST(15000 AS Decimal(10, 0)), 1)
SET IDENTITY_INSERT [dbo].[ProductDetail] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id_user], [userName], [password], [name], [email], [status], [createDate], [id_userGroup]) VALUES (1, N'hieumta', N'hieu', N'Vũ Minh Hiếu', N'vuhieupro1999@gmail.com', NULL, CAST(N'2020-06-16 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[User] ([id_user], [userName], [password], [name], [email], [status], [createDate], [id_userGroup]) VALUES (2, N'hungmta', N'hung', N'Nguyễn Đức Hưng', N'nguyenduchung99hd@gmail.com', NULL, CAST(N'2020-06-16 00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Cart] ADD  CONSTRAINT [DF_Cart_subtotal]  DEFAULT ((0)) FOR [subtotal]
GO
ALTER TABLE [dbo].[Cart] ADD  CONSTRAINT [DF_Cart_total]  DEFAULT ((0)) FOR [total]
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF_CartDetail_price]  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF_CartDetail_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF_CartDetail_intoMoney]  DEFAULT ((0)) FOR [intoMoney]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_BillStatus] FOREIGN KEY([id_status])
REFERENCES [dbo].[BillStatus] ([id_status])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_BillStatus]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Customer] FOREIGN KEY([id_customer])
REFERENCES [dbo].[Customer] ([id_customer])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Customer]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Bill] FOREIGN KEY([id_bill])
REFERENCES [dbo].[Bill] ([id_bill])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Bill]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Product] FOREIGN KEY([id_product])
REFERENCES [dbo].[Product] ([id_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Product]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Customer] FOREIGN KEY([id_customer])
REFERENCES [dbo].[Customer] ([id_customer])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Customer]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Cart] FOREIGN KEY([id_customer])
REFERENCES [dbo].[Cart] ([id_cart])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Cart]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Product] FOREIGN KEY([id_product])
REFERENCES [dbo].[Product] ([id_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Product]
GO
ALTER TABLE [dbo].[Credential]  WITH CHECK ADD  CONSTRAINT [FK_Credential_Role] FOREIGN KEY([id_role])
REFERENCES [dbo].[Role] ([id_role])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Credential] CHECK CONSTRAINT [FK_Credential_Role]
GO
ALTER TABLE [dbo].[Credential]  WITH CHECK ADD  CONSTRAINT [FK_Credential_UserGroup] FOREIGN KEY([id_userGroup])
REFERENCES [dbo].[UserGroup] ([id_userGroup])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Credential] CHECK CONSTRAINT [FK_Credential_UserGroup]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_DiscountCode] FOREIGN KEY([id_discountCode])
REFERENCES [dbo].[DiscountCode] ([id_discountCode])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_DiscountCode]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([id_category])
REFERENCES [dbo].[Category] ([id_category])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetail_Product] FOREIGN KEY([id_product])
REFERENCES [dbo].[Product] ([id_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductDetail] CHECK CONSTRAINT [FK_ProductDetail_Product]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([id_userGroup])
REFERENCES [dbo].[UserGroup] ([id_userGroup])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
USE [master]
GO
ALTER DATABASE [SnackShopDB] SET  READ_WRITE 
GO
