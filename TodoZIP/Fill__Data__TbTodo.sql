
UPDATE tbTodo set IsDeleted = 0, IsCompleted = 0;

SELECT * FROM [TODO].[dbo].[tbTodo];

/*
DELETE FROM tbTodo; 
DBCC CHECKIDENT ('[tbTodo]', RESEED, 0);
SET IDENTITY_INSERT [dbo].[tbTodo] ON 
INSERT [dbo].[tbTodo] ([Id], [Title], [IsCompleted], [IsDeleted]) VALUES (1, N'Finish your Azure Training', 0, 0)
INSERT [dbo].[tbTodo] ([Id], [Title], [IsCompleted], [IsDeleted]) VALUES (2, N'Sell your bmw, get a ferrari', 0, 0)
INSERT [dbo].[tbTodo] ([Id], [Title], [IsCompleted], [IsDeleted]) VALUES (3, N'Goto Paris and Roma', 0, 0)
INSERT [dbo].[tbTodo] ([Id], [Title], [IsCompleted], [IsDeleted]) VALUES (4, N'Buy some good books for reading', 0, 0)
INSERT [dbo].[tbTodo] ([Id], [Title], [IsCompleted], [IsDeleted]) VALUES (5, N'Upload your codes to Github', 0, 0)
INSERT [dbo].[tbTodo] ([Id], [Title], [IsCompleted], [IsDeleted]) VALUES (6, N'AAA', 0, 0)
INSERT [dbo].[tbTodo] ([Id], [Title], [IsCompleted], [IsDeleted]) VALUES (7, N'BBB', 0, 0)
SET IDENTITY_INSERT [dbo].[tbTodo] OFF 
SELECT * FROM [TODO].[dbo].[tbTodo];
*/
