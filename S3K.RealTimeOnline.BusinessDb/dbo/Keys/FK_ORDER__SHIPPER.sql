﻿ALTER TABLE [dbo].[ORDER] 
ADD CONSTRAINT [FK_ORDER__SHIPPER]  
FOREIGN KEY (SHIPPER_ID)  
REFERENCES [dbo].[SHIPPER](ID);