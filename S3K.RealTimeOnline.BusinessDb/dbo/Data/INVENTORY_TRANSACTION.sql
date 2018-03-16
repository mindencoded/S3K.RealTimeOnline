﻿DBCC CHECKIDENT ('[INVENTORY_TRANSACTION]', RESEED, 0);
GO
SET IDENTITY_INSERT INVENTORY_TRANSACTION ON
INSERT INTO [INVENTORY_TRANSACTION] ([ID], [TYPE_ID], [TRANSACTION_CREATED_DATE], [TRANSACTION_MODIFIED_DATE], [PRODUCT_ID], [QUANTITY], [PURCHASE_ORDER_ID], [CUSTOMER_ORDER_ID], [COMMENTS]) VALUES 
     (35 , 1, '2006-03-22', '2006-03-22', 80, 75 , NULL, NULL, NULL)
    ,(36 , 1, '2006-03-22', '2006-03-22', 72, 40 , NULL, NULL, NULL)
    ,(37 , 1, '2006-03-22', '2006-03-22', 52, 100, NULL, NULL, NULL)
    ,(38 , 1, '2006-03-22', '2006-03-22', 56, 120, NULL, NULL, NULL)
    ,(39 , 1, '2006-03-22', '2006-03-22', 57, 80 , NULL, NULL, NULL)
    ,(40 , 1, '2006-03-22', '2006-03-22', 6 , 100, NULL, NULL, NULL)
    ,(41 , 1, '2006-03-22', '2006-03-22', 7 , 40 , NULL, NULL, NULL)
    ,(42 , 1, '2006-03-22', '2006-03-22', 8 , 40 , NULL, NULL, NULL)
    ,(43 , 1, '2006-03-22', '2006-03-22', 14, 40 , NULL, NULL, NULL)
    ,(44 , 1, '2006-03-22', '2006-03-22', 17, 40 , NULL, NULL, NULL)
    ,(45 , 1, '2006-03-22', '2006-03-22', 19, 20 , NULL, NULL, NULL)
    ,(46 , 1, '2006-03-22', '2006-03-22', 20, 40 , NULL, NULL, NULL)
    ,(47 , 1, '2006-03-22', '2006-03-22', 21, 20 , NULL, NULL, NULL)
    ,(48 , 1, '2006-03-22', '2006-03-22', 40, 120, NULL, NULL, NULL)
    ,(49 , 1, '2006-03-22', '2006-03-22', 41, 40 , NULL, NULL, NULL)
    ,(50 , 1, '2006-03-22', '2006-03-22', 48, 100, NULL, NULL, NULL)
    ,(51 , 1, '2006-03-22', '2006-03-22', 51, 40 , NULL, NULL, NULL)
    ,(52 , 1, '2006-03-22', '2006-03-22', 74, 20 , NULL, NULL, NULL)
    ,(53 , 1, '2006-03-22', '2006-03-22', 77, 60 , NULL, NULL, NULL)
    ,(54 , 1, '2006-03-22', '2006-03-22', 3 , 100, NULL, NULL, NULL)
    ,(55 , 1, '2006-03-22', '2006-03-22', 4 , 40 , NULL, NULL, NULL)
    ,(56 , 1, '2006-03-22', '2006-03-22', 5 , 40 , NULL, NULL, NULL)
    ,(57 , 1, '2006-03-22', '2006-03-22', 65, 40 , NULL, NULL, NULL)
    ,(58 , 1, '2006-03-22', '2006-03-22', 66, 80 , NULL, NULL, NULL)
    ,(59 , 1, '2006-03-22', '2006-03-22', 1 , 40 , NULL, NULL, NULL)
    ,(60 , 1, '2006-03-22', '2006-03-22', 34, 60 , NULL, NULL, NULL)
    ,(61 , 1, '2006-03-22', '2006-03-22', 43, 100, NULL, NULL, NULL)
    ,(62 , 1, '2006-03-22', '2006-03-22', 81, 125, NULL, NULL, NULL)
    ,(63 , 2, '2006-03-22', '2006-03-24', 80, 30 , NULL, NULL, NULL)
    ,(64 , 2, '2006-03-22', '2006-03-22', 7 , 10 , NULL, NULL, NULL)
    ,(65 , 2, '2006-03-22', '2006-03-22', 51, 10 , NULL, NULL, NULL)
    ,(66 , 2, '2006-03-22', '2006-03-22', 80, 10 , NULL, NULL, NULL)
    ,(67 , 2, '2006-03-22', '2006-03-22', 1 , 15 , NULL, NULL, NULL)
    ,(68 , 2, '2006-03-22', '2006-03-22', 43, 20 , NULL, NULL, NULL)
    ,(69 , 2, '2006-03-22', '2006-03-24', 19, 20 , NULL, NULL, NULL)
    ,(70 , 2, '2006-03-22', '2006-03-24', 48, 10 , NULL, NULL, NULL)
    ,(71 , 2, '2006-03-22', '2006-03-24', 8 , 17 , NULL, NULL, NULL)
    ,(72 , 1, '2006-03-24', '2006-03-24', 81, 200, NULL, NULL, NULL)
    ,(73 , 2, '2006-03-24', '2006-03-24', 81, 200, NULL, NULL, 'Fill Back Ordered product, Order #40')
    ,(74 , 1, '2006-03-24', '2006-03-24', 48, 100, NULL, NULL, NULL)
    ,(75 , 2, '2006-03-24', '2006-03-24', 48, 100, NULL, NULL, 'Fill Back Ordered product, Order #39')
    ,(76 , 1, '2006-03-24', '2006-03-24', 43, 300, NULL, NULL, NULL)
    ,(77 , 2, '2006-03-24', '2006-03-24', 43, 300, NULL, NULL, 'Fill Back Ordered product, Order #38')
    ,(78 , 1, '2006-03-24', '2006-03-24', 41, 200, NULL, NULL, NULL)
    ,(79 , 2, '2006-03-24', '2006-03-24', 41, 200, NULL, NULL, 'Fill Back Ordered product, Order #36')
    ,(80 , 1, '2006-03-24', '2006-03-24', 19, 30 , NULL, NULL, NULL)
    ,(81 , 2, '2006-03-24', '2006-03-24', 19, 30 , NULL, NULL, 'Fill Back Ordered product, Order #33')
    ,(82 , 1, '2006-03-24', '2006-03-24', 34, 100, NULL, NULL, NULL)
    ,(83 , 2, '2006-03-24', '2006-03-24', 34, 100, NULL, NULL, 'Fill Back Ordered product, Order #30')
    ,(84 , 2, '2006-03-24', '2006-04-04', 6 , 10 , NULL, NULL, NULL)
    ,(85 , 2, '2006-03-24', '2006-04-04', 4 , 10 , NULL, NULL, NULL)
    ,(86 , 3, '2006-03-24', '2006-03-24', 80, 20 , NULL, NULL, NULL)
    ,(87 , 3, '2006-03-24', '2006-03-24', 81, 50 , NULL, NULL, NULL)
    ,(88 , 3, '2006-03-24', '2006-03-24', 1 , 25 , NULL, NULL, NULL)
    ,(89 , 3, '2006-03-24', '2006-03-24', 43, 25 , NULL, NULL, NULL)
    ,(90 , 3, '2006-03-24', '2006-03-24', 81, 25 , NULL, NULL, NULL)
    ,(91 , 2, '2006-03-24', '2006-04-04', 40, 50 , NULL, NULL, NULL)
    ,(92 , 2, '2006-03-24', '2006-04-04', 21, 20 , NULL, NULL, NULL)
    ,(93 , 2, '2006-03-24', '2006-04-04', 5 , 25 , NULL, NULL, NULL)
    ,(94 , 2, '2006-03-24', '2006-04-04', 41, 30 , NULL, NULL, NULL)
    ,(95 , 2, '2006-03-24', '2006-04-04', 40, 30 , NULL, NULL, NULL)
    ,(96 , 3, '2006-03-30', '2006-03-30', 34, 12 , NULL, NULL, NULL)
    ,(97 , 3, '2006-03-30', '2006-03-30', 34, 10 , NULL, NULL, NULL)
    ,(98 , 3, '2006-03-30', '2006-03-30', 34, 1  , NULL, NULL, NULL)
    ,(99 , 2, '2006-04-03', '2006-04-03', 48, 10 , NULL, NULL, NULL)
    ,(100, 1, '2006-04-04', '2006-04-04', 57, 100, NULL, NULL, NULL)
    ,(101, 2, '2006-04-04', '2006-04-04', 57, 100, NULL, NULL, 'Fill Back Ordered product, Order #46')
    ,(102, 1, '2006-04-04', '2006-04-04', 34, 50 , NULL, NULL, NULL)
    ,(103, 1, '2006-04-04', '2006-04-04', 43, 250, NULL, NULL, NULL)
    ,(104, 3, '2006-04-04', '2006-04-04', 43, 300, NULL, NULL, 'Fill Back Ordered product, Order #41')
    ,(105, 1, '2006-04-04', '2006-04-04', 8 , 25 , NULL, NULL, NULL)
    ,(106, 2, '2006-04-04', '2006-04-04', 8 , 25 , NULL, NULL, 'Fill Back Ordered product, Order #48')
    ,(107, 1, '2006-04-04', '2006-04-04', 34, 300, NULL, NULL, NULL)
    ,(108, 2, '2006-04-04', '2006-04-04', 34, 300, NULL, NULL, 'Fill Back Ordered product, Order #47')
    ,(109, 1, '2006-04-04', '2006-04-04', 19, 25 , NULL, NULL, NULL)
    ,(110, 2, '2006-04-04', '2006-04-04', 19, 10 , NULL, NULL, 'Fill Back Ordered product, Order #42')
    ,(111, 1, '2006-04-04', '2006-04-04', 19, 10 , NULL, NULL, NULL)
    ,(112, 2, '2006-04-04', '2006-04-04', 19, 25 , NULL, NULL, 'Fill Back Ordered product, Order #48')
    ,(113, 1, '2006-04-04', '2006-04-04', 72, 50 , NULL, NULL, NULL)
    ,(114, 2, '2006-04-04', '2006-04-04', 72, 50 , NULL, NULL, 'Fill Back Ordered product, Order #46')
    ,(115, 1, '2006-04-04', '2006-04-04', 41, 50 , NULL, NULL, NULL)
    ,(116, 2, '2006-04-04', '2006-04-04', 41, 50 , NULL, NULL, 'Fill Back Ordered product, Order #45')
    ,(117, 2, '2006-04-04', '2006-04-04', 34, 87 , NULL, NULL, NULL)
    ,(118, 2, '2006-04-04', '2006-04-04', 51, 30 , NULL, NULL, NULL)
    ,(119, 2, '2006-04-04', '2006-04-04', 7 , 30 , NULL, NULL, NULL)
    ,(120, 2, '2006-04-04', '2006-04-04', 17, 40 , NULL, NULL, NULL)
    ,(121, 2, '2006-04-04', '2006-04-04', 6 , 90 , NULL, NULL, NULL)
    ,(122, 2, '2006-04-04', '2006-04-04', 4 , 30 , NULL, NULL, NULL)
    ,(123, 2, '2006-04-04', '2006-04-04', 48, 40 , NULL, NULL, NULL)
    ,(124, 2, '2006-04-04', '2006-04-04', 48, 40 , NULL, NULL, NULL)
    ,(125, 2, '2006-04-04', '2006-04-04', 41, 10 , NULL, NULL, NULL)
    ,(126, 2, '2006-04-04', '2006-04-04', 43, 5  , NULL, NULL, NULL)
    ,(127, 2, '2006-04-04', '2006-04-04', 40, 40 , NULL, NULL, NULL)
    ,(128, 2, '2006-04-04', '2006-04-04', 8 , 20 , NULL, NULL, NULL)
    ,(129, 2, '2006-04-04', '2006-04-04', 80, 15 , NULL, NULL, NULL)
    ,(130, 2, '2006-04-04', '2006-04-04', 74, 20 , NULL, NULL, NULL)
    ,(131, 2, '2006-04-04', '2006-04-04', 72, 40 , NULL, NULL, NULL)
    ,(132, 2, '2006-04-04', '2006-04-04', 3 , 50 , NULL, NULL, NULL)
    ,(133, 2, '2006-04-04', '2006-04-04', 8 , 3  , NULL, NULL, NULL)
    ,(134, 2, '2006-04-04', '2006-04-04', 20, 40 , NULL, NULL, NULL)
    ,(135, 2, '2006-04-04', '2006-04-04', 52, 40 , NULL, NULL, NULL)
    ,(136, 3, '2006-04-25', '2006-04-25', 56, 110, NULL, NULL, NULL);
	SET IDENTITY_INSERT INVENTORY_TRANSACTION OFF