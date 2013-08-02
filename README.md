## DESCRIPTION:

PowerShell binary Module to access the ProfitBricks SOAP API. Manage you ProfitBricks Cloud Services using PowerShell or integrate ProfitBricks IaaS to your client management solution using PowerShell scripts.

This is a communitiy Project not maintained by ProfitBricks

## Download 

### just the the DLL

https://github.com/th-m-vogel/ProfitBricks-PS-cmdlet/blob/master/Psmodule.binary/ProfitBricksPSmoduleSoapAPI.dll?raw=true

### binary module as zip-file

https://github.com/th-m-vogel/ProfitBricks-PS-cmdlet/blob/master/Psmodule.binary/ProfitBricksSoapApi.zip?raw=true

## Dependencies

PowerShell V2 (`$psversiontable.psversion`)
.NET Framework 2.0 (recommendet 3.5.1)

## Installation
### Simple DLL load
Load the Module:

	Import-Module -name "Path_To\ProfitBricksPSmoduleSoapAPI.dll" [-verbose]

### PSmodule load using manifest (prefered)

	Import-Module -name "Path_To\ProfitBricksSoapApi.psd1" [-verbose]

### Module installation

Download the [ProfitBricksSoapApi.Zip-File](Psmodule.binary/ProfitBricksSoapApi.zip?raw=true) and extract all.

1. Place the resulting folder `ProfitBricksSoapApi` (does contain 3 Files) in `%USERPROFILE%\Documents\WindowsPowerShell\Modules\` will auto load the module on PowerShell start for the User.
2. Place the resulting folder `ProfitBricksSoapApi` (does contain 3 Files) in `%SYSTEMROT%\System32\WindowsPowerShell\v1.0\Modules\` will make the module system wide availible (not recomendet)
3. Place the resulting folder in any folder ouf your choice and extend the eviromet variable `PSModulePath` by this folder will make the module system wide availible. 

## Usage
Initialise the API:

	Open-PBApiService [-Username] <string> [-Password] <string>
	Open-PBApiService [-Credentials] <pscredential>

Cmdlet usage:

	Verb-PBNoun {-Parameters Value , ...}

Supported command line parameters:

For full list of all Verb-PBNoun CmdLet use  `get-help *-PB*` or open [Implemented Cmdlet.txt](Implemented Cmdlet.txt) 

Use `Get-Help Verb-PBNoun` for parameter List

All parameters are defined as positional parameters, so the parameter name is optional as long as positioning is valid. So for instance the following Set-Storage calls are valid

	Set-PBStorage -storageId xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx -storageName "New Name" -size 120
	Set-PBStorage xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx "New Name" 120
	
[List of implemented CmdLet](Implemented Cmdlet short.txt)

## Example

see also [Sample.ps1](Sample.ps1)

After loading the module open the API using yor ProfitBricks credentials (example does use PCcredentials authentication):

	$credentials = Get-Credential -Message "ProfitBricks Login"
	Open-PBApiService -Credentials $credentials

Get your ressources:

	$DC_Ressources = Get-PBDatacenterIdentifiers | Get-PBDatacenter
	$DC_Images = Get-PBImages
	$DC_ReservedIpBlocks = Get-PBIpBlocks

Create a simple Datacenter

	# Find the Windows Server 2012 Image in Europe Dcatacenter
	$hdd_image = Get-PBImages | where {$_.imagename -like "windows-2012-server*" -and $_.region -eq "EUROPE"}

	# create a new and empty Datacenter in Europe
	$new_dc = New-PBDatacenter -dataCenterName "My New datacenter" -Region EUROPE

	# create a new 30 Gig Storage based on the above selected Image
	$new_disc = New-PBStorage -size 30 -dataCenterId $new_dc.dataCenterId -storageName "Disk1" -mountImageId $hdd_image.imageId -profitBricksImagePassword "ExtremeSecret"

	# Create a Server
	#    2 Cores
	#    2 Gig RAM
	#      using the above created Disk
	#      connected to the Intrenet using LanId 1
    	$new_server = New-PBServer -cores 2 -ram 2048 -serverName "Win2012-01" -dataCenterId $new_dc.dataCenterId -lanId 1 -internetAccess $true -bootFromStorageId $new_disc.storageId -osType Windows

    	write-host -NoNewline "Wait for provisioning to finish ..."
	do {
		write-host -NoNewline "." 
		start-sleep -s 10
	} while ((Get-PBDatacenterState -dataCenterId $new_dc.dataCenterId) -ne "AVAILABLE")

	# get server information after provisioning
	$new_server = Get-PBServer -serverId $new_server.serverId

	# give the new server nic a friendly name
	$new_nic = Set-PBNic -nicId $new_server.nics[0].nicId -nicName "LAN 1"

	# print the nic ip number
	Write-host "Primary IP is: "$new_server.nics[0].Ips[0]

	# Datacenter is ready
	Write-Host "Your new Datacenter is ready for Use."
	Write-Host "It may take additional time for your server to boot for the 1st time!"
	# done

## To Do

- Implement missing CmdLets - **done**
- asap implement new API features when published by ProfitBricks - **Last Update Snapshot / DHCP 08/01/2013**
- create a dll-Help.xml and a module manifest - ongoing
- Add CmdLet New-Instance, create Server including up do 8 network connections and 8 storages
- Add CmdLet Remove-Instance, delte server including all connected storages
- Add support for -Verbos -Debug and -Confirm 

## Contribute

Send patches, feature requests etc. to thomas.m.vogel(at)outlook.com.

## License:

Copyright 2013 Thomas Vogel

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

