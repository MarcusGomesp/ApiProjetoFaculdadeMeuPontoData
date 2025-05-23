USE [PostgresSQLDatabase]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 27/04/2025 20:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tab_banco_horas]    Script Date: 27/04/2025 20:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tab_banco_horas](
	[user_id] [int] NOT NULL,
	[data] [datetime2](7) NOT NULL,
	[total_horas_trabalhadas] [time](7) NULL,
	[qtd_solicitacoes_pendentes] [int] NOT NULL,
 CONSTRAINT [PK_tab_banco_horas] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tab_cadastro]    Script Date: 27/04/2025 20:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tab_cadastro](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](max) NOT NULL,
	[cargo] [nvarchar](max) NOT NULL,
	[departamento] [nvarchar](max) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[cpf] [bigint] NOT NULL,
	[senha] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_tab_cadastro] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tab_perfil]    Script Date: 27/04/2025 20:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tab_perfil](
	[user_id] [int] NOT NULL,
	[url_profile_pic] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_tab_perfil] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tab_registro]    Script Date: 27/04/2025 20:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tab_registro](
	[id_registro] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[data_inicio] [datetime2](7) NOT NULL,
	[qtde_batidas] [int] NOT NULL,
	[hora_extra] [time](7) NULL,
	[fim] [datetime2](7) NULL,
	[saida_almoco] [datetime2](7) NULL,
	[volta_almoco] [datetime2](7) NULL,
	[total_hora] [time](7) NULL,
 CONSTRAINT [PK_tab_registro] PRIMARY KEY CLUSTERED 
(
	[id_registro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tab_solicitacao]    Script Date: 27/04/2025 20:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tab_solicitacao](
	[id_solicitacao] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[id_registro] [int] NOT NULL,
	[horario] [time](7) NOT NULL,
	[status] [nvarchar](max) NOT NULL,
	[observacao] [nvarchar](max) NULL,
 CONSTRAINT [PK_tab_solicitacao] PRIMARY KEY CLUSTERED 
(
	[id_solicitacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tab_solicitacao] ADD  DEFAULT (N'') FOR [status]
GO
ALTER TABLE [dbo].[tab_banco_horas]  WITH CHECK ADD  CONSTRAINT [FK_BancoHoras_Cadastro] FOREIGN KEY([user_id])
REFERENCES [dbo].[tab_cadastro] ([user_id])
GO
ALTER TABLE [dbo].[tab_banco_horas] CHECK CONSTRAINT [FK_BancoHoras_Cadastro]
GO
ALTER TABLE [dbo].[tab_perfil]  WITH CHECK ADD  CONSTRAINT [FK_Perfil_Cadastro] FOREIGN KEY([user_id])
REFERENCES [dbo].[tab_cadastro] ([user_id])
GO
ALTER TABLE [dbo].[tab_perfil] CHECK CONSTRAINT [FK_Perfil_Cadastro]
GO
ALTER TABLE [dbo].[tab_registro]  WITH CHECK ADD  CONSTRAINT [FK_Registro_Cadastro] FOREIGN KEY([user_id])
REFERENCES [dbo].[tab_cadastro] ([user_id])
GO
ALTER TABLE [dbo].[tab_registro] CHECK CONSTRAINT [FK_Registro_Cadastro]
GO
ALTER TABLE [dbo].[tab_solicitacao]  WITH CHECK ADD  CONSTRAINT [FK_Solicitacao_Cadastro] FOREIGN KEY([user_id])
REFERENCES [dbo].[tab_cadastro] ([user_id])
GO
ALTER TABLE [dbo].[tab_solicitacao] CHECK CONSTRAINT [FK_Solicitacao_Cadastro]
GO
ALTER TABLE [dbo].[tab_solicitacao]  WITH CHECK ADD  CONSTRAINT [FK_Solicitacao_Registro] FOREIGN KEY([id_registro])
REFERENCES [dbo].[tab_registro] ([id_registro])
GO
ALTER TABLE [dbo].[tab_solicitacao] CHECK CONSTRAINT [FK_Solicitacao_Registro]
GO
