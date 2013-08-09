########################################################################
# Copyright 2013 Thomas Vogel
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
# http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
########################################################################

####
#
# It is required to import the ProfitBricksSoapApi Powershell Module
#
# If the Module is not loaded by your PowerShell Profile
# 1st load the Module using
#
# Import-Module [[PathTo]ProfitBricksSoapApi
#
####

# For authentication in this sample script Pscredential are used 
# and requested interactive at runtime
$creds = Get-Credential -Message "ProfitBricks Account"
# OPent the PBApiService using the given Credentials

Open-PBApiService -Credentials $creds

# Find the Windows Server 2012 Image in Europe Dcatacenter
$hdd_image = Get-PBImages | where {$_.imagename -like "windows-2012-server*" -and $_.region -eq "EUROPE"}

# create a new and empty Datacenter in Europe
$new_dc = New-PBDatacenter -dataCenterName "My New datacenter" -Region EUROPE

# create a new 30 Gig Storage based on the above selected Image
# and set the initial password
$new_disc = New-PBStorage -size 30 -dataCenterId $new_dc.dataCenterId -storageName "Disk1" -mountImageId $hdd_image.imageId -profitBricksImagePassword "ExtremeSecret"

# Create a Server
#    inside the newly created Datacenter
#    2 Cores
#    2 Gig RAM
#      using the above created Disk
#      connected to the Intrenet using LanId 1
$new_server = New-PBServer -cores 2 -ram 2048 -serverName "Win2012-01" -dataCenterId $new_dc.dataCenterId -lanId 1 -internetAccess $true -bootFromStorageId $new_disc.storageId -osType Windows

# done so far, just wait antil provisioning finished
write-host -NoNewline "Wait for provisioning to finish, check every 10 seconds "
do {
        write-host -NoNewline "." 
        start-sleep -s 10
} while ((Get-PBDatacenterState -dataCenterId $new_dc.dataCenterId) -ne "AVAILABLE")
write-host " done!"

# get server information after provisioning
$new_server = Get-PBServer -serverId $new_server.serverId

# give the new server nic an friendly name
$new_nic = Set-PBNic -nicId $new_server.nics[0].nicId -nicName "LAN 1"

# print the nic information
Write-Host "Primary IP is     :" $new_server.nics[0].Ips[0]
Write-Host "Gatewy IP is      :" $new_server.nics[0].gatewayIp
Write-Host "MAC Address is    :" $new_server.nics[0].macAddress

# Datacenter is ready
Write-Host "Your new Datacenter is ready for Use."
Write-Host "It may take additional time for your server to boot for the 1st time!"

# done