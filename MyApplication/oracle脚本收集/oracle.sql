
--1.�����ɫ 
CREATE ROLE SELECT_ROLE ;

--2.����ɫ����Ȩ��
grant SELECT ANY TABLE to SELECT_ROLE; 

--3.����Ҫ�������û�ΪHZUSER���û������Լ����壩 �� by ���������
create user HZUSER identified by "123456";
grant connect to HZUSER;

--4.�ѽ�ɫ����ָ���˻�
grant SELECT_ROLE to HZUSER; 

--5.oracle �α�ʹ��
declare CURSOR CustCursor
IS 
select * from Hz_Userreginfo; --1.��������������ɸѡ��Ҫ���ĵ�����
CustRecord Hz_Userreginfo%rowtype;
begin
  open CustCursor;
  loop
  fetch CustCursor into CustRecord; 
  exit when CustCursor%notfound;
  --Begin����ִ�и������  , ֻ���� ���֤��idcard �� �˺�logid(��ԭ����ĩβ�ӷ���@)
  update Hz_Userreginfo set idcard = CustRecord.idcard || '@',logid=CustRecord.logid ||'@'  where MID= CustRecord.MID;
  --End
  end loop;
  close CustCursor;  
end;

--6.orcale ���ݿⱸ��

backup database 'mydb' to disk ='D:\mydb.bak' with init
restore database 'mydb' from disk='D:\mydb.bak'




