
--1.�����ɫ 
CREATE ROLE SELECT_ROLE ;

--2.����ɫ����Ȩ��
grant SELECT ANY TABLE to SELECT_ROLE; 

--3.����Ҫ�������û�ΪHZUSER���û������Լ����壩 �� by ���������
create user HZUSER identified by "123456";
grant connect to HZUSER;

--4.�ѽ�ɫ����ָ���˻�
grant SELECT_ROLE to HZUSER; 



