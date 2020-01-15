
--1.定义角色 
CREATE ROLE SELECT_ROLE ;

--2.给角色分配权限
grant SELECT ANY TABLE to SELECT_ROLE; 

--3.假设要创建的用户为HZUSER（用户名可自己定义） ， by 后面跟密码
create user HZUSER identified by "123456";
grant connect to HZUSER;

--4.把角色赋予指定账户
grant SELECT_ROLE to HZUSER; 

--5.oracle 游标使用
declare CURSOR CustCursor
IS 
select * from Hz_Userreginfo; --1.这里设置条件，筛选需要更改的数据
CustRecord Hz_Userreginfo%rowtype;
begin
  open CustCursor;
  loop
  fetch CustCursor into CustRecord; 
  exit when CustCursor%notfound;
  --Begin这里执行更新语句  , 只更改 身份证号idcard ， 账号logid(在原数据末尾加符号@)
  update Hz_Userreginfo set idcard = CustRecord.idcard || '@',logid=CustRecord.logid ||'@'  where MID= CustRecord.MID;
  --End
  end loop;
  close CustCursor;  
end;

--6.orcale 数据库备份

backup database 'mydb' to disk ='D:\mydb.bak' with init
restore database 'mydb' from disk='D:\mydb.bak'




