﻿SELECT 
	u.ID, u.USERNAME, u.ACTIVE 
FROM 
	[dbo].[USER] u 
WHERE 
	u.USERNAME LIKE '%' + @SearchText + '%' AND u.ACTIVE <> @IncludeInactiveUsers 
ORDER BY 
	u.USERNAME DESC