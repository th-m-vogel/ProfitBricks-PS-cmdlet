## DESCRIPTION:

Power Shell Module to access the ProfitBricks SOAP API. Manage you ProfitBricks Clous Services using PowerShell or integrate to your client management soltion.

This is a cummunitiy Project not maintained by ProfitBricks

## Download the DLL

https://github.com/th-m-vogel/ProfitBricks-PS-cmdlet/blob/master/Psmodule.binary/ProfitBricksPSmoduleSoapAPI.dll?raw=true

## Usage

Load the Module:

	Import-Module -name "Path_To\ProfitBricksPSmoduleSoapAPI.dll" [-verbose]

Initialise the API:

	Open-PBApiService [-Username] <string> [-Password] <string>
	Open-PBApiService [-Credentials] <pscredential>

Cmdlet usage:

	Verb-PBNoun {-Parameters Value , ...}

## Example

see also Sample.ps1

After loading the module open the API using yor ProfitBricks credentials (example does use PCcredentials authentication):

	$credentials = Get-Credential -Message "ProfitBricks Login"
	Open-PBApiService -Credentials $credentials

Get your ressources:

	$DC_Ressources = Get-PBDatacenterIdentifiers | Get-PBDatacenter
	$DC_Images = Get-PBImages
	$DC_ReservedIpBlocks = $DC_ReservedIpBlocks = Get-PBIpBlocks

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

## Lcense:

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

