﻿ALTER TABLE [dbo].[PURCHASE_ORDER_DETAIL]
ADD CONSTRAINT [FK_PURCHASE_ORDER_DETAIL__INVENTORY_TRANSACTION]
FOREIGN KEY (INVENTORY_ID)
REFERENCES [INVENTORY_TRANSACTION] (ID)