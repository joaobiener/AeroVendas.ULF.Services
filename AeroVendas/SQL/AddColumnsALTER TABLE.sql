﻿select * from "UNIMEDLF"."AspNetUserRoles"
select * from  "UNIMEDLF"."AspNetUsers" T
TRUNCATE TABLE "UNIMEDLF"."AspNetUsers"


ALTER TABLE "UNIMEDLF"."AspNetUsers"
ADD("RefreshToken"      NVARCHAR2(2000),
"RefreshTokenExpiryTime" TIMESTAMP(7) WITH TIME ZONE not null);