﻿ALTER TABLE [dbo].[ORDER_DETAIL]
ADD CONSTRAINT [FK_ORDER_DETAIL__ORDER_DETAIL_STATUS]
FOREIGN KEY (STATUS_ID)
REFERENCES [dbo].[ORDER_DETAIL_STATUS] (ID)
