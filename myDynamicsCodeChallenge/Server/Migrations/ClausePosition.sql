SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
GO
-- Clauses
CREATE TABLE [dbo].[Clauses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](1200) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clauses] ADD  CONSTRAINT [PK_Clauses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

-- ListPositions
CREATE TABLE [dbo].[ListPositions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](1200) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ListPositions] ADD  CONSTRAINT [PK_ListPositions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

-- ClausePosition
CREATE TABLE [dbo].[ClausePositions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClauseId] [int] NOT NULL,
	[PositionId] [int] NOT NULL,
	[Order] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClausePositions] ADD  CONSTRAINT [PK_ClausePositions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClausePositions]  WITH CHECK ADD  CONSTRAINT [FK_ClausePositions_Clause] FOREIGN KEY([ClauseId])
REFERENCES [dbo].[Clauses] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClausePositions] CHECK CONSTRAINT [FK_ClausePositions_Clause]
GO
ALTER TABLE [dbo].[ClausePositions]  WITH CHECK ADD  CONSTRAINT [FK_ClausePositions_ListPosition] FOREIGN KEY([PositionId])
REFERENCES [dbo].[ListPositions] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClausePositions] CHECK CONSTRAINT [FK_ClausePositions_ListPosition]
GO


-- MoveClauseToPosition
CREATE PROCEDURE [dbo].[MoveClauseToPosition] 
(
    @Id INT,
    @Position INT
)
AS
DECLARE @order INT
SELECT @order = ISNULL(Max([Order]),0) FROM ClausePositions WHERE PositionId = @Position 
UPDATE ClausePositions SET 
    PositionId = @Position, 
    [Order] = @order + 1
WHERE ClauseId = @Id
GO

-- ResetClauses
CREATE PROCEDURE [dbo].[ResetClauses] 
AS
UPDATE ClausePositions SET PositionId = 1 
GO
