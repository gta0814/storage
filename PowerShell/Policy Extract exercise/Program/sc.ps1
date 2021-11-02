#Set-ExecutionPolicy -Scope CurrentUser
#Set-ExecutionPolicy RemoteSigned
cls

    #  set csv files folder path
$csv_files = "C:\Users\ilove\Desktop\Policy Extract exercise\XML_Files"

    #  2. The process should only write a row when the document type (indicated in the XML filename) matches one of the values below.
$arr_documents = ("ACR001","ACR014","DAC001","DLAI00","CNAI00")

    #  get current script path
$local_path = split-path -parent $MyInvocation.MyCommand.Definition

    # get date time execution script
$my_data = get-date

    # set CSV name
    #  3. Output file name should be PolicyExtract_YYYYMMDD.csv (or .txt) (YYY= year, MM = month, DD = day)
$csv_name = "PolicyExtract_" + $my_data.ToString("yyyyMMdd") +".csv"

    # set csv path
$csv_path = $local_path + "\" + $csv_name

    # manually define namespaces used by those xml files
$Namespace = @{
dflt="http://guidewire.com/pc/gx/ama.gx.policy_model";
ns0="http://guidewire.com/pc/gx/ama.gx.agentdata_model" ;
ns1="http://guidewire.com/pc/gx/ama.gx.autodata_model" ;
ns2="http://guidewire.com/pc/gx/ama.gx.applicationinformationdata_model" ;
ns3="http://guidewire.com/pc/gx/ama.gx.claimdata_model" ;
ns4="http://guidewire.com/pc/gx/ama.gx.driverdata_model" ;
ns5="http://guidewire.com/pc/gx/ama.gx.vehicledata_model" ; 
ns6="http://guidewire.com/pc/gx/ama.gx.billingdata_model" ;
ns7="http://guidewire.com/pc/gx/ama.gx.addressdata_model";
ns8="http://guidewire.com/pc/gx/ama.gx.branchdata_model" ;
ns9="http://guidewire.com/pc/gx/ama.gx.documentdata_model";
ns10="http://guidewire.com/pc/gx/ama.gx.insureddata_model" 
}

    #  store csv records into this array
$csv_records = @()
$counter = 0

Get-ChildItem -Path $csv_files |
ForEach-Object{

    foreach( $doc_type in $arr_documents ){

            #  look in file name for the doc type
        if( $_.name.Contains( $doc_type )){

            write-host("Processing " + $_.FullName)

            $counter++

                # build Order Id/Ref : PolicyId + Doc Id - use PolicyNumber as there is no PolicyID
            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:PolicyNumber'

            $Order_Id_Ref = $x.Node.InnerText + "-" + $doc_type


                #  buid field First Name
                #  "Free format
                #  Note that title name, first name and last name will be merged (up to a maximum of 44 characters)."	Separated by a space, <ns5:FirstName> concatenated with <ns5:LastName> of the first <InsuredInfo><Entry> node

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:InsuredInfo/dflt:Entry[1]/ns10:FirstName'
            $y = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:InsuredInfo/dflt:Entry[1]/ns10:LastName'

                #  take first 44 characters
            $First_Name = ($x.Node.InnerText + " " + $y.Node.InnerText)

            if( $First_Name.Length -gt 44 ){
                $First_Name = $First_Name.Substring(0,44)
            }


                #  Company Name
                #  Separated by a space, <ns5:FirstName> concatenated with <ns5:LastName> of second <InsuredInfo><Entry> node (if exists)

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:InsuredInfo/dflt:Entry[2]/ns10:FirstName'
            $y = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:InsuredInfo/dflt:Entry[2]/ns10:FirstName'

            $Company_Name=""

            if( $x -and $y ){

                $Company_Name = $x.Node.InnerText + " " + $x.Node.InnerText

            }

                #  $Additional_Address_Information
                #  Separated by a space, <ns5:FirstName> concatenated with <ns5:LastName> of third <InsuredInfo><Entry> node (if exists)

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:InsuredInfo/dflt:Entry[3]/ns10:FirstName'
            $y = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:InsuredInfo/dflt:Entry[3]/ns10:FirstName'

            $Additional_Address_Information = ""

            if( $x -and $y ){

                $Additional_Address_Information = $x.Node.InnerText + " " + $x.Node.InnerText

            }


                #  <MailingAddress><ns3:AddressLine1>

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:MailingAddress/ns7:AddressLine1'

            $AddressLine1 = $x.Node.InnerText

                #  <MailingAddress><ns3:AddressLine2>

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:MailingAddress/ns7:AddressLine2'

            $AddressLine2 = $x.Node.InnerText

                #  <MailingAddress><ns3:City>

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:MailingAddress/ns7:City'

            $City = $x.Node.InnerText

                #  <MailingAddress><ns3:ProvinceCode>

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:MailingAddress/ns7:ProvinceCode'

            $ProvinceCode = $x.Node.InnerText

                #  <MailingAddress><ns3:PostalCode>

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:MailingAddress/ns7:PostalCode'

            $PostalCode = $x.Node.InnerText


                #  <MailingAddress><ns3:Country>

            $x = Select-Xml -Path $_.FullName -Namespace $Namespace -XPath '/dflt:Policy/dflt:MailingAddress/ns7:Country'

            $Country = $x.Node.InnerText

                #  some fields are defaulted to specific values

            $csv_field = New-Object PSObject

            $csv_field | Add-Member -MemberType NoteProperty -Name "Record Type" -Value "3"  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Order Id/Ref" -Value $Order_Id_Ref  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Client ID" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Title Name" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "First Name" -Value $First_Name  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Last Name" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Title / Dept." -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Company Name" -Value $Company_Name 
            $csv_field | Add-Member -MemberType NoteProperty -Name "Additional Address Information" -Value $Additional_Address_Information  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Address Line 1" -Value $AddressLine1  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Address Line 2" -Value $AddressLine2  
            $csv_field | Add-Member -MemberType NoteProperty -Name "City" -Value $City 
            $csv_field | Add-Member -MemberType NoteProperty -Name "Province or State" -Value $ProvinceCode 
            $csv_field | Add-Member -MemberType NoteProperty -Name "Postal Code or Zip Code" -Value $PostalCode  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Country Code" -Value $Country 
            $csv_field | Add-Member -MemberType NoteProperty -Name "Client Voice Phone" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Client Fax Number" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Client Email Address" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Weight" -Value "50"  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Service" -Value "908"  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Length" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Width" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Height" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Document Indicator" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Oversize indicator" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Delivery Confirmation indicator" -Value "1"  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Signature Indicator" -Value "1"  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Placeholder" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Do Not Safe Drop indicator" -Value "1"  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Card for Pickup indicator" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Proof of Age required (18)" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Proof of age required (19)" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Leave at Door indicator" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Registered Indicator" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Proof of Identity indicator" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Placeholder1" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Deliver to Post Office" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Destination Post Office ID" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Placeholder2" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Placeholder3" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Notify Recipient" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Insured Amount" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "COD Value" -Value ""  
            $csv_field | Add-Member -MemberType NoteProperty -Name "Placeholder4" -Value ""  

            $csv_records += $csv_field


        }
    }

}

    #  write to csv
$csv_records | export-csv -Path $csv_path -NoTypeInformation

write-host ("CSV created on :`r`n" + $csv_path)
 