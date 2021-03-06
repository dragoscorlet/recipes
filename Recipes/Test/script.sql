USE [master]
GO
/****** Object:  Database [Recipes]    Script Date: 2/14/2019 11:48:52 PM ******/
CREATE DATABASE [Recipes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Recipes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS01\MSSQL\DATA\Recipes.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Recipes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS01\MSSQL\DATA\Recipes_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Recipes] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Recipes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Recipes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Recipes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Recipes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Recipes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Recipes] SET ARITHABORT OFF 
GO
ALTER DATABASE [Recipes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Recipes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Recipes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Recipes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Recipes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Recipes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Recipes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Recipes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Recipes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Recipes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Recipes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Recipes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Recipes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Recipes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Recipes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Recipes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Recipes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Recipes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Recipes] SET  MULTI_USER 
GO
ALTER DATABASE [Recipes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Recipes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Recipes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Recipes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Recipes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Recipes] SET QUERY_STORE = OFF
GO
USE [Recipes]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Recipes]
GO
/****** Object:  UserDefinedTableType [dbo].[IngredientIdsList]    Script Date: 2/14/2019 11:48:52 PM ******/
CREATE TYPE [dbo].[IngredientIdsList] AS TABLE(
	[IngredientId] [int] NULL
)
GO
/****** Object:  Table [dbo].[Vendor_Products]    Script Date: 2/14/2019 11:48:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor_Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[MassInGrams] [int] NULL,
	[VolumeInMl] [int] NULL,
	[Price] [decimal](18, 4) NULL,
	[Currency] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[InStock] [bit] NULL,
	[SoldByPiece] [bit] NULL,
	[Url] [nvarchar](max) NULL,
	[Brand] [nvarchar](max) NULL,
	[Vendor] [nvarchar](max) NULL,
	[IdLanguage] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_Brands]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[v_Brands] as
select distinct Brand from dbo.Vendor_Products
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IdLanguage] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients_NoiseWords]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients_NoiseWords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Word] [nvarchar](max) NULL,
	[IdLanguage] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipe_Images]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe_Images](
	[IdRecipe] [int] NULL,
	[Url] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipe_Ingredients]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe_Ingredients](
	[IdRecipe] [int] NULL,
	[IdIngredient] [int] NULL,
	[QuantityInGrams] [int] NULL,
	[VolumeInMl] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[PrepTimeMin] [int] NULL,
	[Servings] [int] NULL,
	[IdLanguage] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor_IngredientProductMapping]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor_IngredientProductMapping](
	[IdIngredient] [int] NULL,
	[IdProduct] [int] NULL,
	[MappingConfidence] [decimal](18, 4) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ingredients] ADD  DEFAULT ((1)) FOR [IdLanguage]
GO
ALTER TABLE [dbo].[Ingredients_NoiseWords] ADD  DEFAULT ((1)) FOR [IdLanguage]
GO
ALTER TABLE [dbo].[Recipes] ADD  DEFAULT ((1)) FOR [IdLanguage]
GO
ALTER TABLE [dbo].[Vendor_Products] ADD  DEFAULT ((1)) FOR [IdLanguage]
GO
ALTER TABLE [dbo].[Ingredients]  WITH CHECK ADD FOREIGN KEY([IdLanguage])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Ingredients_NoiseWords]  WITH CHECK ADD FOREIGN KEY([IdLanguage])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Recipe_Images]  WITH CHECK ADD FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO
ALTER TABLE [dbo].[Recipe_Ingredients]  WITH CHECK ADD FOREIGN KEY([IdIngredient])
REFERENCES [dbo].[Ingredients] ([Id])
GO
ALTER TABLE [dbo].[Recipe_Ingredients]  WITH CHECK ADD FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD FOREIGN KEY([IdLanguage])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Vendor_IngredientProductMapping]  WITH CHECK ADD FOREIGN KEY([IdIngredient])
REFERENCES [dbo].[Ingredients] ([Id])
GO
ALTER TABLE [dbo].[Vendor_IngredientProductMapping]  WITH CHECK ADD FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Vendor_Products] ([Id])
GO
ALTER TABLE [dbo].[Vendor_Products]  WITH CHECK ADD FOREIGN KEY([IdLanguage])
REFERENCES [dbo].[Languages] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[GetIngredientsByPartialName]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetIngredientsByPartialName]
@PartialName NVARCHAR(MAX)
AS
SELECT Id, [Name], IdLanguage FROM Ingredients 
WHERE [Name] like @PartialName + '%'
GO
/****** Object:  StoredProcedure [dbo].[GetNoiseWords]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[GetNoiseWords]
(
	@Language NVARCHAR(MAX)
)
AS
BEGIN

SELECT 
	word 
FROM dbo.Ingredients_NoiseWords words
INNER JOIN dbo.Languages lg ON words.IdLanguage = lg.Id
WHERE lg.[Name] = @Language

END
GO
/****** Object:  StoredProcedure [dbo].[GetRecipe]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRecipe]
@RecipeId int
AS
SELECT 
	r.Id, 
	r.[Description], 
	r.[Name], 
	r.PrepTimeMin, 
	r.Servings, 
	r.IdLanguage, 
	r.[Url],
	ingredients.[Name] IngredientName,
	ingredients.[Id] IngredientId,
	images.[Url] ImageUrl
FROM dbo.Recipes r
INNER JOIN dbo.Recipe_Images images ON r.Id = images.IdRecipe
INNER JOIN dbo.Recipe_Ingredients ringredients ON r.Id = ringredients.IdRecipe
INNER JOIN dbo.Ingredients ingredients ON ingredients.Id = ringredients.IdIngredient
WHERE r.Id = @RecipeId
GO
/****** Object:  StoredProcedure [dbo].[GetRecipes]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRecipes]
@Ingredients IngredientIdsList READONLY
AS
SELECT r.Id, r.[Name], r.[Url], r.[Description], r.PrepTimeMin, r.Servings, r.IdLanguage, im.[Url] [Image] FROM dbo.Recipes r
INNER JOIN dbo.Recipe_Images im ON r.Id = im.IdRecipe
INNER JOIN dbo.Recipe_Ingredients ri ON r.Id = ri.IdRecipe
WHERE ri.IdIngredient in (SELECT IngredientId FROM @Ingredients)

GO
/****** Object:  StoredProcedure [dbo].[GetVendorProductsForRecipe]    Script Date: 2/14/2019 11:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVendorProductsForRecipe]
@Ingredients IngredientIdsList READONLY
AS
SELECT products.* FROM dbo.Recipe_Ingredients ringredients
INNER JOIN dbo.Ingredients ingredients ON ringredients.IdIngredient = ingredients.Id
LEFt JOIN dbo.Vendor_IngredientProductMapping mapping ON ingredients.Id = mapping.IdIngredient 
			AND mapping.MappingConfidence = 100
LEFT JOIN dbo.Vendor_Products products ON mapping.IdProduct = products.Id
WHERE ingredients.Id IN (SELECT IngredientId FROM @Ingredients)
GO
USE [master]
GO
ALTER DATABASE [Recipes] SET  READ_WRITE 
GO
