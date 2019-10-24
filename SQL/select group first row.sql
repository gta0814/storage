use IDCWINWorldSource20160401080039
select * from update Advisor_Tax set Is_Actv=0,Is_Del=1
where Advisor_ID  =211 and Is_Actv = 1 and Advisor_Tax_ID not in (
select Advisor_Tax_ID from (
select *, ROW_NUMBER() over (partition by tax_grp_id order by advisor_tax_id) as [row] from Advisor_Tax 
where Advisor_ID  =211 and Is_Actv = 1) groups
where groups.[row] = 1
)