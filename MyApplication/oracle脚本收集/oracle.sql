
--1.定义角色 
CREATE ROLE SELECT_ROLE ;

--2.给角色分配权限
grant SELECT ANY TABLE to SELECT_ROLE; 

--3.假设要创建的用户为HZUSER（用户名可自己定义） ， by 后面跟密码
create user HZUSER identified by "123456";
grant connect to HZUSER;

--4.把角色赋予指定账户
grant SELECT_ROLE to HZUSER; 



