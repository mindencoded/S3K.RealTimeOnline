﻿DBCC CHECKIDENT ('[INVOICE]', RESEED, 0);
GO
SET IDENTITY_INSERT [INVOICE] ON
INSERT INTO [INVOICE] ([ID], [ORDER_ID], [INVOICE_DATE], [DUE_DATE], [TAX], [SHIPPING], [AMOUNT_DUE]) VALUES 
     (5 , 31, '2006-03-22', NULL, 0, 0, 0)
    ,(6 , 32, '2006-03-22', NULL, 0, 0, 0)
    ,(7 , 40, '2006-03-24', NULL, 0, 0, 0)
    ,(8 , 39, '2006-03-24', NULL, 0, 0, 0)
    ,(9 , 38, '2006-03-24', NULL, 0, 0, 0)
    ,(10, 37, '2006-03-24', NULL, 0, 0, 0)
    ,(11, 36, '2006-03-24', NULL, 0, 0, 0)
    ,(12, 35, '2006-03-24', NULL, 0, 0, 0)
    ,(13, 34, '2006-03-24', NULL, 0, 0, 0)
    ,(14, 33, '2006-03-24', NULL, 0, 0, 0)
    ,(15, 30, '2006-03-24', NULL, 0, 0, 0)
    ,(16, 56, '2006-04-03', NULL, 0, 0, 0)
    ,(17, 55, '2006-04-04', NULL, 0, 0, 0)
    ,(18, 51, '2006-04-04', NULL, 0, 0, 0)
    ,(19, 50, '2006-04-04', NULL, 0, 0, 0)
    ,(20, 48, '2006-04-04', NULL, 0, 0, 0)
    ,(21, 47, '2006-04-04', NULL, 0, 0, 0)
    ,(22, 46, '2006-04-04', NULL, 0, 0, 0)
    ,(23, 45, '2006-04-04', NULL, 0, 0, 0)
    ,(24, 79, '2006-04-04', NULL, 0, 0, 0)
    ,(25, 78, '2006-04-04', NULL, 0, 0, 0)
    ,(26, 77, '2006-04-04', NULL, 0, 0, 0)
    ,(27, 76, '2006-04-04', NULL, 0, 0, 0)
    ,(28, 75, '2006-04-04', NULL, 0, 0, 0)
    ,(29, 74, '2006-04-04', NULL, 0, 0, 0)
    ,(30, 73, '2006-04-04', NULL, 0, 0, 0)
    ,(31, 72, '2006-04-04', NULL, 0, 0, 0)
    ,(32, 71, '2006-04-04', NULL, 0, 0, 0)
    ,(33, 70, '2006-04-04', NULL, 0, 0, 0)
    ,(34, 69, '2006-04-04', NULL, 0, 0, 0)
    ,(35, 67, '2006-04-04', NULL, 0, 0, 0)
    ,(36, 42, '2006-04-04', NULL, 0, 0, 0)
    ,(37, 60, '2006-04-04', NULL, 0, 0, 0)
    ,(38, 63, '2006-04-04', NULL, 0, 0, 0)
    ,(39, 58, '2006-04-04', NULL, 0, 0, 0);
SET IDENTITY_INSERT [INVOICE] OFF