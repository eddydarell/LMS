select * from AspNetUsers
select * from Courses as c
inner join ApplicationUserCourses as auc
on c.ID = auc.Course_ID
inner join AspNetUsers as au
on auc.ApplicationUser_Id = au.Id